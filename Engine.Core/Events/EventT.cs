using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Events
{
    public class Event<T>
    {
        private delegate void Method(T t);

        private Method collection;

        public void AddListener(Action<T> method)
        {
            Method m = new Method(method);
            collection += m;
        }
        public void RemoveListener(Action<T> method)
        {
            Method m = new Method(method);
            collection -= m;
        }
        public void RemoveAllListeners()
        {
            Delegate.RemoveAll(collection, collection);
        }

        public void Invoke(T t)
        {
            collection?.Invoke(t);
        }

        public static Event<T> operator +(Event<T> e, Action<T> method)
        {
            e.AddListener(method);
            return e;
        }
        public static Event<T> operator -(Event<T> e, Action<T> method)
        {
            e.RemoveListener(method);
            return e;
        }
    }
}
