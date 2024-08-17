using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class Interval : Set<float>
    {
        private IntervalType intervalType;
        private float lowerBoundary;
        private float upperBoundary;

        public bool ContainsUpperBoundary => Contains(upperBoundary);
        public bool ContainsLowerBoundary => Contains(lowerBoundary);
        public bool ContainsBoundaries => Type == IntervalType.Closed;

        public IntervalType Type { get => intervalType; set => intervalType = value; }

        public Interval(IntervalType intervalType, float lowerBoundary, float upperBoundary) : base(null)
        {
            Type = intervalType;
            this.lowerBoundary = lowerBoundary;
            this.upperBoundary = upperBoundary;
            AddInterval(this);
        }

        private bool contains(float f)
        {
            switch (Type)
            {
                case IntervalType.Open:
                    if (f > lowerBoundary && f < upperBoundary)
                        return true;
                    break;
                case IntervalType.LowerHalfOpen:
                    if (f > lowerBoundary && f <= upperBoundary)
                        return true;
                    break;
                case IntervalType.UpperHalfOpen:
                    if (f >= lowerBoundary && f < upperBoundary)
                        return true;
                    break;
                case IntervalType.Closed:
                    if (f >= lowerBoundary && f <= upperBoundary)
                        return true;
                    break;
            }
            return false;
        }

        public override bool Contains(float f)
        {
            foreach (Interval I in intervals)
            {
                if (I.contains(f))
                    return true;
            }
            return false;
        }

        public override int Cardinality()
        {
            return -1;
        }

        public Set<float> Union(Interval I2)
        {
            AddInterval(I2);
            return this;
        }

        public Set<float> Intersect(Interval I2)
        {
            bool[] uppers = new bool[2] { ContainsUpperBoundary, I2.ContainsUpperBoundary };
            bool[] lowers = new bool[2] { ContainsLowerBoundary, I2.ContainsLowerBoundary };

            //Information about Boundaries
            bool upper2Lower1 = uppers[1] && lowers[0];
            bool upper2NotLower1 = uppers[1] && !lowers[0];
            bool notUpper2Lower1 = !uppers[1] && lowers[0];

            //Boundary Comparisons
            bool upper2LtLower1 = I2.upperBoundary < lowerBoundary;
            bool lower2GtUpper1 = I2.lowerBoundary > upperBoundary;
            bool upper2LeqLower1 = I2.upperBoundary <= lowerBoundary;
            bool lower2GeqUpper1 = I2.lowerBoundary >= upperBoundary;

            //Check for intersection, if "true" there are none
            bool noIntersection = upper2Lower1 && (upper2LtLower1 || lower2GtUpper1)
                               || (upper2NotLower1 && upper2LeqLower1)
                               || (notUpper2Lower1 && lower2GeqUpper1);

            if (noIntersection)
            {
                intervals.Clear();
                if (containingValues == null)
                    containingValues = new List<float>();
                else
                    containingValues.Clear();
                return null;
            }
            else
            {
                float lowerBoundIntersection = Math.Max(lowerBoundary, I2.lowerBoundary);
                int indexLow = (lowerBoundIntersection == lowerBoundary) ? 0 : 1;

                float upperBoundIntersection = Math.Min(upperBoundary, I2.upperBoundary);
                int indexUp = (upperBoundIntersection == upperBoundary) ? 0 : 1;

                if (lowers[indexLow] && uppers[indexUp])
                {
                    Type = IntervalType.Closed;
                }
                else if (lowers[indexLow] && !uppers[indexUp])
                {
                    Type = IntervalType.UpperHalfOpen;
                }
                else if (!lowers[indexLow] && uppers[indexUp])
                {
                    Type = IntervalType.LowerHalfOpen;
                }
                else
                {
                    Type = IntervalType.Open;
                }

                lowerBoundary = lowerBoundIntersection;
                upperBoundary = upperBoundIntersection;

                if (lowerBoundIntersection == upperBoundIntersection)
                {
                    if (containingValues == null)
                    {
                        containingValues = new List<float>();
                    }
                    containingValues.Add(lowerBoundIntersection);
                }

                return this;
            }
        }

        
    }
}
