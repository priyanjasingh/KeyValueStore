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
            StoreManager store = new StoreManager();

            while (true)
            {
                try
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
                            List<string> atts = new List<string>();
                            while (no_atts > 0)
                            {
                                Console.WriteLine("Please insert attribute in following format : attribute_name : attribute_value");
                                atts.Add(Console.ReadLine());
                                no_atts--;
                            }
                            store.SetInStore(key,atts);
                            break;
                        case 2:
                            Console.WriteLine("Enter key");
                            key_input = Console.ReadLine();
                            store.DeleteKeyFromStore(key_input);
                            Console.WriteLine("key deleted");
                            break;

                        case 3:
                            Console.WriteLine("Enter key");
                            key_input = Console.ReadLine();
                            List<AttributeInstance> res = store.GetKeyValue(key_input);
                            res.ForEach(r => Console.WriteLine(r.attribute.name + "" + r.value));
                            break;
                        case 4:
                            Console.WriteLine("Enter Attribute and value in format Att_name : Att_value");
                            //key_input = Console.ReadLine();
                            List<string> keys = store.GetValueFromSecondaryIndex(Console.ReadLine());
                            keys.ForEach(k => Console.WriteLine(k));
                            break;
                        default: Console.Write("Please insert valid input"); break;

                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Some error occured. please check your input.");
                }
            }
        }
    }
}
