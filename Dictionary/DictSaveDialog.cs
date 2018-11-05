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
    public partial class DictSaveDialog : Form
    {
        public static TypesOfStructures structure;
        
        public DictSaveDialog()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public static void Show()
        {
            new DictSaveDialog().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            structure = TypesOfStructures.AVLTree;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            structure = TypesOfStructures.HashTable;
            Close();
        }
    }
}
