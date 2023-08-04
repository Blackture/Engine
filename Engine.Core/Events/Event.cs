using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Events
{
    public class Event
    {
        private delegate void Method();

        private Method collection;

        public void AddListener(Action method)
        {
            Method m = new Method(method);
            collection += m;
        }
        public void RemoveListener(Action method)
        {
            Method m = new Method(method);
            collection -= m;
        }
        public void RemoveAllListeners()
        {
            Delegate.RemoveAll(collection, collection);
        }

        public void Invoke()
        {
            collection?.Invoke();
        }

        public static Event operator +(Event e, Action method)
        {
            e.AddListener(method);
            return e;
        }
        public static Event operator -(Event e, Action method)
        {
            e.RemoveListener(method);
            return e;
        }
    }
}
