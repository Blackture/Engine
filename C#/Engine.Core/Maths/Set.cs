using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Set<T>
    {
        protected List<Interval> intervals;

        /// <summary>
        /// only used if for example it intersects with the natural numbers
        /// </summary>
        protected List<T> containingValues = null;

        public Set(params T[] values)
        {
            if (values != null)
            {
                containingValues = new List<T>(values);
            }
            if (typeof(T) == typeof(float)) intervals = new List<Interval>();
        }

        /// <summary>
        /// Calculating the cardinality of the interval
        /// </summary>
        /// <returns>Returns the cardinality. If the value is infinty it return -1 instead.</returns>
        public virtual int Cardinality()
        {
            if (containingValues == null) return -1;
            else
            {
                return containingValues.Count;
            }
        }

        public virtual bool Contains(T value)
        {
            return containingValues?.Contains(value) ?? false;
        }

        public void AddInterval(Interval interval)
        {
            if (typeof(T) == typeof(float))
                intervals.Add(interval);
            else Debug.Fail("This should be a float-set to add an interval");
        }
    }
}
