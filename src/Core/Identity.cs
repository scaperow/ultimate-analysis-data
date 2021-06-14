using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using GoldSoft.Identiter.Common;

namespace GoldSoft.Identiter.Core
{
    public class Identity
    {
        private RulesAdapter Rules;

        /// <summary>
        /// 已经标识所有的项目的事件
        /// </summary>
        public event EventHandler IdentityAllCompleted;

        /// <summary>
        /// 已经标识一个项目的事件
        /// </summary>
        public event EventHandler<IdentityResultArgs> IdentityOneCompleted;

        public Identity(RulesAdapter rule)
        {
            Rules = rule;
        }

        /// <summary>
        /// 用于初始化本类，在使用本类其他方法成员时调用
        /// </summary>
        public bool Ready()
        {
            return true;
        }

        public IdentityResult[] IdentityQuotaOnly(ProfessionalEnum professional, Excel[] excels)
        {
            IdentityResult result = null;
            List<IdentityResult> results = new List<IdentityResult>();

            try
            {
                foreach (var excel in excels)
                {
                    result = Process(professional, excel);
                    foreach (var i in result.RulesMatched)
                    {
                        result.ExcelsMatched.Add(RulesAdapter.ConvertToExcels(i, excel.ID, 0));
                    }
                    // excel.ResultForProgram = Excels.FormatSubheadings(result.RulesMatched.ToArray());
                    // excel.ResultForUser = Excels.FormatSubheadings(excel.Subheading.ToArray());

                    if (result.State == IdentityResultStateEnum.Success)
                    {
                        result.Message = "成功";
                    }
                    else
                    {
                        //result.Message = "不能匹配";
                        if (result.RulesMatched.Count > 0)
                        {
                            result.State = IdentityResultStateEnum.Success;
                            result.Message = "成功";
                        }
                        else
                        {
                            result.Message = "不能匹配";
                            result.State = IdentityResultStateEnum.Unable;
                        }
                    }
                    results.Add(result);

                    Thread.Sleep(0);
                }
            }
            catch (Exception e)
            {
            }

            return results.ToArray();
        }

        public void StartIdentity(List<Excel> excels)
        {
            var thread = new Thread(new ThreadStart(delegate()
            {
                IdentityResult result = null;
                ProfessionalEnum? professional = null;
                IdentityResultArgs args = null;

                foreach (var excel in excels)
                {
                    professional = Excel.ParseProfessional(excel.ZY);

                    if (professional.HasValue)
                    {
                        result = Process(professional.Value, excel);


                        //excel.ResultForProgram = Excels.FormatSubheadings(result.RulesMatched.ToArray());
                        //excel.ResultForUser = Excels.FormatSubheadings(excel.Subheading.ToArray());

                        if (result.State == IdentityResultStateEnum.Success)
                        {
                            if (Excel.FullMatch(excel.Subheading.ToArray(), result.RulesMatched.ToArray()))
                            {

                                result.Message = "成功";
                            }
                            else
                            {
                                result.Message = "有差异";
                                //result.State = IdentityResultStateEnum.Success;
                            }
                        }
                        else
                        {
                            if (result.RulesMatched.Count > 0)
                            {
                                result.State = IdentityResultStateEnum.Success;
                                result.Message = "成功";
                            }
                            else
                            {
                                result.Message = "不能匹配";
                                result.State = IdentityResultStateEnum.Unable;
                            }
                        }
                    }
                    else
                    {
                        //excel.State = IdentityResultStateEnum.Unable;
                        //excel.Message = "不能识别的专业";
                        result = new IdentityResult(excel.ID)
                        {
                            Message = "不能识别的专业",
                            State = IdentityResultStateEnum.Unable
                        };
                    }

                    excel.ExcelsMatched = result.ExcelsMatched;
                    excel.Message = result.Message;
                    excel.RulesMatched = result.RulesMatched;
                    excel.State = result.State;

                    if (IdentityOneCompleted != null)
                    {
                        args = new IdentityResultArgs(excel);
                        IdentityOneCompleted(this, args);
                    }

                    Thread.Sleep(0);
                }

                if (IdentityAllCompleted != null)
                {
                    IdentityAllCompleted(this, EventArgs.Empty);
                }
            }))
            {
                IsBackground = true,
                Name = "THREAD_IDENTITY"
            };

            thread.Start();
        }

        /// <summary>
        /// 开始标识
        /// </summary>
        //public void StartIdentity(DataTable table)
        //{
        //    var excels = Excel.Parse(table);
        //    StartIdentity(excels);
        //}

        //public void StartIdentity(string fileName)
        //{
        //    var excels = Excel.Parse(new ExcelAdapter(fileName).QueryDatatable("SELECT * FROM [Sheet1$]", "sheet1"));
        //    StartIdentity(excels);
        //}



        public IdentityResult Process(ProfessionalEnum professional, Excel excels)
        {
            excels.Log("开始标识" + excels.QDMC);

            //通过专业匹配数据
            var matchedProfessional = Rules.QDFLB.Select("QDFL = '" + excels.ZY + "'");

            //通过编码匹配数据
            var QDFLBs_BM = matchedProfessional.Where(delegate(DataRow row)
            {
                var value = row.Pick("QDBM")["QDBM"];

                if (string.IsNullOrEmpty(value))
                {
                    return false;
                }

                var QDBMs = value.Split(',');
                foreach (var QDBM in QDBMs)
                {
                    if (excels.QDBH.StartsWith(QDBM))
                    {
                        return true;
                    }
                }

                return false;

            }).ToList();

            //通过ZYFL5匹配数据
            var QDFLBs_ZYFL5 = matchedProfessional.Where(delegate(DataRow row)
            {
                var profession = row.Pick("ZYFL5")["ZYFL5"];

                if (string.IsNullOrEmpty(profession))
                {
                    return true;
                }

                var express = new Fields("express", profession);
                var pattern = Pattern.Parse(express);
                if (pattern == null)
                {
                    return false;
                }

                var name = FormatNameUseGlobalRule(excels.QDMC, excels.ZY);
                var sample = new Fields("sample", name);
                return pattern.IsMatch(sample);
            }).ToList();

            var QDFLBs_BZ1 = QDFLBs_ZYFL5.Where(delegate(DataRow row)
            {

                var value = row.Pick("BZ1")["BZ1"];
                if (string.IsNullOrEmpty(value))
                {
                    return true;
                }

                return excels.QDMC.Contains(value);
            }).ToList();

            if (excels.QDBH.Length < 9 || QDFLBs_BM.Count == 0)
            {
                return ProcessOther(excels, Rules);
            }

            var index = -1;
            IdentityResult identityA = new IdentityResult(excels.ID);
            IdentityResult identityB = new IdentityResult(excels.ID);
            IdentityResult identityC = new IdentityResult(excels.ID);

            //A环节
            while (++index < QDFLBs_BM.Count)
            {
                var QDFLB = QDFLBs_BM[index];
                var BZ1 = QDFLB.Field<string>("BZ1");
                var ZYFL5 = QDFLB.Field<string>("ZYFL5");

                if (ZYFL5 == "图集")
                {

                    var identity = ProcessImages(excels, Rules, BZ1);
                    if (identity.State == IdentityResultStateEnum.Success)
                    {
                        identityA.State = IdentityResultStateEnum.Success;
                        identityA.RulesMatched.AddRange(identity.RulesMatched);
                    }
                }
                else
                {
                    excels.Log(ZYFL5 + "非图集");
                }
            }

            if (identityA.State == IdentityResultStateEnum.Success)
            {
                return identityA;
            }

            index = 0;
            excels.Log("进入 B 流程");

            //B环节
            {
                var identity = ProcessDefault(excels, Rules);
                if (identity.State == IdentityResultStateEnum.Success)
                {
                    identityB.State = IdentityResultStateEnum.Success;
                    identityB.RulesMatched.AddRange(identity.RulesMatched);
                }
            }

            if (identityB.State != IdentityResultStateEnum.Success)
            {
                excels.Log("进入 D 流程");
                return ProcessOther(excels, Rules);
            }
            else
            {
                return identityB;
            }
        }

        /// <summary>
        /// 通过清单编码获取所有的分类数据
        /// </summary>
        /// <returns></returns>
        private List<DataRow> Filter(DataRow[] db, MatchHandler match, params string[] fields)
        {
            var rows = new List<DataRow>();

            foreach (DataRow row in db)
            {
                if (match(row))
                {
                    rows.Add(row);
                }
            }

            return rows;
        }

        /// <summary>
        /// 图集判断流程
        /// </summary>
        /// <param name="sample"></param>
        /// <param name="db"></param>
        /// <param name="BZ1"></param>
        /// <returns></returns>
        public IdentityResult ProcessImages(Excel sample, RulesAdapter db, string BZ1)
        {
            var identity = new IdentityResult(sample.ID);
            var match = Regex.Match(sample.QDMC, BZ1);

            if (match.Success && match.Index > 0)
            {
                var title = sample.QDMC.Substring(match.Index, sample.QDMC.Length >= match.Index + 12 ? 12 : sample.QDMC.Length - match.Index)
                    .ConvertUnit(db.Convert).TrunIntegerUseSample();
                var regex = BZ1.TrunIntegerUseLogic() + @"\[(?<subtitle>(0[1-9]|1[0-6]))\][\s\S]*";
                var name = FormatNameUseGlobalRule(sample.QDMC, sample.ZY);
                match = Regex.Match(title, regex);
                if (match.Success && match.Length > 0)
                {
                    var number = BZ1 + match.Groups["subtitle"].Value;
                    var rows = Rules.TJBSB.Select(string.Format("YJBS = '{0}'", number));

                    foreach (DataRow row in rows)
                    {
                        var rule = row.Field<string>("SJBS");
                        if (string.IsNullOrEmpty(rule))
                        {
                            continue;
                        }

                        var pattern = Pattern.Parse(new Fields("express", rule));
                        if (regex == null)
                        {
                            continue;
                        }
                        if (pattern.IsMatch(new Fields("sample", name)) == false)
                        {
                            continue;
                        }

                        try
                        {
                            Console.WriteLine(row["SYBH"] + "");
                            var id = row.Field<string>("TJBH");
                            var type = row.Field<Int64>("SYBH");
                            rows = db.TJDEB.Select("TJBH = '" + id + "' and SYBH = '" + type + "'");

                            foreach (DataRow r in rows)
                            {
                                identity.State = IdentityResultStateEnum.Success;
                                identity.RulesMatched.Add(r);
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
            }

            return identity;
        }

        /// <summary>
        /// 一般清单判断流程
        /// </summary>
        /// <param name="excels"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public IdentityResult ProcessDefault(Excel excels, RulesAdapter db)
        {
            var ID = excels.QDBH.Length > 9 ? excels.QDBH.Substring(0, 9) : excels.QDBH;
            var identity = new IdentityResult(excels.ID);
            var sql = string.Format("QDFL = '{0}' AND QDBM LIKE '%{1}%'", excels.ZY.FormatDatabaleField(), "," + ID.FormatDatabaleField() + ",");
            var rules = db.ZDBJ.Select(sql).OrderBy(m => m.Field<int>("ID"));
            var items = new Dictionary<string, string>();
            var name = FormatNameUseGlobalRule(excels.QDMC, excels.ZY);

            foreach (DataRow rule in rules)
            {
                var BZ2 = rule.Field<string>("BZ2");
                var QDDW = rule.Field<string>("QDDW");
                if (!string.IsNullOrEmpty(BZ2) && BZ2.Contains("1") && QDDW.ToLower() != excels.DW.ToLower())
                {
                    continue;
                }

                var express = rule.Pick(new string[] { "express", "unit" }, "DEBS", "YZJFS");
                var pattern = Pattern.Parse(express);

                if (pattern == null)
                {
                    continue;
                }

                var newname = FormatNameUseQuotaRule(name, excels.ZY, rule);
                var fields = new Fields().Append("sample", newname);
                if (pattern.IsMatch(fields) == false)
                {
                    continue;
                }

                var group = rule.Field<string>("FZ");
                if (string.IsNullOrEmpty(group))
                {
                    group = "未命名";
                }

                if (items.ContainsKey(group))
                {
                    continue;
                }

                var quotaID = rule.Field<string>("DEBH");

                identity.State = IdentityResultStateEnum.Success;
                identity.RulesMatched.Add(rule);
                items[group] = quotaID;

                name = RemoveScopeUseRule(name, rule.Field<string>("ZYFL4"));
            }


            return identity;
        }

        /// <summary>
        /// 其他清单判断流程
        /// </summary>
        /// <param name="row"></param>
        /// <param name="db"></param>
        /// <param name="ZYFL5"></param>
        /// <returns></returns>
        public IdentityResult ProcessOther(Excel row, RulesAdapter db, string ZYFL5)
        {
            var identity = new IdentityResult(row.ID);
            var columns = new Queue<string>(new string[] { "ZYFL2", "ZYFL3", "ZYFL4" });
            var rules = db.ZDBJ.Select("QDFL = '" + row.ZY + "' and ZYFL5 like '%" + ZYFL5 + "%'");
            var items = new List<DataRow>();
            var name = FormatNameUseGlobalRule(row.QDMC, row.ZY);

            if (rules.Length == 0)
            {
                return identity;
            }

            while (columns.Count > 0)
            {
                items = new List<DataRow>();
                var column = columns.Dequeue();

                foreach (DataRow rule in rules)
                {
                    var express = rule.Field<string>(column);
                    if (string.IsNullOrEmpty(express))
                    {
                        items.Add(rule);
                    }
                    else
                    {
                        var pattern = Pattern.Parse(new Fields().Append("express", express));

                        if (pattern != null)
                        {
                            var newname = FormatNameUseQuotaRule(name, row.ZY, rule);
                            var fields = new Fields().Append("sample", newname);
                            if (pattern.IsMatch(fields))
                            {
                                name = RemoveScopeUseRule(name, rule.Field<string>("ZYFL4"));
                                items.Add(rule);
                            }
                        }
                    }
                }

                rules = items.ToArray();
            }

            if (items.Count == 0)
            {
                return identity;
            }

            items = new List<DataRow>();
            foreach (DataRow rule in rules)
            {
                var express = rule.Field<string>("DEBS");
                if (string.IsNullOrEmpty(express))
                {
                    items.Add(rule);
                }
                else
                {
                    var pattern = Pattern.Parse(new Fields("express", express));
                    if (pattern == null)
                    {
                        continue;
                    }

                    var newname = FormatNameUseQuotaRule(name, row.ZY, rule);
                    var fields = new Fields("sample", newname);
                    if (pattern.IsMatch(fields))
                    {
                        name = RemoveScopeUseRule(name, rule.Field<string>("ZYFL4"));
                        items.Add(rule);
                    }
                }
            }

            if (items.Count == 0)
            {
                return identity;
            }

            rules = items.ToArray();

            var builder = new StringBuilder();
            foreach (DataRow rule in rules)
            {
                var DEBH = string.Format("{0}|", rule.Field<string>("DEBH"));
                builder.AppendFormat(DEBH);
                identity.RulesMatched.Add(rule);

                row.Log("已录入定额，号码为：" + DEBH);
            }

            identity.State = IdentityResultStateEnum.Success;
            return identity;
        }

        /// <summary>
        /// 其他清单判断流程
        /// </summary>
        /// <param name="row"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public IdentityResult ProcessOther(Excel row, RulesAdapter db)
        {
            var professional = row.ZY;
            var ID = row.QDBH;

            row.QDBH = "其他";
            var identity = ProcessOther(row, db, "其他");
            var items = new Dictionary<string, DataRow>();
            var name = FormatNameUseGlobalRule(row.QDMC, row.ZY);
            row.QDBH = ID;

            if (identity.State == IdentityResultStateEnum.Success)
            {
                identity.State = IdentityResultStateEnum.Success;

                foreach (DataRow rule in identity.RulesMatched)
                {
                    var limit = rule.Field<string>("BZ2");
                    var unit = rule.Field<string>("DEDW");

                    if (!string.IsNullOrEmpty(limit) && !string.IsNullOrEmpty(unit))
                    {
                        if (limit.Contains("1") && unit != row.DW.ToUpper() && unit != row.DW.ToLower())
                        {
                            continue;
                        }
                    }

                    var fields = new Fields("express", rule.Field<string>("DEBS"))
                        .Append("unit", rule.Field<string>("YZJFS"));

                    var pattern = Pattern.Parse(fields);
                    if (pattern == null)
                    {
                        continue;
                    }

                    var newname = FormatNameUseQuotaRule(name, row.ZY, rule);
                    fields.Append("sample", newname);
                    if (pattern.IsMatch(fields) == false)
                    {
                        row.Log(row.QDMC + " 与 " + fields["express"] + " 不匹配");
                        continue;
                    }

                    var group = rule.Field<string>("FZ");

                    if (items.ContainsKey(group) == false)
                    {
                        items[group] = rule;
                        name = RemoveScopeUseRule(name, rule.Field<string>("ZYFL4"));
                    }
                }
            }

            if (items.Count == 0)
            {
                identity.State = IdentityResultStateEnum.Unable;
                return identity;
            }

            identity.RulesMatched.Clear();
            foreach (DataRow item in items.Values)
            {
                var DEBH = item.Field<string>("DEBH");
                identity.RulesMatched.Add(item);

                row.Log("已录入定额，号码为：" + DEBH);
            }

            return identity;
        }

        /// <summary>
        /// 格式化清单名称
        /// </summary>
        /// <param name="fields">[name]清单名称,[professional]专业</param>
        /// <returns></returns>
        private string FormatNameUseGlobalRule(string name, string professional)
        {
            return name.ClearItemNumber()
                .ConvertUnit(Rules.Convert)
                .ReplaceKeywords(Rules.Ignrones[professional].ToArray(), "");
        }

        /// <summary>
        /// 格式化清单名称
        /// </summary>
        /// <returns></returns>
        private string FormatNameUseQuotaRule(string name, string professional, DataRow quota)
        {
            var fields = quota.Pick("PDQM", "PDHM");
            var right = fields["PDQM"];
            var left = fields["PDHM"];

            return name.SubstringKeywords(left, right);
        }

        /// <summary>
        /// 根据规则移除对应的字符串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        private string RemoveScopeUseRule(string name, string rule)
        {

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(rule))
            {
                return name;
            }

            var pattern = Pattern.Parse(new Fields("rule", rule));
            if (pattern == null)
            {
                return name;
            }

            var result = name.ToString();
            var lines = result.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (pattern.IsMatch(new Fields("sample", line)))
                {
                    result = result.Replace(line, "");
                    break;
                }
            }

            return result;
        }

        public delegate bool MatchHandler(DataRow row);
    }
}
