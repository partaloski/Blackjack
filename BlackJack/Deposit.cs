using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Prototyping_of_Project
{
    public partial class Deposit : Form
    {
        public int credits { get; set; }
        public Deposit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            credits = Convert.ToInt32(numericUpDown1.Value);
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }
    }
}
