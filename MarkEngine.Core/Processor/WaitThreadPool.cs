/* 
 * Optical Mark Recognition 
 * Copyright 2015, Justin Fyfe
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); you 
 * may not use this file except in compliance with the License. You may 
 * obtain a copy of the License at 
 * 
 * http://www.apache.org/licenses/LICENSE-2.0 
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the 
 * License for the specific language governing permissions and limitations under 
 * the License.
 * 
 * Author: Justin
 * Date: 4-18-2015
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OmrMarkEngine.Core.Processor
{
    /// <summary>
    /// Represents an implementation of a thread pool that is separate from the default
    /// .NET thread pool
    /// </summary>
    /// <remarks>
    /// <para>From the MARC-HI Everest Framework</para>
    /// <para>Many of the processes within formatters, connectors and the core
    /// Everest Framework are multi-threaded, and represent long running tasks.
    /// This implementation of a thread pool allows for long running tasks
    /// to be executed within a constrained pool of threads without interrupting
    /// the core <see cref="T:System.Threading.ThreadPool"/>.</para>
    /// <para>This thread pool also has the ability to wait until all the
    /// threads within the thread pool have been completed and allows for
    /// a timeout to be specified (ie: wait 10 seconds for all tasks to finish).</para>
    /// <example>
    /// <code lang="cs" title="Using the WaitThreadPool">
    /// <![CDATA[
    ///     WaitThreadPool poolie = new WaitThreadPool();
    ///     for(int i = 0; i < 10; i++)
    ///         poolie.QueueUserWorkItem((WaitCallback)delegate(object state) {
    ///             for(int w = 0; w < 1000000; w++)
    ///                 Console.WriteLine("{0}", w);
    ///         });
    ///     poolie.WaitOne(new Timespan(0, 0, 1)); // Wait for one second for pool to finish
    /// ]]>
    /// </code>
    /// </example>
    /// </remarks>
    public sealed class WaitThreadPool : IDisposable
    {

        // Number of threads to keep alive
        private int m_concurrencyLevel = Environment.ProcessorCount;
        // Queue of work items
        private Queue<WorkItem> m_queue = null;
        // Active threads
        private Thread[] m_threadPool = null;
        // Hint of the number of threads waiting to be executed
        private int m_threadWait = 0;
        // True when the thread pool is being disposed
        private bool m_disposing = false;

        // Current default wait thread pool
        //public static readonly WaitThreadPool Current = new WaitThreadPool();

        /// <summary>
        /// Creates a new instance of the wait thread pool
        /// </summary>
        public WaitThreadPool() : this(Environment.ProcessorCount)
        {
        }

        /// <summary>
        /// Creates a new instance of the wait thread pool
        /// </summary>
        public WaitThreadPool(int concurrencyLevel)
        {
            this.m_concurrencyLevel = concurrencyLevel;
            this.m_queue = new Queue<WorkItem>(this.m_concurrencyLevel);
        }

        /// <summary>
        /// Worker data structure
        /// </summary>
        private struct WorkItem
        {
            /// <summary>
            /// The callback to execute on the worker
            /// </summary>
            public WaitCallback Callback { get; set; }
            /// <summary>
            /// The state or parameter to the worker
            /// </summary>
            public object State { get; set; }
            /// <summary>
            /// The execution context
            /// </summary>
            public ExecutionContext ExecutionContext { get; set; }
        }

        // Number of remaining work items
        private int m_remainingWorkItems = 1;
        // Thread is done reset event
        private ManualResetEvent m_threadDoneResetEvent = new ManualResetEvent(false);

        /// <summary>
        /// Queue a work item to be completed
        /// </summary>
        public void QueueUserWorkItem(WaitCallback callback)
        {
            QueueUserWorkItem(callback, null);
        }

        /// <summary>
        /// Queue a user work item with the specified parameters
        /// </summary>
        public void QueueUserWorkItem(WaitCallback callback, object state)
        {
            ThrowIfDisposed();
            WorkItem wd = new WorkItem()
            {
                Callback = callback,
                State = state,
                ExecutionContext = ExecutionContext.Capture()
            };
            lock (this.m_threadDoneResetEvent) this.m_remainingWorkItems++;
            EnsureStarted(); // Ensure thread pool threads are started
            lock (m_queue)
            {
                m_queue.Enqueue(wd);
                if (m_threadWait > 0)
                    Monitor.Pulse(m_queue);
            }
        }

        /// <summary>
        /// Ensure the thread pool threads are started
        /// </summary>
        private void EnsureStarted()
        {
            if (m_threadPool == null)
            {
                lock (m_queue)
                    if (m_threadPool == null)
                    {
                        m_threadPool = new Thread[m_concurrencyLevel];
                        for (int i = 0; i < m_threadPool.Length; i++)
                        {
                            m_threadPool[i] = new Thread(DispatchLoop);
                            m_threadPool[i].IsBackground = true;
                            m_threadPool[i].Start();
                        }
                    }
            }
        }

        /// <summary>
        /// Dispatch loop
        /// </summary>
        private void DispatchLoop()
        {
            while (true)
            {
                WorkItem wi = default(WorkItem);
                lock (m_queue)
                {
                    if (m_disposing) return; // Shutdown requested
                    while (m_queue.Count == 0)
                    {
                        m_threadWait++;
                        try { Monitor.Wait(m_queue); }
                        finally { m_threadWait--; }
                        if (m_disposing)
                            return;
                    }
                    wi = m_queue.Dequeue();
                }
                DoWorkItem(wi);
            }
        }
    

        /// <summary>
        /// Wait until the thread is complete
        /// </summary>
        /// <returns></returns>
        public bool WaitOne() { return WaitOne(-1, false); }
        /// <summary>
        /// Wait until the thread is complete or the specified timeout elapses
        /// </summary>
        public bool WaitOne(TimeSpan timeout, bool exitContext)
        {
            return WaitOne((int)timeout.TotalMilliseconds, exitContext);
        }
        /// <summary>
        /// Wait until the thread is completed
        /// </summary>
        public bool WaitOne(int timeout, bool exitContext)
        {
            ThrowIfDisposed();
            DoneWorkItem();
            bool rv = this.m_threadDoneResetEvent.WaitOne(timeout, exitContext);
            lock (this.m_threadDoneResetEvent)
            {
                if (rv)
                {
                    this.m_remainingWorkItems = 1;
                    this.m_threadDoneResetEvent.Reset();
                }
                else this.m_remainingWorkItems++;
            }
            return rv;
        }

        /// <summary>
        /// Perform the work if the specified work data
        /// </summary>
        private void DoWorkItem(WorkItem state)
        {
            var worker = (WorkItem)state;
            try { worker.Callback(worker.State); }
            finally { DoneWorkItem(); }
        }

        /// <summary>
        /// Complete a workf item
        /// </summary>
        private void DoneWorkItem()
        {
            lock (this.m_threadDoneResetEvent)
            {
                --this.m_remainingWorkItems;
                if (this.m_remainingWorkItems == 0) this.m_threadDoneResetEvent.Set();
            }
        }

        /// <summary>
        /// Throw an exception if the object is disposed
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (this.m_threadDoneResetEvent == null) throw new ObjectDisposedException(this.GetType().Name);
        }

        #region IDisposable Members

        /// <summary>
        /// Dispose the object
        /// </summary>
        public void Dispose()
        {
            if (this.m_threadDoneResetEvent != null)
            {
                if (this.m_remainingWorkItems > 0)
                    this.WaitOne();

                ((IDisposable)m_threadDoneResetEvent).Dispose();
                this.m_threadDoneResetEvent = null;
                m_disposing = true;
                lock (m_queue)
                    Monitor.PulseAll(m_queue);

                if(m_threadPool != null)
                    for (int i = 0; i < m_threadPool.Length; i++)
                    {
                        m_threadPool[i].Join();
                        m_threadPool[i] = null;
                    }
            }
        }

        #endregion
    }
}
