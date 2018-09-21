using Dictionary.AVLTree;
using Dictionary.Writers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dictionary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AVLTree<string> avl = new AVLTree<string>();
            Writer writer = new FormWriter();

            avl.Add("Hello");
            avl.Add("It's");
            avl.Add("Tree");

            writer.Write(avl.DisplayTree());
        }
    }
}
