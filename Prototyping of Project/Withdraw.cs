using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Prototyping_of_Project
{
    public partial class Withdraw : Form
    {
        public long withdraw_amount_max { get; set; }
        public long withdraw_amount { get; set; }
        public Withdraw(long amount)
        {
            InitializeComponent();
            long tmp = amount;
            List<long> ints = new List<long>();
            while (tmp > 0)
            {
                ints.Add(tmp % 1000);
                tmp /= 1000;
            }
            string xx = "";
            for (int i = ints.Count - 1; i >= 0; i--)
            {
                if (i == ints.Count - 1)
                    xx += ints[i].ToString();
                else
                    xx += ints[i].ToString("000");
                if (i != 0)
                    xx += ",";
            }
            if (xx == "")
                xx = "0";
            lblMax.Text = xx + "$";
            withdraw_amount_max = amount;
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = amount;
            numericUpDown1.Value = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {//Confirm
            withdraw_amount = Convert.ToInt64(numericUpDown1.Value);
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {//Cancel

            DialogResult = DialogResult.No;
        }

        private void button5_Click(object sender, EventArgs e)
        {// MAX
            numericUpDown1.Value = Convert.ToInt64(withdraw_amount_max);
        }

        private void button4_Click(object sender, EventArgs e)
        {// 1/2
            numericUpDown1.Value = Convert.ToInt64(withdraw_amount_max * 0.5);
        }

        private void button3_Click(object sender, EventArgs e)
        {// 1/4
            numericUpDown1.Value = Convert.ToInt64(withdraw_amount_max * 0.25);
        }
    }
}
