using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Controls : Form
    {
        public Controls()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Controls_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }

        private void Controls_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
