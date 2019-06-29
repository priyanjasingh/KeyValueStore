using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValueStore
{
    public interface IStore
    {
        bool Set(string key, List<AttributeInstance> atts);
        //List<T> Get<T,O>(O key);
        //bool DeleteKey<T>(T key);
    }
}
