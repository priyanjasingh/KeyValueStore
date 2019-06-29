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

        public Attribute(string name, Type t)
        {
            this.name = name;
            this.type = t;
        }
        public override bool Equals(object obj)
        {
            Attribute that = (Attribute)obj;
            return this.name == that.name;
        }
    }

    public class AttributeInstance
    {
        public Attribute attribute;
        public dynamic value;
        public AttributeInstance(Attribute attribute, dynamic value)
        {
            this.attribute = attribute;
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            AttributeInstance that = (AttributeInstance)obj;
            return this.value == that.value && this.attribute == that.attribute;
        }
    }
}
