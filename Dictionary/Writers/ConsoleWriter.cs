using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dictionary.Writers
{
    class ConsoleWriter : Writer
    {
        public override void Write(string data, Form1 handler) => Console.Write(data);
    }
}