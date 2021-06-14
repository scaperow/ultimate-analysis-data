using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace GoldSoft.Identiter.Common
{
    /// <summary>
    /// condb 的摘要说明
    /// </summary>
    public class RulesAdapter
    {
        public delegate void LogginHandler(string message);

        private OleDbConnection Connection;
        private OleDbDataAdapter Adapter = new OleDbDataAdapter();
        private OleDbCommand Command;
        private DataSet Dataset = new DataSet();

        static RulesAdapter()
        {
        }

        private Dictionary<string, List<string>> _Ignrones;
        public Dictionary<string, List<string>> Ignrones
        {
            get
            {
                if (_Ignrones == null)
                {
                    _Ignrones = new Dictionary<string, List<string>>();

                    foreach (DataRow row in BPD.Rows)
                    {
                        var profession = row.Field<string>("QDFL");
                        var keyword = row.Field<string>("BM");

                        if (_Ignrones.ContainsKey(profession) == false)
                        {
                            _Ignrones[profession] = new List<string>();
                        }

                        _Ignrones[profession].Add(keyword);
                    }
                }

                return _Ignrones;
            }
        }

        private DataTable _QDFLB;
        /// <summary>
        /// 清单分类表
        /// </summary>
        public DataTable QDFLB
        {
            get
            {
                if (_QDFLB == null)
                {
                    _QDFLB = QueryDatatable("SELECT * FROM QDFLB", "QDFLB");
                }

                return _QDFLB;
            }
        }

        private DataTable _DWZH;
        public DataTable DWZH
        {
            get
            {
                if (_DWZH == null)
                {
                    _DWZH = QueryDatatable("SELECT * FROM DWZH", "DWZH");
                }

                return _DWZH;
            }
        }

        private DataTable _BPD;
        public DataTable BPD
        {
            get
            {
                if (_BPD == null)
                {
                    _BPD = QueryDatatable("SELECT * FROM BPD", "BPD");
                }

                return _BPD;
            }
        }

        private DataTable _ZDBJ;
        /// <summary>
        /// 报价数据表
        /// </summary>
        public DataTable ZDBJ
        {
            get
            {
                if (_ZDBJ == null)
                {
                    _ZDBJ = QueryDatatable("SELECT * FROM ZDBJ", "ZDBJ");
                }

                return _ZDBJ;
            }
        }

        private DataTable _TJBSB;
        /// <summary>
        /// 图集标示表
        /// </summary>
        public DataTable TJBSB
        {
            set
            {
                _TJBSB = value;
            }
            get
            {
                if (_TJBSB == null)
                {
                    _TJBSB = QueryDatatable("SELECT * FROM TJBSTABLE", "TJBSB");
                }

                return _TJBSB;
            }
        }

        private DataTable _TJDEB;
        /// <summary>
        /// 图集定额表
        /// </summary>
        public DataTable TJDEB
        {
            set
            {
                _TJDEB = value;
            }
            get
            {
                if (_TJDEB == null)
                {
                    _TJDEB = QueryDatatable("SELECT * FROM TJDETABLE", "TJDEB");
                }

                return _TJDEB;
            }
        }

        public List<string> _EJBSs;
        public List<string> EJBSs
        {

            get
            {
                if (_EJBSs == null)
                {
                    _EJBSs = new List<string>();
                    foreach (DataRow row in TJBSB.Rows)
                    {
                        if (_EJBSs.Contains(row.Field<string>("EJBS")) == false)
                        {
                            _EJBSs.Add(row.Field<string>("EJBS"));
                        }
                    }
                }

                return _EJBSs;
            }
        }

        public List<Fields> _Convert;
        public List<Fields> Convert
        {
            get
            {
                if (_Convert == null)
                {
                    _Convert = new List<Fields>();

                    foreach (DataRow row in DWZH.Rows)
                    {
                        _Convert.Add(row.Pick(new string[] { "before", "after" }, "转换前", "转换后"));
                    }
                }

                return _Convert;
            }
        }

        public RulesAdapter(string path)
        {
            //Connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path);
        }

        public DataTable QueryDatatable(string sql, string tableName)
        {
            //var table = new DataTable()
            //{
            //    TableName = tableName
            //};
            //Adapter = new OleDbDataAdapter(sql, Connection);
            //Adapter.Fill(table);

            //return table;

            var table = SQL.GetTable(CommandType.Text, sql, null)[0];
            table.TableName = tableName;
            return table;

        }

        public DataSet QueryDataset(string sql)
        {
            Dataset = new DataSet();
            Adapter = new OleDbDataAdapter(sql, Connection);
            Adapter.Fill(Dataset);
            return Dataset;
        }

        public bool Execute(string sql)
        {
            //Connection.Open();
            //Command = new OleDbCommand(sql, Connection);
            //Command.ExecuteNonQuery();
            //Connection.Close();
            //return true;
            try
            {
                SQL.ExecteNonQuery(CommandType.Text, sql, null);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

            return false;
        }


        public static Excel ConvertToExcels(DataRow row, string parentID, int no)
        {
            var excel = new Excel()
            {
                ID = Guid.NewGuid().ToString(),
                IsSubheading = true,
                QDBH = row.Field<string>("DEBH"),
                DW = row.Field<string>("DEDW"),
                XH = no + "",
                QDMC = row.Field<string>("DEMC"),
                GCL = "",
               // GCLXS = row.Field<string>("GCXS"),
                ParentID = parentID
            };

            return excel;
        }
    }
}
