using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValueStore
{
    public class Store
    {
        // Inverse mapping 

        Dictionary<string, List<Attribute>> keyValuePairs;

        Dictionary<Attribute, List<string>> secondaryIndex;

        public Store()
        {
            keyValuePairs = new Dictionary<string, List<Attribute>>();
            secondaryIndex = new Dictionary<Attribute, List<string>>();
            keyValuePairs.Add("delhi", new List<Attribute>() { new Attribute("pollution_level", typeof(string), "very high") });
            secondaryIndex.Add(new Attribute("pollution_level", typeof(string), "very high"), new List<string>() { "delhi" });
            keyValuePairs.Add("jakarta", new List<Attribute>() { new Attribute("pollution_level", typeof(string), "high") });
            //secondaryIndex[GetAttribute("pollution_level")].Add("jakarta");
            secondaryIndex.Add(new Attribute("pollution_level", typeof(string), "high"), new List<string>() { "jakarta" });


        }


        public Attribute GetAttribute(string name)
        {
            if (secondaryIndex.Keys.ToList().Where(att => att.name == name).Any())
                return secondaryIndex.Keys.ToList().Where(att => att.name == name).First();
            return null;
        }

        public Attribute GetAttribute(string name,string value)
        {
            if (secondaryIndex.Keys.ToList().Where(att => att.name == name && att.value == value).Any())
                return secondaryIndex.Keys.ToList().Where(att => att.name == name && att.value == value).First();
            return null;
        }
        // Sets value in the store
        // Populate both keyValuePairs and secondaryIndexs
        public bool Set(string key, List<Attribute> atts)
        {
            try
            {
                if (keyValuePairs.ContainsKey(key))
                    keyValuePairs[key].AddRange(atts);
                else
                    keyValuePairs.Add(key, atts);

                atts.ForEach(att =>
                {
                    if (secondaryIndex.ContainsKey(att))
                        secondaryIndex[att].Add(key);
                    else
                        secondaryIndex.Add(att, new List<string>() { key});
                });
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }

        // Get list of attributes for a key
        public List<Attribute> Get(string key)
        {
            return keyValuePairs[key];
        }

        // delete all atts for a key
        public bool DeleteKey(string key)
        {
            List<Attribute> atts = keyValuePairs[key];
            keyValuePairs.Remove(key);
            atts.ForEach(att =>
            {
                //we are assuming that attribute once initialized will never change.
                secondaryIndex[att].Remove(key);
            });
            return true;
        }
        public List<string> GetSecondaryIndex(Attribute at)
        {
            return secondaryIndex[at];
        }



    }
}
