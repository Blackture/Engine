using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths;

namespace Engine.Core.Components
{
    //Local Coordinate System
    public class LCS3 : Component, IEnumerable<LCS3> ,IEnumerator<LCS3>, IDisposable
    {
        public readonly Straight3D[] axis = new Straight3D[3] { Straight3D.x1_Axis, Straight3D.x2_Axis, Straight3D.x3_Axis };

        public LCS3 Current
        {
            get
            {
                try
                {
                    return children[enumPos];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;

        public Vector3 GlobalPosition { get => globalPosition; set => globalPosition = value; }
        public RotationVector3 GlobalRotation { get => globalRotation; set => globalRotation = value; }
        public Vector3 GlobalScale { get => globalScale; set => globalScale = value; }
        public Vector3 LocalPosition { get => localPosition; set => localPosition = value; }
        public RotationVector3 LocalRotation { get => localRotation; set => localRotation = value; }
        public Vector3 LocalScale { get => localScale; set => localScale = value; }
        /// <summary>
        /// Is true if the LCS has at least one child Lcs
        /// </summary>
        public bool HasChild { get => hasChild; }
        /// <summary>
        /// The higher the value the more parents it has, starting from 0: no parents
        /// </summary>
        public LCS3 Parent { get => Dependency.Parent?.Local; }
        public int ChildCount { get => children.Count; }

        private Vector3 globalPosition;
        private RotationVector3 globalRotation;
        private Vector3 globalScale;
        private Vector3 localPosition;
        private RotationVector3 localRotation;
        private Vector3 localScale;
        private bool hasChild;

        protected List<LCS3> children;
        private int enumPos = -1;
        private bool disposed;

        public LCS3(Object3D dependency) : base(dependency)
        {
            if (dependency.Parent != null)
            {
                dependency.Parent.Local.children.Add(this);
                dependency.Parent.Local.children = dependency.Parent.Local.GetChilds();
            }
            children = GetChilds();
            hasChild = children.Count > 0;
        }

        public List<LCS3> GetChilds()
        {
            List<LCS3> children = new List<LCS3>();
            foreach (LCS3 child in children)
            {
                children.Add(child);
                if (child.children.Count != 0)
                {
                    children.AddRange(child.GetChilds());
                }
            }
            return children;
        }

        public Vector3[] AxisToGlobalUnitVectors()
        {
            Vector3[] localToGlobal = null;
            bool res = true;
            if ((GCS3.Axes[0].Dir.ToMatrix() * new RotationMatrix3x3(globalRotation.X1, globalRotation.X2, globalRotation.X3)).ToVector3(out Vector3 x1))
            {
                if ((GCS3.Axes[1].Dir.ToMatrix() * new RotationMatrix3x3(globalRotation.X1, globalRotation.X2, globalRotation.X3)).ToVector3(out Vector3 x2))
                {
                    if ((GCS3.Axes[2].Dir.ToMatrix() * new RotationMatrix3x3(globalRotation.X1, globalRotation.X2, globalRotation.X3)).ToVector3(out Vector3 x3))
                    {
                        localToGlobal = new Vector3[3] { x1.Normalized, x2.Normalized, x3.Normalized };
                    }
                    else res = false;
                }
                else res = false;
            }
            else res = false;
            if (res)
            {
                return localToGlobal;
            }
            else throw new InvalidOperationException("Could not apply rotation.");
        }

        [Obsolete("Not Implemented")]
        public Vector3 ToLocalPositionVector(Vector3 globalPosition)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<LCS3> GetEnumerator()
        {
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            enumPos++;
            return enumPos < children.Count;
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
        ~LCS3()
        {
            Dispose(false);
        }
    }
}
