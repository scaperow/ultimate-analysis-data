using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Aspose.Cells;

namespace GoldSoft.Identiter.Common
{
    public class ExcelAdapter
    {
        public string ConnectionString { private set; get; }
        private OleDbConnection Connection;

        public ExcelAdapter(string path)
        {
            ConnectionString = "Provider=Microsoft.Ace.OleDb.12.0;" +
                         "Data source=" + path + ";" +
                         "Extended Properties=Excel 8.0";
            Connection = new OleDbConnection(ConnectionString);
        }

        public string GetFirstTableName()
        {
            Connection.Open();
            var result = "$sheet1";
            var table = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (table.Rows.Count > 0)
            {
                result = table.Rows[0]["TABLE_NAME"].ToString();
            }
            Connection.Close();

            return result;
        }

        public DataTable QueryDatatable(string sql, string tableName)
        {
            Connection.Open();

            var adapter = new OleDbDataAdapter(sql, Connection);
            var dataset = new DataSet();
            adapter.Fill(dataset, tableName);

            Connection.Close();

            if (dataset.Tables.Contains(tableName))
            {
                return dataset.Tables[tableName];
            }

            return null;
   
        }

        public static void Save(Excel[] excels, string file)
        {
            var book = new Workbook();
            book.Worksheets.Add();

            var sheet = book.Worksheets[0];

            sheet.Cells[0, 0].PutValue("序号");
            sheet.Cells[0, 1].PutValue("项目编码");
            sheet.Cells[0, 2].PutValue("项目名称");
            sheet.Cells[0, 3].PutValue("计量单位");
            sheet.Cells[0, 4].PutValue("工程数量");
            sheet.Cells[0, 5].PutValue("组价方式");
            for (int i = 0, offset = 0; i < excels.Length; i++)
            {
                var excel = excels[i];
                var row = i + 1 + offset;
                if (!excel.IsSubheading)
                {
                    sheet.Cells[row, 0].PutValue(excel.XH);
                    sheet.Cells[row, 1].PutValue(excel.QDBH);
                    sheet.Cells[row, 2].PutValue(excel.QDMC);
                    sheet.Cells[row, 3].PutValue(excel.DW);
                    sheet.Cells[row, 4].PutValue(excel.GCL);

                    for (var sub = 0; sub < excel.Subheading.Count; sub++)
                    {
                        var si = sub + 1;
                        var subheading = excel.Subheading[sub];
                        sheet.Cells[row + si, 0].PutValue("");
                        sheet.Cells[row + si, 1].PutValue(subheading.QDBH);
                        sheet.Cells[row + si, 2].PutValue(subheading.QDMC);
                        sheet.Cells[row + si, 3].PutValue(subheading.DW);
                        sheet.Cells[row + si, 4].PutValue(subheading.GCL);
                        offset++;
                    }
                }
            }

            book.Save(file, FileFormatType.Excel2007Xlsx);
            sheet = null;
            book = null;


        }
    }
}
