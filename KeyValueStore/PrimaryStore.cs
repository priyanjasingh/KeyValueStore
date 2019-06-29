using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValueStore
{
    public class PrimaryStore : IStore
    {
        // handles concurrency

        ConcurrentDictionary<string, List<AttributeInstance>> keyValuePairs;



        public PrimaryStore()
        {
            keyValuePairs = new ConcurrentDictionary<string, List<AttributeInstance>>();
            keyValuePairs.TryAdd("delhi", new List<AttributeInstance>() { new AttributeInstance(new Attribute("pollution_level", typeof(string)), "very high") });
            keyValuePairs.TryAdd("jakarta", new List<AttributeInstance>() { new AttributeInstance(new Attribute("pollution_level", typeof(string)), "high") });
            //secondaryIndex[GetAttribute("pollution_level")].Add("jakarta");
           }


       
        // Sets value in the store
        // Populate both keyValuePairs and secondaryIndexs
        public bool Set(string key, List<AttributeInstance> atts)
        {
            try
            {
                if (keyValuePairs.ContainsKey(key))
                    keyValuePairs[key].AddRange(atts);
                else
                    keyValuePairs.TryAdd(key, atts);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }



        // delete all atts for a key
        public bool DeleteKey(string key)
        {
            List<AttributeInstance> value = null;
            while(!keyValuePairs.TryRemove(key,out value));
            return value == null;
        }

        public List<AttributeInstance> Get(string key)
        {
            return keyValuePairs[key];
        }
    }
}
