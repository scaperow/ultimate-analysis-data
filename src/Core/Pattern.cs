using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using GoldSoft.Identiter.Common;
using Newtonsoft.Json;

namespace GoldSoft.Identiter.Core
{
    public class Pattern
    {
        public const string NumberPattern = @"\[(?<number>(\d+\.\d+|\d+))\]";
        public List<Pattern> Patternes;
        public List<MatchRule> Rules;

        private Pattern()
        {
            Patternes = new List<Pattern>();
            Rules = new List<MatchRule>();
        }

        public bool IsMatch(Fields fields)
        {
            if (fields.ContainsKey("sample"))
            {
                fields["sample"] = fields["sample"].TrunIntegerUseSample();
            }

            return Match(fields);
        }

        private bool Match(Fields sample)
        {
            if (Rules.Count > 0)
            {
                foreach (var rule in Rules)
                {
                    if (rule.Match(sample))
                    {
                        return true;
                    }
                }

                return false;
            }

            if (Patternes.Count > 0)
            {

                foreach (var sub in Patternes)
                {
                    if (sub.Match(sample) == false)
                    {
                        return false;
                    }
                }

                return true;
            }

            return true;
        }

        public string ToJson()
        {
            if (Rules.Count > 0)
            {
                var builder = new StringBuilder();

                foreach (var rule in Rules)
                {
                    builder.AppendLine();
                    builder.Append("    RULE : " + rule.ToJson());
                }

                return builder.ToString();
            }
            else
            {

                var builder = new StringBuilder();

                foreach (var pattern in Patternes)
                {
                    builder.AppendLine();
                    builder.Append("SUBPATTERN : " + pattern.ToJson());
                }

                return builder.ToString();
            }
        }

        private void FillRules(Fields fields)
        {
            var math = new Multiplication();
            if (math.IsParse(fields))
            {
                fields["express"] = math.Parse(fields);
                Rules.Add(math);
            }

            var numberrange = new NumberRangeRule(fields);
            if (numberrange.IsParse(fields))
            {
                fields["express"] = numberrange.Parse(fields);
                Rules.Add(numberrange);
            }

            var non = new Nonstring();
            if (non.IsParse(fields))
            {
                fields["express"] = non.Parse(fields);
                Rules.Add(non);
            }

            var unit = new UnitRule();
            if (unit.IsParse(fields))
            {
                fields["express"] = unit.Parse(fields);
                Rules.Add(unit);
            }

            var logic = new LogicRule(fields);
            if (logic.IsParse(fields))
            {
                fields["express"] = logic.Parse(fields);
                Rules.Add(logic);
            }
        }

        public static Pattern Parse(Fields fields)
        {
            var express = (fields.ContainsKey("express") ? fields["express"] : "")
                .Replace("(非)", "[与](非)");

            var unit = fields.ContainsKey("unit") ? fields["unit"] : "";

            if (string.IsNullOrEmpty(express))
            {
                return null;
            }

            var pattern = new Pattern();
            var expresses = express.Split(new string[] { "[与]" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in expresses)
            {
                var subpattern = new Pattern();
                var subfields = fields;

                fields["express"] = item.TrunLogic();

                subpattern.FillRules(subfields);
                pattern.Patternes.Add(subpattern);
            }

            return pattern;
        }

        public static string GetNumberRangePattern(int from, int to)
        {
            if (from < 0 || to < 0)
            {
                throw new Exception("Negative values not supported");
            }

            if (from > to)
            {
                throw new Exception("Invalid range $from..$to, from > to");
            }

            var ranges = new List<int>();
            var increment = 1;
            var next = from;
            var higher = true;

            ranges.Add(from);

            while (true)
            {

                next += increment;

                if (next + increment > to)
                {
                    if (next <= to)
                    {
                        ranges.Add(next);
                    }
                    increment /= 10;
                    higher = false;
                }
                else if (next % (increment * 10) == 0)
                {
                    ranges.Add(next);
                    increment = higher ? increment * 10 : increment / 10;
                }

                if (!higher && increment < 10)
                {
                    break;
                }
            }

            ranges.Add(to + 1);

            var regex = @"\D+(?:";

            for (var i = 0; i < ranges.Count - 1; i++)
            {
                var str_from = ranges[i].ToString();
                var str_to = "";
                if (ranges.Count > i + 1)
                {
                    str_to = (ranges[i + 1] - 1).ToString();
                }

                for (var j = 0; j < str_from.Length; j++)
                {
                    if (str_from[j] == str_to[j])
                    {
                        regex += str_from[j];
                    }
                    else
                    {
                        regex += "[" + str_from[j] + "-" + str_to[j] + "]";
                    }
                }
                regex += "|";
            }

            return regex.Substring(0, regex.Length - 1) + @")\D+";
        }

        private static String GetLessEqualsPattern(string value)
        {
            int length = value.Length;
            if (length > 1)
            {
                int i = int.Parse(value.Substring(0, 1));
                if (i == 0)
                {
                    value = value.Substring(1);
                    if (value.Replace("0*", "") != "")
                    {
                        value = i + GetLessEqualsPattern(value);
                    }
                    else
                    {
                        value = null;
                    }
                }
                else
                {
                    i -= 1;
                    value = "[0-" + i + "]\\d{" + (length - 1) + '}';
                }
            }
            else
            {
                value = "[0-" + value + ']';
            }

            return value;
        }

        public static String GetLessEqualsPattern(int value)
        {
            if (value < 0)
            {
                throw new Exception();
            }

            var valueString = value.ToString();
            int length = valueString.Length;
            var p = @"\D+(";

            if (length > 1)
            {
                p += "\\d{1," + (length - 1) + '}';
            }

            var p2 = "";
            for (int i = 0; i < length; i++)
            {
                p2 = GetLessEqualsPattern(valueString.Substring(i));
                if (null != p2)
                {
                    p += '|' + valueString.Substring(0, i) + p2;
                }
            }
            p += @")\D+";
            return p;
        }

    }

    public abstract class MatchRule
    {

        public abstract string Parse(Fields fields);

        public abstract bool Match(Fields fields);

        public abstract bool IsParse(Fields fields);

        public abstract string ToJson();
    }

    /// <summary>
    /// 匹配数字范围
    /// </summary>
    public class NumberRangeRule : MatchRule
    {
        private const string ParsePattern = @"(?<start>(\d+\.\d+|\d+))～(?<end>(\d+\.\d+|\d+))";
        private readonly string MatchPattern = "";
        private Range<string> NumberRange;
        private DataTable Calculator;
        private Fields Field;

        public NumberRangeRule(Fields fields)
        {
            var unit = fields.ContainsKey("unit") ? fields["unit"] : "";

            if (string.IsNullOrEmpty(unit))
            {
                MatchPattern = Pattern.NumberPattern;
            }
            else
            {
                MatchPattern = unit.TrunLogic().TrunIntegerUseLogic().CloseUnit().Replace("#", Pattern.NumberPattern).RetrunLogic();
            }

            Calculator = new DataTable();
            Field = fields;
        }

        public override string Parse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            var match = Regex.Match(express, ParsePattern);

            if (match.Success && match.Length > 0)
            {
                var start = match.Groups["start"].Value.Replace(@"\.", ".");
                var end = match.Groups["end"].Value.Replace(@"\.", ".");

                NumberRange = new Range<string>(start, end);
            }

            express = Regex.Replace(express, ParsePattern, new MatchEvaluator(delegate(Match m)
            {
                return "";
            }));

            return express;
        }

        public override bool Match(Fields fields)
        {
            var sample = fields.ContainsKey("sample") ? fields["sample"] : "";
            var match = Regex.Match(sample, MatchPattern);

            while (match.Success && match.Length > 0)
            {
                var number = match.Groups["number"].Value;
                var sql = string.Format("{0} >= {1} and {0} <= {2}", number, NumberRange.Min, NumberRange.Max);

                var result = Calculator.Compute(sql, "");

                if (result is bool)
                {
                    var b = bool.Parse(result.ToString());

                    if (b)
                    {
                        return true;
                    }
                }

                match = match.NextMatch();
            }

            return false;
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(new
            {
                NumberRangeRule = new
                {
                    MatchPattern = MatchPattern,
                    Min = NumberRange == null ? "" : NumberRange.Min,
                    Max = NumberRange == null ? "" : NumberRange.Max
                }
            });
        }

        public override bool IsParse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            var match = Regex.Match(express, ParsePattern);

            if (string.IsNullOrEmpty(express))
            {
                return false;
            }

            if (match.Success && match.Length > 0)
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 匹配连乘的规则
    /// </summary>
    public class Multiplication : MatchRule
    {
        private const string ParsePattern = @"\|\(a\*b(?<thired>\*c){0,1}\,(?<result>.*)\)(?=\|)";
        private const string MatchPattern = @"\[(?<a>\d+)\]\*\[(?<b>\d+)\](\*\[(?<c>\d+)\]){0,1}";
        private const string RangePattern = @"(?<math>[abc\+-]+)[>=<]+(?<min>(\d+))～(?<max>(\d+))";
        private string MathExpress = "";
        private static DataTable Calculator;
        private bool HasThiredNumber = true;

        public Multiplication()
        {
            Calculator = new DataTable();
        }

        public override string Parse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            var match = Regex.Match(express, ParsePattern);

            if (match.Success == false || match.Length == 0)
            {
                return express;
            }

            var result = match.Groups["result"].Value;
            var thired = match.Groups["thired"].Value;
            var r = Regex.Match(result, RangePattern);

            result = Regex.Replace(result, RangePattern, new MatchEvaluator(delegate(Match m)
            {
                var min = r.Groups["min"].Value;
                var max = r.Groups["max"].Value;
                var math = r.Groups["math"].Value;

                return string.Format("{0}>={1} and {0}<={2}", math, min, max);
            }));

            result = string.Format("({0})", result.Replace("与", ") and (").Replace("或", ") or ("));

            MathExpress = result;
            HasThiredNumber = string.IsNullOrEmpty(thired) == false;

            express = Regex.Replace(express, ParsePattern, new MatchEvaluator(delegate(Match m)
            {
                return "";
            }));

            return express;
        }

        public override bool Match(Fields fields)
        {
            var sample = fields.ContainsKey("sample") ? fields["sample"] : "";
            var match = Regex.Match(sample, MatchPattern);

            if (match.Success == false || match.Length == 0)
            {
                return false;
            }

            var a = match.Groups["a"].Value;
            var b = match.Groups["b"].Value;
            var c = match.Groups["c"].Value;

            //三连乘不能匹配两连乘
            if (HasThiredNumber && string.IsNullOrEmpty(c))
            {
                return false;
            }

            //下列语句会将 and 替换为 [数字]nd
            var math = MathExpress.Replace("and", "end")
                .Replace("a", a.ToString())
                .Replace("b", b.ToString())
                .Replace("c", c.ToString())
                .Replace("end", "and");
            object o = null;

            try
            {
                o = Calculator.Compute(math, "");
            }
            catch (Exception)
            {
            }

            if (o is bool && (bool)o)
            {
                return true;
            }

            return false;

        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(new
             {
                 Multiplication = new
                 {
                     MathExpress = MathExpress
                 }
             });
        }

        public override bool IsParse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            var match = Regex.Match(express, ParsePattern);

            if (match.Success && match.Length > 0)
            {
                return true;
            }

            return false;

        }
    }

    /// <summary>
    /// 匹配逻辑字符串的规则
    /// </summary>
    public class LogicRule : MatchRule
    {
        public string MatchPattern;
        public Fields Field;

        public LogicRule(Fields fields)
        {
            Field = fields;
        }

        public override string Parse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            var unit = fields.ContainsKey("unit") ? fields["unit"] : "";

            if (string.IsNullOrEmpty(unit))
            {
                MatchPattern = express.TrunIntegerUseLogic();
            }
            else
            {
                MatchPattern = express.TrunIntegerUseLogic(unit);
            }

            if (string.IsNullOrEmpty(MatchPattern))
            {
                MatchPattern = @"\S*";
            }

            MatchPattern = MatchPattern.TrunSymbles(".", "*").RetrunLogic().Replace(@"\\.", @"\.").Replace(@"\\*", @"\*");

            return express;
        }

        public override bool Match(Fields fields)
        {
            var sample = fields.ContainsKey("sample") ? fields["sample"] : "";
            var match = Regex.Match(sample, MatchPattern);

            return match.Success && match.Length > 0;
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(new
            {
                LogicRule = new
                {
                    MatchPattern = MatchPattern
                }
            });
        }

        public override bool IsParse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            var match = Regex.Match(express, @"[\W\w]+");

            if (express != "|")
            {
                if (match.Success && match.Length > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class Nonstring : MatchRule
    {
        private List<string> Keywords;
        private const string ParsePattern = @"\|\(\|\(非\)(?<non>[^\|\(\)]*)\)(?=\|)";

        public Nonstring()
        {
            Keywords = new List<string>();
        }

        public override string Parse(Fields fields)
        {
            var express = (fields.ContainsKey("express") ? fields["express"] : "")
                .TrunIntegerUseLogic();
            var match = Regex.Match(express, ParsePattern);

            if (match.Success && match.Length > 0)
            {
                Keywords.Add(match.Groups["non"].Value.TrunSymbles("*", "."));

                match = match.NextMatch();
            }

            return Regex.Replace(express, ParsePattern, new MatchEvaluator(delegate(Match m)
            {
                return "";
            }));
        }

        public override bool Match(Fields fields)
        {
            var sample = fields.ContainsKey("sample") ? fields["sample"] : "";

            foreach (var keyword in Keywords)
            {
                var match = Regex.Match(sample, keyword);
                if (match.Success == true && match.Length > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public override bool IsParse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            var m = Regex.Match(express, ParsePattern);

            return m.Success && m.Length > 0;
        }

        public override string ToJson()
        {


            return JsonConvert.SerializeObject(new
            {
                Nonstring = new
                {
                    Keywords = Keywords
                }
            });
        }
    }

    public class UnitRule : MatchRule
    {
        private const string ParsePattern_m = @"\|\(m\)(?=\|)";
        private const string ParsePattern_g = @"\|\(g\)(?=\|)";
        private string MatchPattern;

        public override string Parse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            var match = Regex.Match(express, ParsePattern_m);

            if (match.Success && match.Length > 0)
            {
                MatchPattern = @"[^mk]m([^m\[23]|$)";

                return Regex.Replace(express, ParsePattern_m, new MatchEvaluator(delegate(Match m)
                {
                    return "";
                }));
            }

            match = Regex.Match(express, ParsePattern_g);
            if (match.Success && match.Length > 0)
            {
                MatchPattern = "[^k]g";

                return Regex.Replace(express, ParsePattern_g, new MatchEvaluator(delegate(Match m)
                {
                    return "";
                }));
            }

            return express;
        }

        public override bool Match(Fields fields)
        {
            var sample = fields.ContainsKey("sample") ? fields["sample"] : "";

            if (string.IsNullOrEmpty(sample) == false)
            {
                var match = Regex.Match(sample, MatchPattern);
                if (match.Success && match.Length > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool IsParse(Fields fields)
        {
            var express = fields.ContainsKey("express") ? fields["express"] : "";
            if (!string.IsNullOrEmpty(express))
            {
                var match = Regex.Match(express, ParsePattern_m);
                if (match.Success && match.Length > 0)
                {
                    return true;
                }

                match = Regex.Match(express, ParsePattern_g);
                if (match.Success && match.Length > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(new
             {
                 UnitRule = new
                 {
                     MatchPattern = MatchPattern
                 }
             });
        }
    }

    public class Range<T>
    {
        public T Max { set; get; }
        public T Min { set; get; }

        public Range(T min, T max)
        {
            Max = max;
            Min = min;
        }
    }
}
