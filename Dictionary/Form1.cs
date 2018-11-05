using Dictionary.AVLTree;
using Dictionary.HashTable;
using Dictionary.Interfaces;
using Dictionary.Writers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dictionary
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofd;
        SaveFileDialog sfd;
        string structType;
        SearchStruct<string> structure;
        string currentPath;

        public void Test()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("Abba", 1);
            dict.Add("Deep", 2);
            Console.WriteLine(dict["Abba"]);
        }
        string words;
        List<string> definitions;
        public Form1()
        {
            InitializeComponent();
            definitions = new List<string>();
            structType = "AVLTree";
            ofd = new OpenFileDialog
            {
                Filter = "Dictionary file|*.dictionary",
                Title = "Add dictionary file"
            };

            sfd = new SaveFileDialog()
            {
                Filter = "Dictionary file|*.dictionary",
                Title = "Add dictionary file"
            };
        }

        public SearchStruct<string> handler()
        {
            if ("AVLTree".Equals(structType))
                return new AVLTree<string>();
            else
                return new HashTable<string>();
        }

        public SearchStruct<string> Deserealize(string word)
        {
            if ("AVLTree".Equals(structType))
                return JsonConvert.DeserializeObject<AVLTree<string>>(words);
            else
                return JsonConvert.DeserializeObject<HashTable<string>>(words);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Writer writer = new FormWriter();
            writer.Write(structure.ToString(), this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Stream fs = sfd.OpenFile();
                StreamWriter sw = new StreamWriter(fs);
                DictSaveDialog.Show();
                sw.Write($"{DictSaveDialog.structure.ToString()}\\{{}}");
                File.Create($"{sfd.FileName}.definitions");
                sw.Close();
                fs.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentPath = ofd.FileName;
                Text = $"Словарик {currentPath.Split('\\')[currentPath.Split('\\').Length - 1]}";
                var allText = File.ReadAllText(ofd.FileName);
                words = allText.Split('\\')[1];
                structType = allText.Split('\\')[0];
                definitions.AddRange(File.ReadAllLines($"{ofd.FileName}.definitions"));
                try
                {
                    structure = Deserealize(words); //AVLTree<string>
                    if (structure != null) fillTreeView(structure.ToString(), Text);
                    button4.Enabled = true;
                    button5.Enabled = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Неверный тип структуры данных");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.All
            };
            int newRef = File.ReadAllText($"{currentPath}.definitions").Split('\n').Length;
            structure.Add(textBox1.Text, newRef);
            string json = JsonConvert.SerializeObject(structure, jsonSerializerSettings);
            File.WriteAllText($"{currentPath}", $"{structType}\\{json}");
            File.AppendAllText($"{currentPath}.definitions", $"{richTextBox2.Text}\n");

            definitions.Add(richTextBox2.Text);
            MessageBox.Show($"Определение слова {textBox1.Text} добавлено в словарь {currentPath.Split('\\')[currentPath.Split('\\').Length - 1]}", "Успех!");
            textBox1.Clear();
            richTextBox2.Clear();
            fillTreeView(structure.ToString(), Text);
        }

        private void findAndImageWord(string find)
        {
            var reference = structure.Find(find);
            if (reference != null)
                MessageBox.Show($"{find} цэ {definitions[(int)reference - 1]}", "Определение");
            else
                MessageBox.Show($"Слово не найдено", "Определение");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            findAndImageWord(textBox2.Text);
        }

        private void fillTreeView(string data, string name)
        {
            treeView1.BeginUpdate();
            if (treeView1.Nodes.ContainsKey(name))
            {
                treeView1.Nodes[name].Nodes.Clear();
            }
            else
            {
                treeView1.Nodes.Add(name, name);
            }
            foreach (var word in data.Split('\n'))
                if (word != "")
                    treeView1.Nodes[name].Nodes.Add(word);
            treeView1.EndUpdate();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeViewHitTestInfo info = treeView1.HitTest(treeView1.PointToClient(Cursor.Position));
            if (info != null)
            {
                findAndImageWord(info.Node.Text);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            structType = "HashTable";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            structType = "AVLTree";
        }
    }
}
