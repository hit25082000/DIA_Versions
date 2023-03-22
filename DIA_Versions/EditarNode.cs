using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DIA_Versions
{
    public partial class EditarNode : Form
    {
        public string valorEditado { get; set; }

        public EditarNode(string node)
        {
            InitializeComponent();
            label2.Text = node;
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // This will make the form non-resizable
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            valorEditado = textBox1.Text;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
