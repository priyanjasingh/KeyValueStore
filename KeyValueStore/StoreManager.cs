using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValueStore
{
    public class StoreManager
    {
        public static PrimaryStore primaryStore = new PrimaryStore();
        public static SecondaryStore secondaryStore = new SecondaryStore();

        public List<Attribute> attributes;
        public Attribute GetAttribute(string name)
        {
            return attributes.Where(at => at.name == name).First();
        }

        public void AddAttribute(Attribute at)
        {
            attributes.Add(at);
        }

        public bool SetInStore(string key, List<string> atts)
        {
            List<AttributeInstance> attributes = new List<AttributeInstance>();
            atts.ForEach(att => {
                string[] pair = att.Split(':');
                Attribute curr_attribute = GetAttribute(pair[0]);

                //New attribute
                if (curr_attribute == null)
                {
                    bool bool_value;
                    int int_value;
                    decimal dec_value;
                    if (Boolean.TryParse(pair[1], out bool_value))
                        attributes.Add(new AttributeInstance(new Attribute(pair[0],typeof(bool)), bool_value));
                    else if (int.TryParse(pair[1], out int_value))
                        attributes.Add(new AttributeInstance(new Attribute(pair[0], typeof(int)), int_value));
                    else if (decimal.TryParse(pair[1], out dec_value))
                        attributes.Add(new AttributeInstance(new Attribute(pair[0], typeof(decimal)), dec_value));
                    else
                        attributes.Add(new AttributeInstance(new Attribute(pair[0], typeof(string)), pair[1]));
                }
                else
                {
                    try
                    {
                        Convert.ChangeType(pair[1], curr_attribute.type);
                    }
                    catch
                    {
                        Console.WriteLine("not in expected datattype");
                    }
                    attributes.Add(new AttributeInstance(curr_attribute,pair[1]));
                }

            });
            

            primaryStore.Set(key, attributes);
            secondaryStore.Set(key, attributes);
            return true;
        }

        public void DeleteKeyFromStore(string key_input)
        {
            List<AttributeInstance> atts = primaryStore.Get(key_input);
            primaryStore.DeleteKey(key_input);
            secondaryStore.DeleteKey(atts,key_input);
        }

        public List<AttributeInstance> GetKeyValue(string key_input)
        {

            return primaryStore.Get(key_input);
        }

        public List<string> GetValueFromSecondaryIndex(string key)
        {  
            string[] pair = key.Split(':');
            Attribute curr_attribute = GetAttribute(pair[0]);

            return secondaryStore.Get(new AttributeInstance(curr_attribute,pair[1]));
        }

    }
    
}
