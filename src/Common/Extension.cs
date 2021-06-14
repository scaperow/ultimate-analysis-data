using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using GoldSoft.Identiter.Common;
namespace System
{
    public static class StringExtension
    {
        /// <summary>
        /// 格式化数字库中的字段值
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static string FormatDatabaleField(this string keyword)
        {
            return keyword.Replace("'", "").Replace("‘", "").Replace("’", "");
        }

        /// <summary>
        /// 格式化表达式
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string TrunLogic(this string express)
        {
            return string.Format("|({0})|", express.Replace("|", @"\|").Replace("(或)", ")|(").Replace("(非)", "|(非)"));
        }

        /// <summary>
        /// 闭合短单位造成的重复性
        /// 例如 m 可以 匹配到 mm,m2,m3
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static string CloseUnit(this string unit)
        {
            var result = unit.ToString();

            result = Regex.Replace(result, @"\|\(m#\)\|", new MatchEvaluator(delegate(Match m)
            {
                return @"|([^m]m#)|";
            }));

            result = Regex.Replace(result, @"\|\(#m\)\|", new MatchEvaluator(delegate(Match m)
            {
                return @"|(#m([^m\[23]|$))|";
            }));

            result = Regex.Replace(result, @"\|\(g#\)\|", new MatchEvaluator(delegate(Match m)
            {
                return @"|([^k]g#)|";
            }));

            result = Regex.Replace(result, @"\|\(#g\)\|", new MatchEvaluator(delegate(Match m)
            {
                return @"|(#g)|";
            }));

            return result;
        }

        /// <summary>
        /// 转意表达式中的数字
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string TrunIntegerUseLogic(this string express)
        {
            return Regex.Replace(express, @"((\d+\.\d+)|\d+)", new MatchEvaluator(delegate(Match m)
            {
                return @"\[" + m.Value.Replace(".", @"\.") + @"\]";
            }));
        }

        /// <summary>
        /// 转意样本中的数字
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string TrunIntegerUseSample(this string express)
        {
            var r = Regex.Replace(express, @"((\d+\.\d+)|\d+)", new MatchEvaluator(delegate(Match m)
            {
                return @"[" + m.Value + @"]";
            }));

            return r;
        }

        /// <summary>
        /// 转意表达式中的数字
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string TrunIntegerUseLogic(this string express, string filter)
        {
            var r = Regex.Replace(express, @"((\d+\.\d+)|\d+)", new MatchEvaluator(delegate(Match m)
            {
                return "(" + filter.Replace("#", @"\[" + m.Value.Replace(".", @"\.") + @"\]") + ")";
            }));

            return r;
        }

        /// <summary>
        /// 转意字符
        /// </summary>
        /// <param name="express"></param>
        /// <param name="symbles"></param>
        /// <returns></returns>
        public static string TrunSymbles(this string express, params string[] symbles)
        {
            var r = express.ToString();
            foreach (var s in symbles)
            {
                r = r.Replace(s, @"\" + s);
            }

            return r;
        }

        /// <summary>
        /// 清理处理后的表达式
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public static string RetrunLogic(this string express)
        {
            if (string.IsNullOrEmpty(express))
            {
                return express;
            }

            var result = express.ToString();

            if (result.StartsWith("|"))
            {
                result = result.Remove(0, 1);
            }

            if (result.EndsWith("|"))
            {
                result = result.Remove(result.Length - 1, 1);
            }

            return result;
        }

        /// <summary>
        /// 格式化样本
        /// </summary>
        /// <param name="express"></param>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        //public static string FormatSample(this string express, string[] keys, string[] values)
        //{
        //    if (string.IsNullOrEmpty(express))
        //    {
        //        return express;
        //    }

        //    var newexpress = express.ToString();

        //    if (keys.Length == values.Length)
        //    {
        //        for (var i = 0; i < keys.Length; i++)
        //        {
        //            newexpress = express.Replace(keys[i], values[i]);
        //        }
        //    }

        //    newexpress = Regex.Replace(newexpress, @"\n\d+[\.\, \、]+", new MatchEvaluator(delegate(Match match)
        //    {
        //        return "\n";
        //    }));

        //    return newexpress;
        //}

        /// <summary>
        /// 清除项目符号
        /// </summary>
        /// <returns></returns>
        public static string ClearItemNumber(this string content)
        {
            return Regex.Replace(content, @"\n\s*\d+([\.\, \、]+|(?=\w{1}))", new MatchEvaluator(delegate(Match match)
            {
                return "\n";
            }));
        }

        /// <summary>
        /// 转换单位
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ConvertUnit(this string content, List<Fields> units)
        {
            var c = content.ToString();

            foreach (var field in units)
            {
                c = c.Replace(field["before"], field["after"]);
            }


            return c;
        }

        /// <summary>
        /// 将匹配到的关键词替换为指定的关键词
        /// </summary>
        /// <param name="content"></param>
        /// <param name="keywords"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceKeywords(this string content, string[] keywords, string value)
        {
            var c = content.ToString();

            foreach (var keyword in keywords)
            {
                c = c.Replace(keyword, value);
            }

            return c;
        }

        /// <summary>
        /// 将匹配到的关键词替换为指定的关键词
        /// </summary>
        /// <param name="content"></param>
        /// <param name="keywords"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceKeywords(this string content, string value, params string[] keywords)
        {
            var c = content.ToString();

            foreach (var keyword in keywords)
            {
                c = c.Replace(keyword, value);
            }

            return c;
        }

        /// <summary>
        /// 通过关键字截取字符串
        /// </summary>
        /// <param name="content"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static string SubstringKeywords(this string content, string left, string right)
        {
            string c = content.ToString();

            if (string.IsNullOrEmpty(right) == false)
            {
                var befores = right.Split(new string[] { "(或)" }, StringSplitOptions.RemoveEmptyEntries);
                var express_before = c.Split(befores, StringSplitOptions.RemoveEmptyEntries);
                c = express_before[0];
            }

            if (string.IsNullOrEmpty(left) == false)
            {
                var afters = left.Split(new string[] { "(或)" }, StringSplitOptions.RemoveEmptyEntries);
                var express_after = c.Split(afters, StringSplitOptions.RemoveEmptyEntries);
                if (express_after.Length > 1)
                {
                    c = express_after[1];
                }
                else
                {
                    c = express_after[0];
                }
            }

            return c;
        }

        public static bool EqualsNo(this string self, string no)
        {
            var s = self.ToLower();
            s = Regex.Replace(s, @"[换+ \*]+.*", new MatchEvaluator(delegate(Match m)
            {
                return "";
            }));

            var n = no.ToLower();
            n = Regex.Replace(n, @"[换+ \*]+.*", new MatchEvaluator(delegate(Match m)
            {
                return "";
            }));

            return s == n;
        }
    }
}

namespace System.Data
{
    public static class DataRowExtension
    {
        public static string Pick(this DataRow row, int index, string missing)
        {
            var result = missing;
            if (row.Table.Columns.Count > index)
            {
                result = row[index] +"";
            }

            return result;
        }

        public static Dictionary<string, string> Pick(this DataRow row, params string[] fields)
        {
            var result = new Dictionary<string, string>();

            foreach (var field in fields)
            {
                if (row.Table.Columns.Contains(field))
                {
                    result[field] = row.Field<string>(field);
                }
                else
                {
                    result[field] = ""; 
                }
            }

            return result;
        }

        public static object PickOfMissing(this DataRow row, string field, object missing)
        {
            
                if (row.Table.Columns.Contains(field))
                {
                    return row.Field<object>(field);
                }
                else
                {
                    return missing;
                }
            
        }

        public static Fields Pick(this DataRow row, string[] names, params string[] columns)
        {
            var result = new Fields();

            for (int i = 0; i < columns.Length; i++)
            {
                var field = columns[i];

                if (row.Table.Columns.Contains(field))
                {
                    if (names != null && i < names.Length)
                    {
                        result[names[i]] = row.Field<string>(field);
                    }
                    else
                    {
                        result[field] = row.Field<string>(field);
                    }
                }
                else
                {
                    if (names != null && i < names.Length)
                    {
                        result[names[i]] = "";
                    }
                    else
                    {
                        result[field] = "";
                    }
                }
            }

            return result;
        }
    }
}

namespace GoldSoft.Identiter.Common
{
    public class Fields : Dictionary<string, string>
    {
        public Fields()
        {
        }

        public new string this[string key]
        {
            set
            {
                base[key] = value;
            }
            get
            {
                return base.ContainsKey(key) ? base[key] : null;
            }
        }

        public Fields(string key, string value)
        {
            this[key] = value;
        }

        public Fields Append(string key, string value)
        {
            this[key] = value;

            return this;
        }
    }
}


namespace System.Runtime.CompilerServices
{
    public class DynamicAttribute : Attribute { }
}

