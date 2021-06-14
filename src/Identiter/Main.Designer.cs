namespace Analysis
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Images = new System.Windows.Forms.ImageList(this.components);
            this.Feels = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.Bar = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.ButtonOpen = new DevExpress.XtraBars.BarLargeButtonItem();
            this.ButtonSave = new DevExpress.XtraBars.BarLargeButtonItem();
            this.ButtonCloudAnalysis = new DevExpress.XtraBars.BarLargeButtonItem();
            this.ButtonApplyToCurrent = new DevExpress.XtraBars.BarLargeButtonItem();
            this.ButtonApplyToEmpty = new DevExpress.XtraBars.BarLargeButtonItem();
            this.ButtonApplyToAll = new DevExpress.XtraBars.BarLargeButtonItem();
            this.StartReport = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.Tree = new DevExpress.XtraTreeList.TreeList();
            this.Scroll = new DevExpress.XtraEditors.XtraScrollableControl();
            this.Alert = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.Menus = new DevExpress.XtraBars.PopupMenu(this.components);
            this.MenuToggle = new DevExpress.XtraBars.BarButtonItem();
            this.MenuOpenAll = new DevExpress.XtraBars.BarButtonItem();
            this.FoldAll = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.Bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tree)).BeginInit();
            this.Scroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Menus)).BeginInit();
            this.SuspendLayout();
            // 
            // Images
            // 
            this.Images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Images.ImageStream")));
            this.Images.TransparentColor = System.Drawing.Color.Transparent;
            this.Images.Images.SetKeyName(0, "0049-folder-open.png");
            // 
            // Feels
            // 
            this.Feels.LookAndFeel.SkinName = "Sharp Plus";
            // 
            // Bar
            // 
            this.Bar.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.Bar.DockControls.Add(this.barDockControlTop);
            this.Bar.DockControls.Add(this.barDockControlBottom);
            this.Bar.DockControls.Add(this.barDockControlLeft);
            this.Bar.DockControls.Add(this.barDockControlRight);
            this.Bar.Form = this;
            this.Bar.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ButtonOpen,
            this.ButtonSave,
            this.ButtonCloudAnalysis,
            this.ButtonApplyToCurrent,
            this.ButtonApplyToEmpty,
            this.ButtonApplyToAll,
            this.StartReport,
            this.MenuToggle,
            this.MenuOpenAll,
            this.FoldAll});
            this.Bar.MaxItemId = 10;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.ButtonOpen),
            new DevExpress.XtraBars.LinkPersistInfo(this.ButtonSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.ButtonCloudAnalysis),
            new DevExpress.XtraBars.LinkPersistInfo(this.ButtonApplyToCurrent),
            new DevExpress.XtraBars.LinkPersistInfo(this.ButtonApplyToEmpty),
            new DevExpress.XtraBars.LinkPersistInfo(this.ButtonApplyToAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.StartReport)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.Caption = "打开";
            this.ButtonOpen.Glyph = ((System.Drawing.Image)(resources.GetObject("ButtonOpen.Glyph")));
            this.ButtonOpen.Id = 0;
            this.ButtonOpen.MinSize = new System.Drawing.Size(60, 0);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.ButtonOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ButtonOpen_ItemClick);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Caption = "保存";
            this.ButtonSave.Glyph = ((System.Drawing.Image)(resources.GetObject("ButtonSave.Glyph")));
            this.ButtonSave.Id = 1;
            this.ButtonSave.MinSize = new System.Drawing.Size(60, 0);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.ButtonSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ButtonSave_ItemClick);
            // 
            // ButtonCloudAnalysis
            // 
            this.ButtonCloudAnalysis.Caption = "云分析";
            this.ButtonCloudAnalysis.Glyph = ((System.Drawing.Image)(resources.GetObject("ButtonCloudAnalysis.Glyph")));
            this.ButtonCloudAnalysis.Id = 2;
            this.ButtonCloudAnalysis.MinSize = new System.Drawing.Size(60, 0);
            this.ButtonCloudAnalysis.Name = "ButtonCloudAnalysis";
            this.ButtonCloudAnalysis.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.ButtonCloudAnalysis.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ButtonCloudAnalysis_ItemClick);
            // 
            // ButtonApplyToCurrent
            // 
            this.ButtonApplyToCurrent.Caption = " 应用当前清单";
            this.ButtonApplyToCurrent.Glyph = ((System.Drawing.Image)(resources.GetObject("ButtonApplyToCurrent.Glyph")));
            this.ButtonApplyToCurrent.Id = 3;
            this.ButtonApplyToCurrent.MinSize = new System.Drawing.Size(100, 0);
            this.ButtonApplyToCurrent.Name = "ButtonApplyToCurrent";
            this.ButtonApplyToCurrent.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //this.ButtonApplyToCurrent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ButtonApplyToCurrent_ItemClick);
            // 
            // ButtonApplyToEmpty
            // 
            this.ButtonApplyToEmpty.Caption = "应用未组价清单";
            this.ButtonApplyToEmpty.Glyph = ((System.Drawing.Image)(resources.GetObject("ButtonApplyToEmpty.Glyph")));
            this.ButtonApplyToEmpty.Id = 4;
            this.ButtonApplyToEmpty.MinSize = new System.Drawing.Size(100, 0);
            this.ButtonApplyToEmpty.Name = "ButtonApplyToEmpty";
            this.ButtonApplyToEmpty.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //this.ButtonApplyToEmpty.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ButtonApplyToEmpty_ItemClick);
            // 
            // ButtonApplyToAll
            // 
            this.ButtonApplyToAll.Caption = "应用所有清单";
            this.ButtonApplyToAll.Glyph = ((System.Drawing.Image)(resources.GetObject("ButtonApplyToAll.Glyph")));
            this.ButtonApplyToAll.Id = 5;
            this.ButtonApplyToAll.MinSize = new System.Drawing.Size(100, 0);
            this.ButtonApplyToAll.Name = "ButtonApplyToAll";
            this.ButtonApplyToAll.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.ButtonApplyToAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ButtonApplyToAll_ItemClick);
            // 
            // StartReport
            // 
            this.StartReport.Caption = "开始报价";
            this.StartReport.Glyph = ((System.Drawing.Image)(resources.GetObject("StartReport.Glyph")));
            this.StartReport.Id = 6;
            this.StartReport.MinSize = new System.Drawing.Size(100, 0);
            this.StartReport.Name = "StartReport";
            this.StartReport.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.StartReport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.StartReport_ItemClick);
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(867, 59);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(117, 572);
            this.panelControl1.TabIndex = 4;
            // 
            // Tree
            // 
            this.Tree.Appearance.CustomizationFormHint.BackColor = System.Drawing.Color.White;
            this.Tree.Appearance.CustomizationFormHint.BackColor2 = System.Drawing.Color.White;
            this.Tree.Appearance.CustomizationFormHint.Options.UseBackColor = true;
            this.Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tree.Location = new System.Drawing.Point(0, 0);
            this.Tree.Name = "Tree";
            this.Tree.Size = new System.Drawing.Size(867, 572);
            this.Tree.TabIndex = 0;
            // 
            // Scroll
            // 
            this.Scroll.Controls.Add(this.Tree);
            this.Scroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Scroll.Location = new System.Drawing.Point(0, 59);
            this.Scroll.Name = "Scroll";
            this.Scroll.Size = new System.Drawing.Size(867, 572);
            this.Scroll.TabIndex = 5;
            // 
            // Alert
            // 
            this.Alert.FormLocation = DevExpress.XtraBars.Alerter.AlertFormLocation.TopRight;
            // 
            // Menus
            // 
            this.Menus.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.MenuToggle),
            new DevExpress.XtraBars.LinkPersistInfo(this.MenuOpenAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.FoldAll)});
            this.Menus.Manager = this.Bar;
            this.Menus.Name = "Menus";
            // 
            // MenuToggle
            // 
            this.MenuToggle.Caption = "展开";
            this.MenuToggle.Id = 7;
            this.MenuToggle.Name = "MenuToggle";
            this.MenuToggle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MenuToggle_ItemClick);
            // 
            // MenuOpenAll
            // 
            this.MenuOpenAll.Caption = "全部展开";
            this.MenuOpenAll.Id = 8;
            this.MenuOpenAll.Name = "MenuOpenAll";
            this.MenuOpenAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MenuOpenAll_ItemClick);
            // 
            // FoldAll
            // 
            this.FoldAll.Caption = "全部折叠";
            this.FoldAll.Id = 9;
            this.FoldAll.Name = "FoldAll";
            this.FoldAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.FoldAll_ItemClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 631);
            this.Controls.Add(this.Scroll);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new System.Drawing.Size(1000, 670);
            this.Name = "Main";
            this.Text = "工程量清单云计价 CQP";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tree)).EndInit();
            this.Scroll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Menus)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.ImageList Images;
        private DevExpress.LookAndFeel.DefaultLookAndFeel Feels;
        private DevExpress.XtraBars.BarManager Bar;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarLargeButtonItem ButtonOpen;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem ButtonSave;
        private DevExpress.XtraBars.BarLargeButtonItem ButtonCloudAnalysis;
        private DevExpress.XtraBars.BarLargeButtonItem ButtonApplyToCurrent;
        private DevExpress.XtraBars.BarLargeButtonItem ButtonApplyToEmpty;
        private DevExpress.XtraBars.BarLargeButtonItem ButtonApplyToAll;
        private DevExpress.XtraBars.BarLargeButtonItem StartReport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraTreeList.TreeList Tree;
        private DevExpress.XtraEditors.XtraScrollableControl Scroll;
        private DevExpress.XtraBars.Alerter.AlertControl Alert;
        private DevExpress.XtraBars.BarButtonItem MenuToggle;
        private DevExpress.XtraBars.BarButtonItem MenuOpenAll;
        private DevExpress.XtraBars.BarButtonItem FoldAll;
        private DevExpress.XtraBars.PopupMenu Menus;


    }
}