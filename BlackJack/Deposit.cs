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

        private void Deposit_Load(object sender, EventArgs e)
        {

        }

        private void Deposit_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Deposit_KeyDown(object sender, KeyEventArgs e)
        {
            credits = Convert.ToInt32(numericUpDown1.Value);
            if (e.KeyCode == Keys.Enter)
                DialogResult = DialogResult.Yes;
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.No;
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            credits = Convert.ToInt32(numericUpDown1.Value);
            if (e.KeyCode == Keys.Enter)
                this.DialogResult = DialogResult.Yes;
            if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.No;
        }
    }
}
