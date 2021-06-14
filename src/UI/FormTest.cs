using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace GoldSoft.Identiter.UI
{
    public partial class FormTest : Form
    {
        //Identity I;

        //private Excel ExcelSource;
        //private DB Database;
        //private List<Excels> ExcelsList;

        //List<Excels> Different = new List<Excels>();
        //List<Excels> Same = new List<Excels>();
        //List<Excels> Unable = new List<Excels>();

        public FormTest()
        {
            InitializeComponent();
        }

        //private void ButtonStart_Click(object sender, EventArgs e)
        //{
        //    if (Database == null)
        //    {
        //        this.TextDBFile.Focus();

        //        return;
        //    }

        //    if (ExcelSource == null)
        //    {
        //        this.TextTestFile.Focus();

        //        return;
        //    }

        //    TextMessage.Text = "";
        //    GridDifferent.DataSource = null;
        //    GridSame.DataSource = null;
        //    GridUnable.DataSource = null;
        //    Different.Clear();
        //    Same.Clear();
        //    Unable.Clear();
        //    I = new Identity(Database);

        //    if (I.Ready())
        //    {
        //        I.IdentityOneCompleted += I_IdentityOneCompleted;
        //        I.IdentityAllCompleted += I_IdentityAllCompleted;
        //        I.StartIdentity(TextTestFile.Text);
        //    }
        //}

        //void I_IdentityAllCompleted(object sender, EventArgs e)
        //{
        //    this.Invoke(new MethodInvoker(delegate
        //    {
        //        GridDifferent.DataSource = Different;
        //        GridSame.DataSource = Same;
        //        GridUnable.DataSource = Unable;
        //    }));
        //}

        //void I_IdentityOneCompleted(object sender, IdentityResultArgs e)
        //{
        //    var result = e.Excel;

        //    switch (result.State)
        //    {
        //        case IdentityResultStateEnum.Difference:
        //            Different.Add( e.Excel);
        //            break;

        //        case IdentityResultStateEnum.Success:
        //            Same.Add( e.Excel);
        //            break;

        //        default:
        //            Unable.Add( e.Excel);
        //            break;
        //    }
        //}

        //void Identity_IdentityFinish(object sender, EventArgs e)
        //{

        //}

        //void Identity_IdentityCompleted(object sender, IdentityResultArgs e)
        //{

        //}

        //private void ButtonChooseQuoteDB_Click(object sender, EventArgs e)
        //{
        //    if (OpenForDB.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        TextDBFile.Text = OpenForDB.FileName;

        //        Database = new DB(TextDBFile.Text);
        //    }
        //}

        //private void ButtonChooseTestDB_Click(object sender, EventArgs e)
        //{
        //    if (OpenForTest.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        TextTestFile.Text = OpenForTest.FileName;

        //        ExcelSource = new Excel(TextTestFile.Text);
        //        ExcelsList = Excels.Parse(ExcelSource.QueryDatatable("SELECT * FROM [Sheet1$]", "sheet1"));
        //        Grid.DataSource = ExcelsList;
        //        LabelEverys.Text = ExcelsList.Count.ToString();
        //    }
        //}

        //void DB_Logging(string message)
        //{
        //    if (PauseLogging)
        //    {
        //        LoggingCache.Append(Environment.NewLine);
        //        LoggingCache.Append(message);
        //    }
        //    else
        //    {
        //        this.Invoke(new MethodInvoker(delegate
        //        {
        //            this.TextMessage.AppendText(Environment.NewLine);
        //            this.TextMessage.AppendText(message);
        //        }));
        //    }
        //}

        //public bool PauseLogging;
        //public StringBuilder LoggingCache;

        //private void CheckPauseLogging_CheckedChanged(object sender, EventArgs e)
        //{
        //    lock (this)
        //    {
        //        if (CheckPauseLogging.Checked)
        //        {
        //            PauseLogging = true;
        //            LoggingCache = new StringBuilder(TextMessage.Text);
        //        }
        //        else
        //        {
        //            TextMessage.Text = LoggingCache.ToString();
        //            PauseLogging = false;
        //        }
        //    }
        //}

        //public List<string> ParseIDsResult(DataRow[] rows)
        //{
        //    var excels = new List<string>();
        //    foreach (DataRow row in rows)
        //    {
        //        excels.Add(row.Field<string>("DEBH"));
        //    }
        //    return excels;
        //}

        //private void LinkPatternTest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    var test = new PatternTest();
        //    test.Show();
        //}

        //private void ButtonExportEvery_Click(object sender, EventArgs e)
        //{
        //    Export(this.Grid.DataSource as List<Excels>);
        //}

        //private void Export(List<Excels> excels)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    var builder = new StringBuilder();
        //    var file = "ExportOn[" + DateTime.Now.ToString("MM-dd HH-mm-ss") + "].txt";

        //    foreach (var excel in excels)
        //    {

        //        var json = serializer.Serialize(new
        //        {
        //            编号 = excel.QDBH,
        //            序号 = excel.XH,
        //            用户组价 = excel.ResultForUser,
        //            自动组价 = excel.ResultForProgram
        //        });

        //        builder.Append(json);
        //        builder.AppendLine();
        //    }

        //    File.WriteAllText(file, builder.ToString());

        //    Process.Start(file);
        //}

        //private void ButtonExportDifferent_Click(object sender, EventArgs e)
        //{
        //    Export(this.GridDifferent.DataSource as List<Excels>);
        //}

        //private void ButtonExportUnable_Click(object sender, EventArgs e)
        //{
        //    Export(this.GridUnable.DataSource as List<Excels>);
        //}

        //private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var control = sender as DataGridView;
        //    if (control == null)
        //    {
        //        return;
        //    }

        //    if (e.RowIndex < 0)
        //    {
        //        return;
        //    }

        //    control.Rows[e.RowIndex].Height = control.Rows[e.RowIndex].GetPreferredHeight(e.RowIndex, DataGridViewAutoSizeRowMode.AllCells, false);
        //}

        //private void FormTest_Load(object sender, EventArgs e)
        //{

        //}
    }
}
