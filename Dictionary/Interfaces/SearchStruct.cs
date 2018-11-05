using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Interfaces
{
    public interface SearchStruct<T>
    {
        void Add(T data, int reference);
        int? Find(T key);
    }
}
