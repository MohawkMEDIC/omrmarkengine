/* 
 * Optical Mark Recognition Engine
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
 * Date: 4-16-2015
 */

using OmrMarkEngine.Core;
using OmrMarkEngine.Core.Processor;
using OmrMarkEngine.Output;
using OmrMarkEngine.Template;
using OmrMarkEngine.Wia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OmrScannerApplication
{

   
    public partial class frmAutoScan : Form
    {

        /// <summary>
        /// Scanned pages
        /// </summary>
        private OmrPageOutputCollection m_scannedPages;
        private ScanEngine m_scanEngine = new ScanEngine();

        private object m_lockObject = new object();

        
        private Queue<Byte[]> m_workQueue = new Queue<Byte[]>();

        // Auto scan
        public frmAutoScan()
        {
            InitializeComponent();
            this.ReloadDevices();
            this.m_scanEngine.ScanCompleted += m_scanEngine_ScanCompleted;
        }

        /// <summary>
        /// Scan engine has completed
        /// </summary>
        void m_scanEngine_ScanCompleted(object sender, ScanCompletedEventArgs e)
        {

            // Enqueue the data
            this.m_workQueue.Enqueue(e.Image);
            if (!this.bwUpdate.IsBusy)
                this.bwUpdate.RunWorkerAsync();
        }

        /// <summary>
        /// Reload devices
        /// </summary>
        private void ReloadDevices()
        {
            cboScanners.Items.Clear();
            cboScanners.Items.AddRange(this.m_scanEngine.GetWiaDevices().ToArray());
        }

        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.ReloadDevices();
        }

        /// <summary>
        /// Start scan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.m_scannedPages = new OmrPageOutputCollection();
            stsMain.Enabled = true;
            this.stsMain.Style = ProgressBarStyle.Marquee;
            lblStatus.Text = "Acquiring Images...";
            groupBox1.Enabled = false;
            lsvView.Items.Clear();

            this.m_scanEngine.ScanAsync(cboScanners.SelectedItem as ScannerInfo);
            

            lblStatus.Text = "Waiting for processing to complete...";

        }

        private void lsvView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvView.SelectedItems.Count == 0) return;
            pictureBox1.Image = lsvView.SelectedItems[0].Tag as Image;

        }

        /// <summary>
        /// Do work
        /// </summary>
        private void bwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
    
            Dictionary<ListViewItem, Image> retVal = new Dictionary<ListViewItem, Image>();
            e.Result = retVal;

            // Do work 
            if(this.m_workQueue.Count > 0)
            {
                
                ScannedImage scannedImage = null;
                lock(this.m_lockObject)
                    using (var ms = new MemoryStream(this.m_workQueue.Dequeue()))
                    {
                        var img = Image.FromStream(ms);
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        scannedImage = new ScannedImage(img);

                        // Save
                        img.Save(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "input", Guid.NewGuid().ToString() + ".bmp"));
                    }
                
                
                scannedImage.Analyze();

                Image original = (Image)new AForge.Imaging.Filters.ResizeNearestNeighbor(scannedImage.Image.Width / 4, scannedImage.Image.Height / 4).Apply((Bitmap)scannedImage.Image);
                
                // Add an Error entry
                if(!scannedImage.IsScannable)
                {
                    ListViewItem lsv = new ListViewItem();
                    lsv.Tag = 2;
                    lsv.SubItems.Add(new ListViewItem.ListViewSubItem(lsv, "Scanned image doesn't appear to be a scannable form."));
                    retVal.Add(lsv, original);
                }
                else
                {
                    Engine engine = new Engine();
                    engine.SaveIntermediaryImages = true;
                    var templateFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), scannedImage.TemplateName + ".mxml");
                    if(!File.Exists(templateFile))
                    {
                        ListViewItem lsv = new ListViewItem();
                        lsv.Tag = 2; 
                        lsv.SubItems.Add(new ListViewItem.ListViewSubItem(lsv, "Template file " + templateFile + " is missing"));
                        retVal.Add(lsv, original);
                        return;
                    }

                    // Apply template
                    var pageData = engine.ApplyTemplate( 
                        OmrTemplate.Load(templateFile), 
                        scannedImage);
                    lock (this.m_lockObject)
                    {
                        if(pageData.Outcome == OmrScanOutcome.Failure)
                        {
                            ListViewItem lsv = new ListViewItem();
                            lsv.Tag = 0;
                            lsv.SubItems.Add(new ListViewItem.ListViewSubItem(lsv, pageData.ErrorMessage));
                            retVal.Add(lsv, original);
                        }
                        else
                        {
                            ListViewItem lsv = new ListViewItem();
                            lsv.Tag = 1;
                            lsv.SubItems.Add(new ListViewItem.ListViewSubItem(lsv, this.MakeSummary(pageData)));
                            retVal.Add(lsv, original);
                        }
                        this.m_scannedPages.Pages.Add(pageData);
                    }
                }
            }

           
        }

        /// <summary>
        /// Make a summary
        /// </summary>
        private string MakeSummary(OmrPageOutput pageData)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Following Data was Scanned for {0}:\r\nParms:", pageData.TemplateId);
            foreach (var parm in pageData.Parameters)
                sb.AppendFormat("{0}; ", parm);

            foreach(var itm in pageData.Details)
            {
                if(itm is OmrOutputDataCollection)
                {
                    sb.AppendFormat("(Row {0}: " , itm.Id);
                    foreach(var subItm in (itm as OmrOutputDataCollection).Details)
                    {
                        if(subItm is OmrBubbleData)
                        {
                            var bubItm = subItm as OmrBubbleData;
                            sb.AppendFormat("{0} = {1}; ", bubItm.Key, bubItm.Value);
                        }
                        else if (subItm is OmrBarcodeData)
                            sb.AppendFormat("Barcode {0}; ", (subItm as OmrBarcodeData).BarcodeData);

                    }
                    sb.Append(")\r\n");
                }
                else if (itm is OmrBubbleData)
                {
                    var bubItm = itm as OmrBubbleData;
                    sb.AppendFormat("{0} = {1} \r\n ", bubItm.Key, bubItm.Value);
                }
                else if (itm is OmrBarcodeData)
                    sb.AppendFormat("Barcode {0} \r\n ", (itm as OmrBarcodeData).BarcodeData);

            }
            return sb.ToString();
        }

        /// <summary>
        /// Completed, add the data
        /// </summary>
        private void bwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Dictionary<ListViewItem, Image> result = e.Result as Dictionary<ListViewItem, Image>;
            foreach(var itm in result)
            {
                //imlScan.Images.Add(itm.Value);
                var lsv= lsvView.Items.Add(String.Empty, (int)itm.Key.Tag);
                lsv.SubItems.Add(itm.Key.SubItems[1].Text);
                lsv.Tag = itm.Value;

            }

            if (this.m_workQueue.Count > 0)
                this.bwUpdate.RunWorkerAsync();
            else
            {
                if(this.m_scannedPages.Pages.Exists(o=>o.Outcome != OmrScanOutcome.Success) &&
                    MessageBox.Show("One or more of the sheets contained errors while scanning. Uploads to TIIS will be incomplete. Do you want to continue?", "Confirm Partial Upload", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes ||
                    !this.m_scannedPages.Pages.Exists(o=>o.Outcome != OmrScanOutcome.Success))
                {
                    // Save the scanned page
                    this.m_scannedPages.Save(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), String.Format("scan-{0}.xml", DateTime.Now.ToString("yyyyMMddHHmmss"))));
                    this.lblStatus.Text = "Uploading data to Immunization Registry...";
                    this.bwPostProcess.RunWorkerAsync();
                }
                
            }
        }

        /// <summary>
        /// Do the work of uploading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwPostProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            // Loop through each page and do an appropriate action
            var templateScriptUtil = new OmrMarkEngine.Template.Scripting.TemplateScriptUtil();

            foreach(var page in this.m_scannedPages.Pages)
            {
                // Run through the scripts
                var templateFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), page.TemplateId + ".mxml");
                // Apply template
                var template = OmrTemplate.Load(templateFile);

                // Run script
                templateScriptUtil.Run(template, page);

                
            }
        }
        
        /// <summary>
        /// Completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwPostProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                lblStatus.Text = "Error Occurred";
                MessageBox.Show("Error occurred: \r\n " + e.Error.Message, "Error Sync", MessageBoxButtons.OK);
                groupBox1.Enabled = true;
            }
            else
            {
                lblStatus.Text = "Sync Completed";
                groupBox1.Enabled = true;
                stsMain.Style = ProgressBarStyle.Continuous;
            }
        }



    }
}
