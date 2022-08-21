namespace _2022._08._15_PW_2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRunNotepad = new System.Windows.Forms.Button();
            this.buttonRunCalculator = new System.Windows.Forms.Button();
            this.buttonRunPaint = new System.Windows.Forms.Button();
            this.buttonRunSCSCalc = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRunNotepad
            // 
            this.buttonRunNotepad.Location = new System.Drawing.Point(12, 12);
            this.buttonRunNotepad.Name = "buttonRunNotepad";
            this.buttonRunNotepad.Size = new System.Drawing.Size(103, 23);
            this.buttonRunNotepad.TabIndex = 0;
            this.buttonRunNotepad.Text = "Run Notepad";
            this.buttonRunNotepad.UseVisualStyleBackColor = true;
            this.buttonRunNotepad.Click += new System.EventHandler(this.buttonRunNotepad_Click);
            // 
            // buttonRunCalculator
            // 
            this.buttonRunCalculator.Location = new System.Drawing.Point(12, 41);
            this.buttonRunCalculator.Name = "buttonRunCalculator";
            this.buttonRunCalculator.Size = new System.Drawing.Size(103, 23);
            this.buttonRunCalculator.TabIndex = 1;
            this.buttonRunCalculator.Text = "Run Calculator";
            this.buttonRunCalculator.UseVisualStyleBackColor = true;
            // 
            // buttonRunPaint
            // 
            this.buttonRunPaint.Location = new System.Drawing.Point(12, 70);
            this.buttonRunPaint.Name = "buttonRunPaint";
            this.buttonRunPaint.Size = new System.Drawing.Size(103, 23);
            this.buttonRunPaint.TabIndex = 2;
            this.buttonRunPaint.Text = "Run Paint";
            this.buttonRunPaint.UseVisualStyleBackColor = true;
            // 
            // buttonRunSCSCalc
            // 
            this.buttonRunSCSCalc.Location = new System.Drawing.Point(12, 99);
            this.buttonRunSCSCalc.Name = "buttonRunSCSCalc";
            this.buttonRunSCSCalc.Size = new System.Drawing.Size(103, 23);
            this.buttonRunSCSCalc.TabIndex = 3;
            this.buttonRunSCSCalc.Text = "Run SCS-Calc";
            this.buttonRunSCSCalc.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(121, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(667, 424);
            this.listBox1.TabIndex = 4;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 413);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(103, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Close";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.buttonRunSCSCalc);
            this.Controls.Add(this.buttonRunPaint);
            this.Controls.Add(this.buttonRunCalculator);
            this.Controls.Add(this.buttonRunNotepad);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button buttonRunNotepad;
        private Button buttonRunCalculator;
        private Button buttonRunPaint;
        private Button buttonRunSCSCalc;
        private ListBox listBox1;
        private Button button5;
    }
}