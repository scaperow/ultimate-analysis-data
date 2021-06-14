namespace GoldSoft.Identiter.UI
{
    partial class PatternTest
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
            this.TextResult = new System.Windows.Forms.TextBox();
            this.TextInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ControlRangeLeft = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ControlRangeRight = new System.Windows.Forms.NumericUpDown();
            this.TextRangePattern = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ControlGreatValue = new System.Windows.Forms.NumericUpDown();
            this.TextGreatPattern = new System.Windows.Forms.TextBox();
            this.ControlLessValue = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.TextLessPattern = new System.Windows.Forms.TextBox();
            this.TabContainer = new System.Windows.Forms.TabControl();
            this.PageLogic = new System.Windows.Forms.TabPage();
            this.TextDB = new System.Windows.Forms.TextBox();
            this.LabelExpressResult = new System.Windows.Forms.Label();
            this.TextValidContent = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PageNumber = new System.Windows.Forms.TabPage();
            this.ShowFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ControlRangeLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlRangeRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlGreatValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlLessValue)).BeginInit();
            this.TabContainer.SuspendLayout();
            this.PageLogic.SuspendLayout();
            this.PageNumber.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextResult
            // 
            this.TextResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextResult.Location = new System.Drawing.Point(124, 276);
            this.TextResult.Multiline = true;
            this.TextResult.Name = "TextResult";
            this.TextResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextResult.Size = new System.Drawing.Size(342, 150);
            this.TextResult.TabIndex = 0;
            // 
            // TextInput
            // 
            this.TextInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextInput.Location = new System.Drawing.Point(124, 33);
            this.TextInput.Multiline = true;
            this.TextInput.Name = "TextInput";
            this.TextInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextInput.Size = new System.Drawing.Size(342, 237);
            this.TextInput.TabIndex = 1;
            this.TextInput.TextChanged += new System.EventHandler(this.TextInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 104);
            this.label1.TabIndex = 2;
            this.label1.Text = "Express to parse\r\n(split as ###)\r\n\r\nline 1 : express\r\nline 2 : unit filter\r\nline " +
    "3 : substring left\r\nline 4 : substring right\r\n\r\n";
            // 
            // ControlRangeLeft
            // 
            this.ControlRangeLeft.Location = new System.Drawing.Point(8, 42);
            this.ControlRangeLeft.Name = "ControlRangeLeft";
            this.ControlRangeLeft.Size = new System.Drawing.Size(120, 20);
            this.ControlRangeLeft.TabIndex = 3;
            this.ControlRangeLeft.ValueChanged += new System.EventHandler(this.ControlRangeLeft_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Match a range of Number type";
            // 
            // ControlRangeRight
            // 
            this.ControlRangeRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlRangeRight.Location = new System.Drawing.Point(312, 42);
            this.ControlRangeRight.Name = "ControlRangeRight";
            this.ControlRangeRight.Size = new System.Drawing.Size(148, 20);
            this.ControlRangeRight.TabIndex = 3;
            this.ControlRangeRight.ValueChanged += new System.EventHandler(this.ControlRangeRight_ValueChanged);
            // 
            // TextRangePattern
            // 
            this.TextRangePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextRangePattern.Location = new System.Drawing.Point(8, 68);
            this.TextRangePattern.Multiline = true;
            this.TextRangePattern.Name = "TextRangePattern";
            this.TextRangePattern.Size = new System.Drawing.Size(452, 44);
            this.TextRangePattern.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Match a pattern that great than above number";
            // 
            // ControlGreatValue
            // 
            this.ControlGreatValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlGreatValue.Location = new System.Drawing.Point(8, 161);
            this.ControlGreatValue.Name = "ControlGreatValue";
            this.ControlGreatValue.Size = new System.Drawing.Size(120, 20);
            this.ControlGreatValue.TabIndex = 3;
            this.ControlGreatValue.ValueChanged += new System.EventHandler(this.ControlGreatValue_ValueChanged);
            // 
            // TextGreatPattern
            // 
            this.TextGreatPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextGreatPattern.Location = new System.Drawing.Point(8, 187);
            this.TextGreatPattern.Multiline = true;
            this.TextGreatPattern.Name = "TextGreatPattern";
            this.TextGreatPattern.Size = new System.Drawing.Size(452, 50);
            this.TextGreatPattern.TabIndex = 0;
            // 
            // ControlLessValue
            // 
            this.ControlLessValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlLessValue.Location = new System.Drawing.Point(12, 285);
            this.ControlLessValue.Name = "ControlLessValue";
            this.ControlLessValue.Size = new System.Drawing.Size(120, 20);
            this.ControlLessValue.TabIndex = 6;
            this.ControlLessValue.ValueChanged += new System.EventHandler(this.ControlLessValue_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(219, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Match a pattern that less than above number";
            // 
            // TextLessPattern
            // 
            this.TextLessPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextLessPattern.Location = new System.Drawing.Point(12, 311);
            this.TextLessPattern.Multiline = true;
            this.TextLessPattern.Name = "TextLessPattern";
            this.TextLessPattern.Size = new System.Drawing.Size(452, 50);
            this.TextLessPattern.TabIndex = 4;
            // 
            // Container
            // 
            this.TabContainer.Controls.Add(this.PageLogic);
            this.TabContainer.Controls.Add(this.PageNumber);
            this.TabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabContainer.Location = new System.Drawing.Point(0, 0);
            this.TabContainer.Name = "Container";
            this.TabContainer.SelectedIndex = 0;
            this.TabContainer.Size = new System.Drawing.Size(482, 600);
            this.TabContainer.TabIndex = 7;
            // 
            // PageLogic
            // 
            this.PageLogic.Controls.Add(this.TextDB);
            this.PageLogic.Controls.Add(this.LabelExpressResult);
            this.PageLogic.Controls.Add(this.TextValidContent);
            this.PageLogic.Controls.Add(this.label7);
            this.PageLogic.Controls.Add(this.label6);
            this.PageLogic.Controls.Add(this.TextInput);
            this.PageLogic.Controls.Add(this.label5);
            this.PageLogic.Controls.Add(this.label1);
            this.PageLogic.Controls.Add(this.TextResult);
            this.PageLogic.Location = new System.Drawing.Point(4, 22);
            this.PageLogic.Name = "PageLogic";
            this.PageLogic.Padding = new System.Windows.Forms.Padding(3);
            this.PageLogic.Size = new System.Drawing.Size(474, 574);
            this.PageLogic.TabIndex = 0;
            this.PageLogic.Text = "Logic";
            this.PageLogic.UseVisualStyleBackColor = true;
            this.PageLogic.Click += new System.EventHandler(this.PageLogic_Click);
            // 
            // TextDB
            // 
            this.TextDB.Location = new System.Drawing.Point(124, 7);
            this.TextDB.Name = "TextDB";
            this.TextDB.ReadOnly = true;
            this.TextDB.Size = new System.Drawing.Size(342, 20);
            this.TextDB.TabIndex = 6;
            this.TextDB.Click += new System.EventHandler(this.TextDB_Click);
            // 
            // LabelExpressResult
            // 
            this.LabelExpressResult.AutoSize = true;
            this.LabelExpressResult.Location = new System.Drawing.Point(8, 536);
            this.LabelExpressResult.Name = "LabelExpressResult";
            this.LabelExpressResult.Size = new System.Drawing.Size(32, 13);
            this.LabelExpressResult.TabIndex = 5;
            this.LabelExpressResult.Text = "State";
            // 
            // TextValidContent
            // 
            this.TextValidContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextValidContent.Location = new System.Drawing.Point(124, 432);
            this.TextValidContent.Multiline = true;
            this.TextValidContent.Name = "TextValidContent";
            this.TextValidContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextValidContent.Size = new System.Drawing.Size(342, 134);
            this.TextValidContent.TabIndex = 3;
            this.TextValidContent.TextChanged += new System.EventHandler(this.TextValidContent_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "DB";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 461);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 65);
            this.label6.TabIndex = 4;
            this.label6.Text = "Content for valid\r\n(split as ###)\r\n\r\nline 1 : sample\r\nline 2 : profession";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Parsed pattern";
            // 
            // PageNumber
            // 
            this.PageNumber.Controls.Add(this.label2);
            this.PageNumber.Controls.Add(this.ControlLessValue);
            this.PageNumber.Controls.Add(this.TextRangePattern);
            this.PageNumber.Controls.Add(this.label4);
            this.PageNumber.Controls.Add(this.TextGreatPattern);
            this.PageNumber.Controls.Add(this.TextLessPattern);
            this.PageNumber.Controls.Add(this.label3);
            this.PageNumber.Controls.Add(this.ControlRangeRight);
            this.PageNumber.Controls.Add(this.ControlRangeLeft);
            this.PageNumber.Controls.Add(this.ControlGreatValue);
            this.PageNumber.Location = new System.Drawing.Point(4, 22);
            this.PageNumber.Name = "PageNumber";
            this.PageNumber.Padding = new System.Windows.Forms.Padding(3);
            this.PageNumber.Size = new System.Drawing.Size(474, 574);
            this.PageNumber.TabIndex = 1;
            this.PageNumber.Text = "Number";
            this.PageNumber.UseVisualStyleBackColor = true;
            // 
            // ShowFile
            // 
            this.ShowFile.FileName = "openFileDialog1";
            this.ShowFile.FileOk += new System.ComponentModel.CancelEventHandler(this.ShowFile_FileOk);
            // 
            // PatternTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 600);
            this.Controls.Add(this.TabContainer);
            this.Name = "PatternTest";
            this.Text = "RegexTest";
            this.Load += new System.EventHandler(this.RegexTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ControlRangeLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlRangeRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlGreatValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ControlLessValue)).EndInit();
            this.TabContainer.ResumeLayout(false);
            this.PageLogic.ResumeLayout(false);
            this.PageLogic.PerformLayout();
            this.PageNumber.ResumeLayout(false);
            this.PageNumber.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TextResult;
        private System.Windows.Forms.TextBox TextInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ControlRangeLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ControlRangeRight;
        private System.Windows.Forms.TextBox TextRangePattern;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ControlGreatValue;
        private System.Windows.Forms.TextBox TextGreatPattern;
        private System.Windows.Forms.NumericUpDown ControlLessValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextLessPattern;
        private System.Windows.Forms.TabControl TabContainer;
        private System.Windows.Forms.TabPage PageLogic;
        private System.Windows.Forms.TabPage PageNumber;
        private System.Windows.Forms.Label LabelExpressResult;
        private System.Windows.Forms.TextBox TextValidContent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextDB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog ShowFile;
    }
}