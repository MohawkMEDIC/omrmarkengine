namespace ScannerTemplate
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.spReplay = new System.Windows.Forms.SplitContainer();
            this.trvDocument = new System.Windows.Forms.TreeView();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.lsvImages = new System.Windows.Forms.ListView();
            this.Preview = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlView = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstErrors = new System.Windows.Forms.ListBox();
            this.skHost1 = new FyfeSoftware.Sketchy.WinForms.SketchyCanvasHost();
            this.spProperties = new System.Windows.Forms.SplitContainer();
            this.btnAddBubble = new System.Windows.Forms.Button();
            this.btnAddBarcodeField = new System.Windows.Forms.Button();
            this.pgMain = new System.Windows.Forms.PropertyGrid();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewFromImage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewFromScanner = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScanner = new System.Windows.Forms.ToolStripMenuItem();
            this.fromImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.enableTemplateScriptsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSelectedOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbZoom = new System.Windows.Forms.TrackBar();
            this.lblZm = new System.Windows.Forms.Label();
            this.imlDocumentView = new System.Windows.Forms.ImageList(this.components);
            this.bwImageProcess = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spReplay)).BeginInit();
            this.spReplay.Panel1.SuspendLayout();
            this.spReplay.Panel2.SuspendLayout();
            this.spReplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spProperties)).BeginInit();
            this.spProperties.Panel1.SuspendLayout();
            this.spProperties.Panel2.SuspendLayout();
            this.spProperties.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.Location = new System.Drawing.Point(0, 24);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.spReplay);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.spProperties);
            this.scMain.Size = new System.Drawing.Size(754, 392);
            this.scMain.SplitterDistance = 559;
            this.scMain.TabIndex = 0;
            // 
            // spReplay
            // 
            this.spReplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spReplay.Location = new System.Drawing.Point(0, 0);
            this.spReplay.Name = "spReplay";
            // 
            // spReplay.Panel1
            // 
            this.spReplay.Panel1.Controls.Add(this.trvDocument);
            this.spReplay.Panel1.Controls.Add(this.lblCurrentPage);
            this.spReplay.Panel1.Controls.Add(this.lsvImages);
            this.spReplay.Panel1.Controls.Add(this.label2);
            this.spReplay.Panel1.Controls.Add(this.label3);
            this.spReplay.Panel1.Controls.Add(this.lstErrors);
            // 
            // spReplay.Panel2
            // 
            this.spReplay.Panel2.Controls.Add(this.skHost1);
            this.spReplay.Size = new System.Drawing.Size(559, 392);
            this.spReplay.SplitterDistance = 162;
            this.spReplay.TabIndex = 1;
            // 
            // trvDocument
            // 
            this.trvDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvDocument.Location = new System.Drawing.Point(0, 196);
            this.trvDocument.Name = "trvDocument";
            this.trvDocument.Size = new System.Drawing.Size(162, 122);
            this.trvDocument.TabIndex = 3;
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCurrentPage.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCurrentPage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCurrentPage.Location = new System.Drawing.Point(0, 178);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(162, 18);
            this.lblCurrentPage.TabIndex = 2;
            this.lblCurrentPage.Text = "No Page Selected";
            // 
            // lsvImages
            // 
            this.lsvImages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Preview,
            this.columnHeader1,
            this.columnHeader2});
            this.lsvImages.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsvImages.FullRowSelect = true;
            this.lsvImages.GridLines = true;
            this.lsvImages.LargeImageList = this.imlView;
            this.lsvImages.Location = new System.Drawing.Point(0, 18);
            this.lsvImages.Name = "lsvImages";
            this.lsvImages.ShowGroups = false;
            this.lsvImages.Size = new System.Drawing.Size(162, 160);
            this.lsvImages.SmallImageList = this.imlView;
            this.lsvImages.StateImageList = this.imlView;
            this.lsvImages.TabIndex = 1;
            this.lsvImages.TileSize = new System.Drawing.Size(64, 64);
            this.lsvImages.UseCompatibleStateImageBehavior = false;
            this.lsvImages.View = System.Windows.Forms.View.Details;
            this.lsvImages.SelectedIndexChanged += new System.EventHandler(this.lsvImages_SelectedIndexChanged);
            // 
            // Preview
            // 
            this.Preview.Text = "Valid";
            this.Preview.Width = 61;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Scan ID";
            this.columnHeader1.Width = 82;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Details";
            // 
            // imlView
            // 
            this.imlView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlView.ImageStream")));
            this.imlView.TransparentColor = System.Drawing.Color.Transparent;
            this.imlView.Images.SetKeyName(0, "task-accepted.png");
            this.imlView.Images.SetKeyName(1, "task-reject.png");
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Test Input";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(0, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Validation";
            // 
            // lstErrors
            // 
            this.lstErrors.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstErrors.FormattingEnabled = true;
            this.lstErrors.Location = new System.Drawing.Point(0, 336);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.Size = new System.Drawing.Size(162, 56);
            this.lstErrors.TabIndex = 5;
            // 
            // skHost1
            // 
            this.skHost1.AutoScroll = true;
            this.skHost1.Canvas = null;
            this.skHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skHost1.Location = new System.Drawing.Point(0, 0);
            this.skHost1.Name = "skHost1";
            this.skHost1.Size = new System.Drawing.Size(393, 392);
            this.skHost1.TabIndex = 0;
            // 
            // spProperties
            // 
            this.spProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spProperties.Location = new System.Drawing.Point(0, 0);
            this.spProperties.Name = "spProperties";
            this.spProperties.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spProperties.Panel1
            // 
            this.spProperties.Panel1.Controls.Add(this.btnAddBubble);
            this.spProperties.Panel1.Controls.Add(this.btnAddBarcodeField);
            // 
            // spProperties.Panel2
            // 
            this.spProperties.Panel2.Controls.Add(this.pgMain);
            this.spProperties.Size = new System.Drawing.Size(191, 392);
            this.spProperties.SplitterDistance = 159;
            this.spProperties.TabIndex = 0;
            // 
            // btnAddBubble
            // 
            this.btnAddBubble.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBubble.Image")));
            this.btnAddBubble.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddBubble.Location = new System.Drawing.Point(3, 37);
            this.btnAddBubble.Name = "btnAddBubble";
            this.btnAddBubble.Size = new System.Drawing.Size(185, 28);
            this.btnAddBubble.TabIndex = 1;
            this.btnAddBubble.Text = "Single Bubble Answer";
            this.btnAddBubble.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddBubble.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddBubble.UseVisualStyleBackColor = true;
            this.btnAddBubble.Click += new System.EventHandler(this.btnAddBubble_Click);
            // 
            // btnAddBarcodeField
            // 
            this.btnAddBarcodeField.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBarcodeField.Image")));
            this.btnAddBarcodeField.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddBarcodeField.Location = new System.Drawing.Point(3, 3);
            this.btnAddBarcodeField.Name = "btnAddBarcodeField";
            this.btnAddBarcodeField.Size = new System.Drawing.Size(185, 28);
            this.btnAddBarcodeField.TabIndex = 0;
            this.btnAddBarcodeField.Text = "Barcode Area";
            this.btnAddBarcodeField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddBarcodeField.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddBarcodeField.UseVisualStyleBackColor = true;
            this.btnAddBarcodeField.Click += new System.EventHandler(this.btnAddBarcodeField_Click);
            // 
            // pgMain
            // 
            this.pgMain.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.pgMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgMain.Location = new System.Drawing.Point(0, 0);
            this.pgMain.Name = "pgMain";
            this.pgMain.Size = new System.Drawing.Size(191, 229);
            this.pgMain.TabIndex = 0;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.editToolStripMenuItem,
            this.testToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(754, 24);
            this.mnuMain.TabIndex = 1;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewFromImage,
            this.mnuNewFromScanner,
            this.mnuOpen,
            this.toolStripMenuItem2,
            this.mnuSave,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuNewFromImage
            // 
            this.mnuNewFromImage.Name = "mnuNewFromImage";
            this.mnuNewFromImage.Size = new System.Drawing.Size(224, 22);
            this.mnuNewFromImage.Text = "New Template from &Image...";
            this.mnuNewFromImage.Click += new System.EventHandler(this.mnuNewFromImage_Click);
            // 
            // mnuNewFromScanner
            // 
            this.mnuNewFromScanner.Name = "mnuNewFromScanner";
            this.mnuNewFromScanner.Size = new System.Drawing.Size(224, 22);
            this.mnuNewFromScanner.Text = "New Template from &Scanner";
            // 
            // mnuOpen
            // 
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuOpen.Size = new System.Drawing.Size(224, 22);
            this.mnuOpen.Text = "&Open Template...";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(221, 6);
            // 
            // mnuSave
            // 
            this.mnuSave.Enabled = false;
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSave.Size = new System.Drawing.Size(224, 22);
            this.mnuSave.Text = "&Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(221, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.saveViewToolStripMenuItem,
            this.toolStripMenuItem4,
            this.deleteSelectedToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(175, 6);
            // 
            // saveViewToolStripMenuItem
            // 
            this.saveViewToolStripMenuItem.Name = "saveViewToolStripMenuItem";
            this.saveViewToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.saveViewToolStripMenuItem.Text = "Save View...";
            this.saveViewToolStripMenuItem.Click += new System.EventHandler(this.saveViewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(175, 6);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.deleteSelectedToolStripMenuItem.Text = "Delete Selected";
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScanner,
            this.fromImagesToolStripMenuItem,
            this.toolStripMenuItem5,
            this.enableTemplateScriptsToolStripMenuItem,
            this.saveSelectedOutputToolStripMenuItem});
            this.testToolStripMenuItem.Enabled = false;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "&Test";
            // 
            // mnuScanner
            // 
            this.mnuScanner.Name = "mnuScanner";
            this.mnuScanner.Size = new System.Drawing.Size(199, 22);
            this.mnuScanner.Text = "From &Scanner";
            // 
            // fromImagesToolStripMenuItem
            // 
            this.fromImagesToolStripMenuItem.Name = "fromImagesToolStripMenuItem";
            this.fromImagesToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.fromImagesToolStripMenuItem.Text = "From &Images...";
            this.fromImagesToolStripMenuItem.Click += new System.EventHandler(this.fromImagesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(196, 6);
            // 
            // enableTemplateScriptsToolStripMenuItem
            // 
            this.enableTemplateScriptsToolStripMenuItem.Enabled = false;
            this.enableTemplateScriptsToolStripMenuItem.Name = "enableTemplateScriptsToolStripMenuItem";
            this.enableTemplateScriptsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.enableTemplateScriptsToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.enableTemplateScriptsToolStripMenuItem.Text = "&Run Template Script";
            this.enableTemplateScriptsToolStripMenuItem.Click += new System.EventHandler(this.enableTemplateScriptsToolStripMenuItem_Click);
            // 
            // saveSelectedOutputToolStripMenuItem
            // 
            this.saveSelectedOutputToolStripMenuItem.Enabled = false;
            this.saveSelectedOutputToolStripMenuItem.Name = "saveSelectedOutputToolStripMenuItem";
            this.saveSelectedOutputToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.saveSelectedOutputToolStripMenuItem.Text = "&Save Selected Output...";
            this.saveSelectedOutputToolStripMenuItem.Click += new System.EventHandler(this.saveSelectedOutputToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbZoom);
            this.panel1.Controls.Add(this.lblZm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 21);
            this.panel1.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Idle";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(582, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zoom";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbZoom
            // 
            this.tbZoom.Dock = System.Windows.Forms.DockStyle.Right;
            this.tbZoom.Location = new System.Drawing.Point(616, 0);
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(104, 21);
            this.tbZoom.TabIndex = 10;
            this.tbZoom.Value = 5;
            this.tbZoom.Scroll += new System.EventHandler(this.tbZoom_Scroll);
            // 
            // lblZm
            // 
            this.lblZm.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblZm.Location = new System.Drawing.Point(720, 0);
            this.lblZm.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblZm.Name = "lblZm";
            this.lblZm.Size = new System.Drawing.Size(34, 21);
            this.lblZm.TabIndex = 11;
            this.lblZm.Text = "100%";
            this.lblZm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imlDocumentView
            // 
            this.imlDocumentView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlDocumentView.ImageStream")));
            this.imlDocumentView.TransparentColor = System.Drawing.Color.Transparent;
            this.imlDocumentView.Images.SetKeyName(0, "user-invisible.png");
            this.imlDocumentView.Images.SetKeyName(1, "view-barcode.png");
            this.imlDocumentView.Images.SetKeyName(2, "go-next-context.png");
            // 
            // bwImageProcess
            // 
            this.bwImageProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwImageProcess_DoWork);
            this.bwImageProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwImageProcess_RunWorkerCompleted);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 437);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "OMR Template Designer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.spReplay.Panel1.ResumeLayout(false);
            this.spReplay.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spReplay)).EndInit();
            this.spReplay.ResumeLayout(false);
            this.spProperties.Panel1.ResumeLayout(false);
            this.spProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spProperties)).EndInit();
            this.spProperties.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private FyfeSoftware.Sketchy.WinForms.SketchyCanvasHost skHost1;
        private System.Windows.Forms.SplitContainer spProperties;
        private System.Windows.Forms.PropertyGrid pgMain;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuNewFromImage;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tbZoom;
        private System.Windows.Forms.Label lblZm;
        private System.Windows.Forms.Button btnAddBarcodeField;
        private System.Windows.Forms.Button btnAddBubble;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.SplitContainer spReplay;
        private System.Windows.Forms.ListView lsvImages;
        private System.Windows.Forms.ImageList imlView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuScanner;
        private System.Windows.Forms.ToolStripMenuItem fromImagesToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader Preview;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TreeView trvDocument;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.ImageList imlDocumentView;
        private System.ComponentModel.BackgroundWorker bwImageProcess;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstErrors;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuNewFromScanner;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem enableTemplateScriptsToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem saveSelectedOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

