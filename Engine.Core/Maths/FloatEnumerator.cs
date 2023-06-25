using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class FloatEnumerator : IEnumerator<float>, IDisposable
    {
        private float[] data;
        private int enumPos = -1;
        private bool disposed;

        public float Current
        {
            get
            {
                try
                {
                    return data[enumPos];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public FloatEnumerator(float[] data)
        {
            this.data = data;
        }

        public bool MoveNext()
        {
            enumPos++;
            return enumPos < data.Length;
        }

        public void Reset()
        {
            enumPos = -1;
        }

        // Protected Dispose method to release resources
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose of any managed resources here
                }

                // Dispose of any unmanaged resources here

                disposed = true;
            }
        }

        // Implement the Dispose method of IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Finalizer
        ~FloatEnumerator()
        {
            Dispose(false);
        }
    }
}
