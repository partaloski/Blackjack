﻿
namespace Prototyping_of_Project
{
    partial class StartGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartGame));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelMax = new System.Windows.Forms.Label();
            this.btnHalf = new System.Windows.Forms.Button();
            this.btnQuarter = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Bowlby One SC", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(391, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Bet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Bowlby One SC", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(391, 111);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(194, 40);
            this.button2.TabIndex = 0;
            this.button2.Text = "Cancel Bet";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(12, 12);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(573, 27);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bowlby One SC", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max amount to bet:";
            // 
            // labelMax
            // 
            this.labelMax.AutoSize = true;
            this.labelMax.BackColor = System.Drawing.Color.Transparent;
            this.labelMax.Font = new System.Drawing.Font("Bowlby One SC", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelMax.ForeColor = System.Drawing.Color.IndianRed;
            this.labelMax.Location = new System.Drawing.Point(51, 83);
            this.labelMax.Name = "labelMax";
            this.labelMax.Size = new System.Drawing.Size(108, 38);
            this.labelMax.TabIndex = 3;
            this.labelMax.Text = "label2";
            // 
            // btnHalf
            // 
            this.btnHalf.Font = new System.Drawing.Font("Bowlby One SC", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnHalf.Location = new System.Drawing.Point(391, 203);
            this.btnHalf.Name = "btnHalf";
            this.btnHalf.Size = new System.Drawing.Size(194, 40);
            this.btnHalf.TabIndex = 0;
            this.btnHalf.Text = "Bet Half";
            this.btnHalf.UseVisualStyleBackColor = true;
            this.btnHalf.Click += new System.EventHandler(this.f);
            // 
            // btnQuarter
            // 
            this.btnQuarter.Font = new System.Drawing.Font("Bowlby One SC", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnQuarter.Location = new System.Drawing.Point(391, 249);
            this.btnQuarter.Name = "btnQuarter";
            this.btnQuarter.Size = new System.Drawing.Size(194, 40);
            this.btnQuarter.TabIndex = 0;
            this.btnQuarter.Text = "Bet Quarter";
            this.btnQuarter.UseVisualStyleBackColor = true;
            this.btnQuarter.Click += new System.EventHandler(this.btnQuarter_Click);
            // 
            // btnMax
            // 
            this.btnMax.Font = new System.Drawing.Font("Bowlby One SC", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnMax.Location = new System.Drawing.Point(391, 157);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(194, 40);
            this.btnMax.TabIndex = 0;
            this.btnMax.Text = "Bet Max";
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // StartGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(597, 392);
            this.Controls.Add(this.labelMax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btnMax);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnQuarter);
            this.Controls.Add(this.btnHalf);
            this.Controls.Add(this.button1);
            this.Name = "StartGame";
            this.Text = "StartGame";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMax;
        private System.Windows.Forms.Button btnHalf;
        private System.Windows.Forms.Button btnQuarter;
        private System.Windows.Forms.Button btnMax;
    }
}