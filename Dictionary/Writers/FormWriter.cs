using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dictionary.Writers
{
    class FormWriter : Writer
    {
        public override void Write(string data, Form1 handler) { }//handler.Invoke(new Action(()=> { handler.richTextBox1.Text = data; }));
    }
}