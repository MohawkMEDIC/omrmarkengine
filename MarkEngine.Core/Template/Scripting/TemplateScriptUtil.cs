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
 * Date: 4-17-2015
 */
using OmrMarkEngine.Output;
using OmrMarkEngine.Template;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OmrMarkEngine.Template.Scripting
{
    /// <summary>
    /// Sync script utility
    /// </summary>
    /// HACK: This is a temporary solution until I write in the Roslyn Scripting API
    public class TemplateScriptUtil
    {

        private static Object s_syncLock = new object();
        private static Dictionary<String, Assembly> s_compiledScripts = new Dictionary<string, Assembly>();

        static TemplateScriptUtil()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        /// <summary>
        /// Assembly resolution
        /// </summary>
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                if (args.Name == asm.FullName)
                    return asm;

            /// Try for an non-same number Version
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                string fAsmName = args.Name;
                if (fAsmName.Contains(","))
                    fAsmName = fAsmName.Substring(0, fAsmName.IndexOf(","));
                if (fAsmName == asm.GetName().Name)
                    return asm;
            }

            return null;
        }

        /// <summary>
        /// Process the template data
        /// </summary>
        public void Run(OmrTemplate template, OmrPageOutput processedOutput)
        {
            if(template.Script == null)
                return ;

            // First we need to compile the script
            Assembly asmToRun = null;
            if(!s_compiledScripts.TryGetValue(template.Id, out asmToRun))
                using(var strm = new StreamReader(typeof(TemplateScriptUtil).Assembly.GetManifestResourceStream("OmrMarkEngine.Core.Template.Scripting.ScriptTemplate.txt")))
                {
                    var scriptTemplate = strm.ReadToEnd();
                    scriptTemplate = scriptTemplate.Replace("$$script$$", template.Script.Script);

                    var asmOption = new CompilerParameters() {
                        GenerateExecutable = false,
                        GenerateInMemory = true,
                        CompilerOptions = "/d:TRACE /d:DEBUG",
                        IncludeDebugInformation= true
                    };
                    // Add assemblies

                    asmOption.ReferencedAssemblies.Add(typeof(INotifyPropertyChanged).Assembly.Location);
                    asmOption.ReferencedAssemblies.Add(typeof(String).Assembly.Location);
                    asmOption.ReferencedAssemblies.Add(typeof(System.Linq.Enumerable).Assembly.Location);
                    asmOption.ReferencedAssemblies.Add(typeof(OmrTemplate).Assembly.Location);
                    asmOption.ReferencedAssemblies.Add(typeof(System.Runtime.Serialization.DataContractAttribute).Assembly.Location);

                    foreach (var asmRef in template.Script.Assemblies)
                    {
                        string asmLocation = asmRef;
                        if (!Path.IsPathRooted(asmLocation))
                            asmLocation = Path.Combine(Path.GetDirectoryName(template.FileName), asmLocation);

                        Assembly.LoadFile(asmLocation);
                        asmOption.ReferencedAssemblies.Add(asmLocation);
                    }
                    // Compile
                    var compileResult = Microsoft.CSharp.CSharpCodeProvider.CreateProvider("CS").CompileAssemblyFromSource(asmOption, scriptTemplate);
                    if(compileResult.Errors.HasErrors)
                    {
                        foreach(var err in compileResult.Errors)
                            Trace.TraceError("{0}", err);
                        throw new InvalidOperationException("Error exeucting post processing script");
                    }

                    asmToRun = compileResult.CompiledAssembly;
                    s_compiledScripts.Add(template.Id, asmToRun);
                }

            
            // Now we need to run the script
            var templateScript = asmToRun.GetType("OmrScannerApplication.Scripting.Script");
            if (templateScript == null)
                throw new InvalidOperationException("Cannot execute post processing script");
            var errField = templateScript.GetField("Err");
            var runMethod = templateScript.GetMethod("Run", new Type[] { typeof(OmrPageOutput) });
            if (runMethod == null || errField == null)
                throw new InvalidOperationException("Cannot execute post processing script");

            // Invoke
            try
            {
                lock (s_syncLock)
                {
                    errField.SetValue(null, null);
                    runMethod.Invoke(null, new Object[] { processedOutput });
                    if (!String.IsNullOrEmpty(errField.GetValue(null) as String))
                        throw new ScriptingErrorException(errField.GetValue(null).ToString());
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
