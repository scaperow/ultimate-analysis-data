using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
namespace GoldSoft.Identiter.Common
{
    [Serializable]
    public class Excel
    {
        public string ID
        {
            get;
            set;
        }
        public string ParentID
        {
            get;
            set;
        }
        public string XH
        {
            get;
            set;
        }
        public string QDBH
        {
            get;
            set;
        }
        public string QDMC
        {
            get;
            set;
        }
        public string DW
        {
            get;
            set;
        }
        public string GCLXS
        {
            get;
            set;
        }
        public string GCL
        {
            get;
            set;
        }
        public string ZY
        {
            get;
            set;
        }
        public bool HasIdentited { set; get; }
        public bool IsSubheading
        {
            get;
            set;
        }
        public bool CanIdentity { set; get; }
        public bool HasUpload { set; get; }
        public IdentityResultStateEnum? State
        {
            set;
            get;
        }
        [JsonIgnore]
        public List<DataRow> RulesMatched
        {
            set;
            get;
        }
        [JsonIgnore]
        public string SerializeString
        {
            get;
            set;
        }
        [JsonIgnore]
        public string Logging
        {
            get;
            set;
        }
        public List<Excel> ExcelsMatched
        {
            set;
            get;
        }
        public string Message
        {
            set;
            get;
        }
        public List<Excel> Subheading
        {
            get;
            set;
        }
        public string ResultForProgram { set; get; }

        public Excel()
        {
            this.Subheading = new System.Collections.Generic.List<Excel>();
            this.RulesMatched = new List<DataRow>();
            this.ExcelsMatched = new List<Excel>();
        }
        public string Serialize()
        {
            string result;
            if (this.IsSubheading)
            {
                result = string.Format("{0},{1},{2},{3}", new object[]
				{
					this.QDBH,
					this.GCLXS,
					"",
					""
				});
            }
            else
            {
                System.Text.StringBuilder express = new System.Text.StringBuilder();
                foreach (Excel subheading in this.Subheading)
                {
                    express.Append(subheading.Serialize());
                    express.Append("|");
                }
                result = express.ToString();
            }
            return result;
        }
        public static bool SetFields(Excel excel, DataRow row, ProfessionalEnum professional)
        {
            excel.ID = System.Guid.NewGuid().ToString();
            excel.ParentID = "";

            excel.XH = row.Pick(0, "");
            excel.QDBH = row.Pick(1, "");
            excel.QDMC = row.Pick(2, "");
            excel.DW = row.Pick(3, "");
            excel.GCL = row.Pick(4, "");
            excel.GCLXS = row.Pick(5, "");
            excel.ZY = ParseProfessional(professional) ;

            return true;
        }
        public static System.Collections.Generic.List<Excel> Parse(ProfessionalEnum professional, DataTable table)
        {
            System.Collections.Generic.List<Excel> excels = new System.Collections.Generic.List<Excel>();
            Excel excel = new Excel();
            foreach (DataRow row in table.Rows)
            {
                //excel.SerializeString = excel.Serialize();
                Excel.SetFields(excel, row, professional);
                if (string.IsNullOrEmpty(excel.XH))
                {
                    continue;
                }

                excels.Add(excel);
                excel = new Excel();

                //不解析子目仅仅解析清单
                //string LB = row.Field<string>("类别");
                //string text = LB;
                //if (text != null)
                //{
                //    if (!(text == "清单"))
                //    {
                //        if (text == "子目")
                //        {
                //            Excels subheading = new Excels();
                //            Excels.SetFields(subheading, row);
                //            subheading.IsSubheading = true;
                //            subheading.ParentID = excel.ID;
                //            excel.Subheading.Add(subheading);
                //        }
                //    }
                //    else
                //    {
                //        excel.SerializeString = excel.Serialize();
                //        excel = new Excels();
                //        excels.Add(excel);
                //        Excels.SetFields(excel, row);
                //    }
                //}
            }
            return excels;
        }
        public static ProfessionalEnum? ParseProfessional(string name)
        {
            ProfessionalEnum? professional = null;
            switch (name)
            {
                case "建筑装饰":
                    professional = ProfessionalEnum.Decoration;
                    break;

                case "安装":
                    professional = ProfessionalEnum.Installation;
                    break;

                case "市政":
                    professional = ProfessionalEnum.Municipal;
                    break;

                case "绿化":
                    professional = ProfessionalEnum.Green;
                    break;

                case "园林":
                    professional = ProfessionalEnum.Gardens;
                    break;
            }

            return professional;
        }
        public static string ParseProfessional(ProfessionalEnum professional)
        {
            var result = "";
            switch (professional)
            {
                case ProfessionalEnum.Decoration:
                    result = "建筑装饰";
                    break;

                case ProfessionalEnum.Installation:
                    result = "安装";
                    break;

                case ProfessionalEnum.Municipal:
                    result = "市政";
                    break;

                case ProfessionalEnum.Green:
                    result = "绿化";
                    break;

                case ProfessionalEnum.Gardens:
                    result = "园林";
                    break;
            }

            return result;
        }
        public void Log(string message)
        {
            this.Logging += System.Environment.NewLine;
            this.Logging += message;
        }
        public static string FormatSubheadings(params Excel[] excels)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < excels.Length; i++)
            {
                Excel excel = excels[i];
                builder.AppendFormat(" {0} |", excel.QDBH);
            }
            return builder.ToString();
        }
        public static string FormatSubheadings(params DataRow[] rows)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            for (int i = 0; i < rows.Length; i++)
            {
                DataRow row = rows[i];
                builder.AppendFormat(" {0} |", row.Field<string>("DEBH"));
            }
            return builder.ToString();
        }
        public static bool FullMatch(Excel[] excels, DataRow[] rows)
        {
            bool result3;
            if (excels == null && rows != null)
            {
                result3 = false;
            }
            else
            {
                if (excels != null && rows == null)
                {
                    result3 = false;
                }
                else
                {
                    if (excels.Length != rows.Length)
                    {
                        result3 = false;
                    }
                    else
                    {
                        for (int i = 0; i < excels.Length; i++)
                        {
                            Excel ea = excels[i];
                            System.Collections.Generic.IEnumerable<DataRow> result = rows.Where(delegate(DataRow m)
                            {
                                string ID = m.Field<string>("DEBH");
                                return ID.EqualsNo(ea.QDBH);
                            });
                            if (result == null || result.Count<DataRow>() < 1)
                            {
                                result3 = false;
                                return result3;
                            }
                        }
                        for (int i = 0; i < rows.Length; i++)
                        {
                            DataRow row = rows[i];
                            string ID = row.Field<string>("DEBH");
                            System.Collections.Generic.IEnumerable<Excel> result2 =
                                from m in excels
                                where ID.EqualsNo(m.QDBH)
                                select m;
                            if (result2 == null || result2.Count<Excel>() < 1)
                            {
                                result3 = false;
                                return result3;
                            }
                        }
                        result3 = true;
                    }
                }
            }
            return result3;
        }
    }
}
