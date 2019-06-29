using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValueStore
{
    public class SecondaryStore : IStore
    {
        //handles concurrency
        public ConcurrentDictionary<AttributeInstance, List<string>> secondaryIndex;
        
        public SecondaryStore()
        {
            secondaryIndex = new ConcurrentDictionary<AttributeInstance, List<string>>();
            secondaryIndex.TryAdd(new AttributeInstance(new Attribute("pollution_level", typeof(string)), "very high"), new List<string>() { "delhi" });
            //secondaryIndex[GetAttribute("pollution_level")].Add("jakarta");
            secondaryIndex.TryAdd(new AttributeInstance(new Attribute("pollution_level", typeof(string)), "high"), new List<string>() { "jakarta" });
        }

        public bool DeleteKey(List<AttributeInstance> atts,string key)
        {
            atts.ForEach(att =>
            {
                //we are assuming that attribute once initialized will never change.
                secondaryIndex[att].Remove(key);
            });
            return true;
        }

        public List<string> Get(AttributeInstance key)
        {
            return secondaryIndex[key];
        }

   

        public bool Set(string key, List<AttributeInstance> atts)
        {
            atts.ForEach(att =>
            {
                if (secondaryIndex.ContainsKey(att))
                    secondaryIndex[att].Add(key);
                else
                    secondaryIndex.TryAdd(att, new List<string>() { key });
            });
            return true;
        }
    }
}
