using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValueStore
{
    public class Attribute
    {
        public string name;
        public Type type;
        public dynamic value;

        public Attribute(string name, Type t, dynamic value)
        {
            this.name = name;
            this.value = value;
            this.type = t;
        }
    }
}
