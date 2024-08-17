using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Events
{
    public class Event<T0, T1>
    {
        private delegate void Method(T0 t0, T1 t1);

        private Method collection;

        public void AddListener(Action<T0, T1> method)
        {
            Method m = new Method(method);
            collection += m;
        }
        public void RemoveListener(Action<T0, T1> method)
        {
            Method m = new Method(method);
            collection -= m;
        }
        public void RemoveAllListeners()
        {
            Delegate.RemoveAll(collection, collection);
        }

        public void Invoke(T0 t0, T1 t1)
        {
            collection?.Invoke(t0, t1);
        }

        public static Event<T0, T1> operator +(Event<T0, T1> e, Action<T0, T1> method)
        {
            e.AddListener(method);
            return e;
        }
        public static Event<T0, T1> operator -(Event<T0, T1> e, Action<T0, T1> method)
        {
            e.RemoveListener(method);
            return e;
        }

    }
}
