using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dictionary.Writers
{
    class ConsoleWriter : Writer
    {
        public override void Write(string data) => Console.Write(data);
    }
}