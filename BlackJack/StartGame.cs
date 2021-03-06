using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Prototyping_of_Project
{
    public partial class StartGame : Form{
        public long value { get; set; }
        public static long lastBet = 1;
        private long available;
        public StartGame(long available){
            InitializeComponent();
            this.available = available;
            if (available < lastBet)
                lastBet = available;
            if (available == 0)
                numericUpDown1.Maximum = 1;
            else
                numericUpDown1.Maximum = available;
            if (lastBet < 1)
                lastBet = Convert.ToInt64(1);
            numericUpDown1.Value = Convert.ToDecimal(lastBet);
            long tmp = available;
            List<long> ints = new List<long>();
            while (tmp > 0) {
                ints.Add(tmp % 1000);
                tmp /= 1000;
            }
            string xx = "";
            for (int i = ints.Count - 1; i >= 0; i--) {
                if (i == ints.Count - 1)
                    xx += ints[i].ToString();
                else
                    xx += ints[i].ToString("000");
                if (i != 0)
                    xx += ",";
            }
            if (xx == "")
                xx = "0";
            labelMax.Text = xx + "$";
        }

        //An event for when the Bet button is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            lastBet = Convert.ToInt64(numericUpDown1.Value);
            value = Convert.ToInt64(numericUpDown1.Value);
            if(available == 0)
            {
                MessageBox.Show("You do not have enough funds to play the game, deposit and come back again!");
                DialogResult = DialogResult.Cancel;
                return;
            }
            DialogResult = DialogResult.OK;
        }

        //An event for when the Cancel Bet button is clicked
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            lastBet = Convert.ToInt64(numericUpDown1.Value);
        }

        //A function that with a button click sets the value of the numericupdown to a quarter of what is available
        private void btnQuarter_Click(object sender, EventArgs e)
        {
            value = Convert.ToInt64(this.available * 0.25);
            if (value == 0)
            {
                value = 1;
            }
            numericUpDown1.Value = Convert.ToDecimal(value);
        }

        //A function that sets the value to half of what is available
        private void f(object sender, EventArgs e)
        {
            value = Convert.ToInt64(this.available * 0.5);
            if (value == 0)
            {
                value = 1;
            }
            numericUpDown1.Value = Convert.ToDecimal(value);
        }

        //A function that sets the value to max of what is available
        private void btnMax_Click(object sender, EventArgs e)
        {
            value = Convert.ToInt64(this.available);
            if (value == 0)
            {
                value = 1;
            }
            numericUpDown1.Value = Convert.ToDecimal(value);
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            lastBet = Convert.ToInt64(numericUpDown1.Value);
            value = Convert.ToInt64(numericUpDown1.Value);
            if (e.KeyCode == Keys.Enter)
                this.DialogResult = DialogResult.OK;
            if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }
    }
}
