using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValueStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing key value store");
            Store store = new Store();

            while (true)
            {
                Console.WriteLine("1. Add a key with attributes \n 2. Remove a key  \n 3. Fetch the key \n 4. Get all keys by attribute");

                int input = int.Parse(Console.ReadLine());
                string key_input;
                switch (input)
                {
                    case 1:
                        Console.WriteLine("Please insert key value");
                        string key = Console.ReadLine();
                        Console.WriteLine("Please insert number of attributes");
                        int no_atts = int.Parse(Console.ReadLine());
                        List<Attribute> atts = new List<Attribute>();
                        while (no_atts > 0)
                        {
                            Console.WriteLine("Please insert attribute in following format : attribute_name : attribute_value");
                            string attribute_input = Console.ReadLine();
                            string[] pair0 = attribute_input.Split(':');
                            string attribute_value = pair0[1];
                            // figure out if this is a new attribute ir existing
                            Attribute curr_attribute0 = store.GetAttribute(pair0[0]);

                            if (curr_attribute0 == null)
                            {
                                bool bool_value;
                                int int_value;
                                decimal dec_value;

                                if (Boolean.TryParse(pair0[1], out bool_value))
                                    atts.Add(new Attribute(pair0[0], typeof(Boolean), bool_value));
                                else if (int.TryParse(pair0[1], out int_value))
                                    atts.Add(new Attribute(pair0[0], typeof(Boolean), int_value));
                                else if (decimal.TryParse(pair0[1], out dec_value))
                                    atts.Add(new Attribute(pair0[0], typeof(Boolean), dec_value));
                                else
                                    atts.Add(new Attribute(pair0[0], typeof(string), pair0[1]));
                            }
                            else
                            {
                                try
                                {
                                    Convert.ChangeType(pair0[1], curr_attribute0.type);
                                }
                                catch
                                {
                                    Console.WriteLine("not in expected datattype");
                                }
                                curr_attribute0.value = pair0[1];
                                atts.Add(curr_attribute0);
                            }
                            store.Set(key,atts);
                            no_atts--;
                        }

                        break;
                    case 2:
                        Console.WriteLine("Enter key");

                        key_input = Console.ReadLine();
                        store.DeleteKey(key_input);
                        Console.WriteLine("key deleted");
                        break;

                    case 3:
                        Console.WriteLine("Enter key");
                        key_input = Console.ReadLine();
                        Console.WriteLine(JsonConvert.SerializeObject(store.Get(key_input)));
                        break;
                    case 4:
                        Console.WriteLine("Enter Attribute and value in format Att_name : Att_value");
                        key_input = Console.ReadLine();
                        string[] pair = key_input.Split(':');
                        Attribute curr_attribute = store.GetAttribute(pair[0], pair[1]);
                        Console.WriteLine(store.GetSecondaryIndex(curr_attribute));
                        break;
                    default: Console.Write("Please insert valid input"); break;

                }

            }
        }
    }
}
