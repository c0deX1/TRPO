using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dictionary.Writers
{
    abstract class Writer
    {
        public abstract void Write(string data); 
    }
}