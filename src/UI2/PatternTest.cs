using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClaySharp;
using GoldSoft.Identiter.Common;

namespace Analysis
{
    public partial class PatternTest : Form
    {
        DB Database;

        public PatternTest()
        {
            InitializeComponent();
        }

        private void TextInput_TextChanged(object sender, EventArgs e)
        {

            //var fields = ContractExpress();
            //if (fields == null)
            //{
            //    return;
            //}

            //var pattern = Pattern.Parse(fields);
            //if (pattern != null)
            //{
            //    this.TextResult.Text = pattern.ToJson();
            //}
        }

        private void RegexTest_Load(object sender, EventArgs e)
        {

        }

        private void ControlRangeLeft_ValueChanged(object sender, EventArgs e)
        {
            //if (ControlRangeLeft.Value < ControlRangeRight.Value)
            //{
            //    TextRangePattern.Text = Pattern.GetNumberRangePattern((int)ControlRangeLeft.Value, (int)ControlRangeRight.Value);
            //}
        }

        private void ControlRangeRight_ValueChanged(object sender, EventArgs e)
        {
            if (ControlRangeLeft.Value < ControlRangeRight.Value)
            {
                //TextRangePattern.Text = Pattern.GetNumberRangePattern((int)ControlRangeLeft.Value, (int)ControlRangeRight.Value);
            }
        }

        private void ControlGreatValue_ValueChanged(object sender, EventArgs e)
        {
            //TextGreatPattern.Text = Pattern.GetNumberRangePattern((int)ControlGreatValue.Value, 99999);
        }

        private void ControlLessValue_ValueChanged(object sender, EventArgs e)
        {
            //TextLessPattern.Text = Pattern.GetLessEqualsPattern((int)ControlLessValue.Value);
        }

        private void TextValidContent_TextChanged(object sender, EventArgs e)
        {
            //var fields = ContractExpress();

            //if (fields != null)
            //{
                //var pattern = Pattern.Parse(fields);

                //if (pattern != null)
                //{
                //    fields["sample"] = GetFormatHeadingName();
                //    LabelExpressResult.Text = pattern.IsMatch(fields) ? "Success" : "Fail";

                //    return;
                //}
            //}

            //LabelExpressResult.Text = "";
        }

        public string GetFormatHeadingName()
        {
            var express = ContractExpress();
            var sample = ContractSample();

            if (express == null || sample == null)
            {
                return "";
            }

            var igrones = new string[] { };
            if (Database.Ignrones.ContainsKey(sample["profession"]))
            {
                igrones = Database.Ignrones[sample["profession"]].ToArray();
            }

            var result = sample["sample"].ClearItemNumber()
            .SubstringKeywords(express["left"], express["right"])
            .ReplaceKeywords(igrones, "")
            .ConvertUnit(Database.Convert);

            return result;
        }

        public Fields ContractSample()
        {
            if (string.IsNullOrEmpty(TextValidContent.Text))
            {
                return null;
            }

            var fields = new Fields();
            var lines = TextValidContent.Text.Split(new string[] { "###" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 0)
            {
                fields["sample"] = lines[0];
            }
            else
            {
                fields["sample"] = "";
            }

            if (lines.Length > 1)
            {
                fields["profession"] = lines[1];
            }
            else
            {
                fields["profession"] = "";
            }

            return fields;
        }

        public Fields ContractExpress()
        {

            if (string.IsNullOrEmpty(TextInput.Text))
            {
                return null;
            }

            var fields = new Fields();

            var lines = TextInput.Text.Split(new string[] { "###" }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 0)
            {
                fields["express"] = lines[0];
            }
            else
            {
                fields["express"] = "";
            }

            if (lines.Length > 1)
            {
                fields["unit"] = lines[1];
            }
            else
            {
                fields["unit"] = "";
            }

            if (lines.Length > 2)
            {
                fields["left"] = lines[2];
            }
            else
            {
                fields["left"] = "";
            }

            if (lines.Length > 3)
            {
                fields["right"] = lines[3];
            }
            else
            {
                fields["right"] = "";
            }

            return fields;
        }

        private void TextDB_Click(object sender, EventArgs e)
        {
            ShowFile.ShowDialog();
        }

        private void ShowFile_FileOk(object sender, CancelEventArgs e)
        {
            TextDB.Text = ShowFile.FileName;
            Database = new DB(ShowFile.FileName);
        }

        private void PageLogic_Click(object sender, EventArgs e)
        {

        }
    }
}
