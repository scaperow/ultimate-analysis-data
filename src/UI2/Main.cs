using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Utils.Drawing;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System.IO;
using Newtonsoft.Json;
using GoldSoft.Identiter.Common;
using System.Net;

namespace Analysis
{
    public partial class Main : DevExpress.XtraEditors.XtraForm
    {
        public Main()
        {
            InitializeComponent();
            InitializeTreeList();

            var client = new WebClient();
            var args = new System.Collections.Specialized.NameValueCollection();
            args.Add("key", "323232");

            var buffer = client.UploadValues(@"http://localhost:1030/home/identity", "GET", args);
            var response = Encoding.UTF8.GetString(buffer);
            var result = JsonConvert.DeserializeObject<JsonResponse>(response);
            //var a = result["result"];
            if (string.IsNullOrEmpty(result.Error))
            {
                 MessageBox.Show(result.Error);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Visible = false;

            SplashScreen splash = new SplashScreen();
            if (splash.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Visible = true;
            }
        }

        private void InitializeTreeList()
        {
            //this.treeListColumn1.AppearanceHeader.Options.UseTextOptions = true;
            //this.treeListColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //this.treeListColumn1.Caption = "项目编码";
            //this.treeListColumn1.FieldName = "XMBM";
            //this.treeListColumn1.Fixed = DevExpress.XtraTreeList.Columns.FixedStyle.Left;
            //this.treeListColumn1.Name = "treeListColumn1";
            //this.treeListColumn1.OptionsColumn.AllowSort = false;
            //this.treeListColumn1.Visible = true;
            //this.treeListColumn1.VisibleIndex = 0;
            //this.treeListColumn1.Width = 126;
            var codeColumn = new TreeListColumn()
            {
                Caption = "项目编码",
                FieldName = "QDBH",
                Fixed = FixedStyle.Left,
                Name = "codeColumn",
                Visible = true,
                VisibleIndex = 0,
                Width = 126
            };

            var seriazeColumn = new TreeListColumn()
            {
                Caption = "序号",
                FieldName = "XH",
                Fixed = FixedStyle.Left,
                Name = "seriazeColumn",
                Visible = true,
                VisibleIndex = 0,
                Width = 80
            };

            var nameColumn = new TreeListColumn()
            {
                Caption = "项目名称",
                FieldName = "QDMC",
                Fixed = FixedStyle.Left,
                Name = "nameColumn",
                Visible = true,
                VisibleIndex = 0,
                Width = 200
            };

            var unitColumn = new TreeListColumn()
            {
                Caption = "计量单位",
                FieldName = "DW",
                Fixed = FixedStyle.Left,
                Name = "unitColumn",
                Visible = true,
                VisibleIndex = 0,
                Width = 80
            };

            var quantityColumn = new TreeListColumn()
            {
                Caption = "工程数量",
                FieldName = "GCL",
                Name = "quantityColumn",
                Visible = true,
                VisibleIndex = 0,
                Width = 80
            };

            var cloudPriceColumn = new TreeListColumn()
            {
                Caption = "云报价标识",
                FieldName = "HasAppled",
                Name = "cloudPriceColumn",
                Visible = true,
                VisibleIndex = 0,
                Width = 80
            };

            var usePriceColumn = new TreeListColumn()
            {
                Caption = "采集标识",
                FieldName = "HasUpload",
                Name = "usePriceColumn",
                Visible = true,
                VisibleIndex = 0,
                Width = 80
            };

            var complexPriceColumn = new TreeListColumn()
            {
                Caption = "组价方式",
                FieldName = "ResultForProgram",
                Name = "complexPriceColumn",
                Visible = true,
                VisibleIndex = 0,
                Width = 80
            };

            this.Tree.Columns.AddRange(new TreeListColumn[]{
                codeColumn,seriazeColumn,nameColumn,unitColumn,quantityColumn,cloudPriceColumn,usePriceColumn,complexPriceColumn
            });

            this.Tree.SelectionChanged += Tree_SelectionChanged;
            this.Tree.NodeCellStyle += Tree_NodeCellStyle;
            this.Tree.MouseClick += Tree_MouseClick;
        }

        void Tree_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Menus.ShowPopup(Cursor.Position);
            }
        }

        void Tree_SelectionChanged(object sender, EventArgs e)
        {
            if (this.Tree.Selection.Count == 0)
            {
                MenuToggle.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (this.Tree.Selection.Count == 1)
            {
                MenuToggle.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                var node = this.Tree.Selection[0];
                MenuToggle.Caption = node.Expanded ? "折叠" : "展开";
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var excels = this.Tree.DataSource as List<Excels>;
            if (excels == null)
            {
                return;
            }

            string startup = Path.Combine(Application.StartupPath, "result");
            string file = Path.Combine(startup, Guid.NewGuid() + ".uad");
            var stream = JsonConvert.SerializeObject(excels);

            File.Create(file).Close();
            File.WriteAllText(file, stream);
            System.Diagnostics.Process.Start("explorer.exe", startup);
        }

        private void ButtonOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var extension = Path.GetExtension(dialog.FileName);
                List<Excels> excels = null;
                switch (extension)
                {
                    case ".uad":
                        var stream = File.ReadAllText(dialog.FileName);
                        excels = JsonConvert.DeserializeObject<List<Excels>>(stream);
                        break;

                    case ".xlsx":
                    case ".xls":
                        var excel = new Excel(dialog.FileName);
                        var table = excel.GetFirstTableName();
                        excels = Excels.Parse(excel.QueryDatatable("SELECT *  FROM [" + table + "]", "sheet1"));
                        break;

                    default:
                        MessageBox.Show("不能识别的文件");
                        break;
                }

                Tree.DataSource = excels;
            }
        }

        private void ButtonCloudAnalysis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!(Tree.DataSource is List<Excels>))
            {
                Alert.Show(this, "提示", "没有数据");
                return;
            }

            var excels = Tree.DataSource as List<Excels>;
            if (excels == null)
            {
                Alert.Show(this, "提示", "数据无效");
                return;
            }

            //var identity = new Identity(new DB("rule.accdb"));

            //if (identity.Ready())
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //this.Enabled = false;

            //identity.IdentityAllCompleted += Identity_IdentityAllCompleted;
            //identity.IdentityOneCompleted += Identity_IdentityOneCompleted;
            //identity.StartIdentity(excels);
            //}
        }

        //void Identity_IdentityOneCompleted(object sender, IdentityResultArgs e)
        //{
        //    return;
        //    this.Invoke(new MethodInvoker(delegate
        //    {
        //        foreach (TreeListNode node in Tree.Nodes)
        //        {

        //            var record = this.Tree.GetDataRecordByNode(node) as Excels;
        //            if (record != null && record.ID == e.Excel.ID)
        //            {
        //                var builder = new StringBuilder();
        //                for (var i = 0; i < e.Excel.RulesMatched.Count; i++)
        //                {
        //                    builder.Append(e.Excel.RulesMatched[i].Pick("DEBH")["DEBH"] + ",,|");
        //                }

        //                builder.Append("@");

        //                if (record.State == IdentityResultStateEnum.Success)
        //                {
        //                    record.ResultForProgram = builder.ToString();
        //                }
        //            }
        //        }
        //    }));

        //}

        void Identity_IdentityAllCompleted(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
           {
               Tree.RefreshDataSource();
               Tree.Refresh();
               this.Cursor = Cursors.Default;
               //this.Enabled = true;
           }));
        }

        //private void Apply(List<Excels> excels, params TreeListNode[] nodes)
        //{
        //    foreach (var node in nodes)
        //    {
        //        var tag = Tree.GetDataRecordByNode(node);
        //        if (tag is Excels)
        //        {
        //            var excel = tag as Excels;

        //            if (excel != null)
        //            {
        //                if (excel.State == IdentityResultStateEnum.Success)
        //                {
        //                    var no = 0;
        //                    excels.RemoveAll(m => m.ParentID == excel.ID);
        //                    foreach (var rule in excel.RulesMatched)
        //                    {
        //                        var subheading = DB.ConvertToExcels(rule, excel.ID, ++no);
        //                        excels.Add(subheading);
        //                    }

        //                    excel.HasAppled = true;
        //                }
        //            }
        //            //node.Tag = new
        //            //{
        //            //    Appled = true
        //            //};
        //        }
        //    }

        //    Tree.RefreshDataSource();
        //}

        //private void ButtonApplyToCurrent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    var selection = this.Tree.Selection;
        //    var excels = Tree.DataSource as List<Excels>;
        //    var nodes = new List<TreeListNode>();

        //    if (selection != null && excels != null || excels.Count > 0)
        //    {
        //        Apply(excels, Tree.Selection.Cast<TreeListNode>().ToArray());
        //    }
        //}

        //private void ButtonApplyToEmpty_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    var excels = Tree.DataSource as List<Excels>;
        //    var nodes = new List<TreeListNode>();
        //    foreach (TreeListNode node in Tree.Nodes)
        //    {
        //        if (node.Tag == null)
        //        {
        //            continue;
        //        }

        //        var tag = node.Tag as dynamic;
        //        if (tag == null)
        //        {
        //            continue;
        //        }

        //        if (tag.Appled)
        //        {
        //            continue;
        //        }

        //        nodes.Add(node);
        //    }

        //    //Apply(excels, nodes);
        //}

        private void ButtonApplyToAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var excels = Tree.DataSource as List<Excels>;
            //Apply(excels, Tree.Nodes.Cast<TreeListNode>().ToArray());
        }

        private void StartReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void Tree_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            var excels = this.Tree.GetDataRecordByNode(e.Node) as Excels;
            if (excels == null)
            {
                return;
            }

            if (!excels.State.HasValue)
            {
                return;
            }

            switch (excels.State)
            {
                case IdentityResultStateEnum.Success:
                    e.Appearance.BeginUpdate();
                    e.Appearance.BackColor = Color.FromArgb(100, 189, 100);
                    e.Appearance.BackColor2 = Color.FromArgb(100, 189, 100);
                    e.Appearance.EndUpdate();
                    break;

                default:
                    e.Appearance.BackColor = Color.FromArgb(255, 255, 255);
                    e.Appearance.BackColor = Color.FromArgb(155, 155, 155);
                    e.Appearance.ForeColor = Color.FromArgb(0, 0, 0);
                    break;
            }
        }

        private void MenuToggle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.Tree.Selection.Count == 1)
            {
                var node = this.Tree.Selection[0];
                node.Expanded = !node.Expanded;
            }

        }

        private void MenuOpenAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Tree.Selection.Count == 0 || this.Tree.Selection.Count == 1)
            {
                this.Tree.ExpandAll();
            }
            else
            {
                foreach (TreeListNode node in this.Tree.Selection)
                {
                    node.Expanded = true;
                }
            }
        }

        private void FoldAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Tree.Selection.Count == 0 || this.Tree.Selection.Count == 1)
            {
                this.Tree.CollapseAll();
            }
            else
            {
                foreach (TreeListNode node in this.Tree.Selection)
                {
                    node.Expanded = false;
                }
            }
        }
    }
}
