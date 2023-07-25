using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths;
using Engine.Core.Components;
using System.Collections;

namespace Engine.Core
{
    public class Object3D
    {
        private LCS3 local;
        private Object3D parent;

        public string Name;
        public List<Component> Components = new List<Component>();
        /// <summary>
        /// local coordinate system of the object
        /// </summary>
        public LCS3 Local => local;

        /// <summary>
        /// The set-accessor resets the local transformations the new parent ones. To keep this Object3D's transformation, use the method instead.
        /// </summary>
        public Object3D Parent
        {
            get { return parent; }
            set { SetParent(value); }
        }

        public Object3D(bool global = false)
        {
            Init(global);
        }
        public Object3D(Vector3 position, bool global = false)
        {
            Init(global, position);
        }
        public Object3D(Vector3 position, RotationVector3 rotation, bool global = false)
        {
            Init(global, position, rotation);
        }
        public Object3D(Vector3 position, RotationVector3 rotation, Vector3 scale, bool global = false)
        {
            Init(global, position, rotation, scale);
        }

        private void Init(bool global = false, Vector3 position = null, RotationVector3 rotation = null, Vector3 scale = null)
        {
            if (global)
            {
                LCS3 lcs = AddComponent(new LCS3(this));
                lcs.GlobalPosition = position ?? Parent?.Local.GlobalPosition ?? new Vector3(Vector3.Zero);
                lcs.GlobalRotation = rotation ?? Parent?.Local.GlobalRotation ?? new RotationVector3(0, 0, 0, RotationMeasure.RAD);
                lcs.GlobalScale = scale ?? Parent?.Local.GlobalScale ?? new Vector3(1, 1, 1);
                local = lcs;
            }
            else
            {
                LCS3 lcs = AddComponent(new LCS3(this));
                if (Parent == null)
                {
                    lcs.GlobalPosition = position ?? new Vector3(Vector3.Zero);
                    lcs.GlobalRotation = rotation ?? new RotationVector3(0, 0, 0, RotationMeasure.RAD);
                    lcs.GlobalScale = scale ?? new Vector3(1, 1, 1);

                    lcs.LocalPosition = position ?? new Vector3(Vector3.Zero);
                    lcs.LocalRotation = rotation ?? new RotationVector3(0, 0, 0, RotationMeasure.RAD);
                    lcs.LocalScale = scale ?? new Vector3(1, 1, 1);
                }
                else
                {
                    lcs.LocalPosition = position ?? new Vector3(Vector3.Zero);
                    lcs.LocalRotation = rotation ?? new RotationVector3(0, 0, 0, RotationMeasure.RAD);
                    lcs.LocalScale = scale ?? new Vector3(1, 1, 1);

                    Parent.Local.GlobalPosition = Parent.Local.GlobalPosition + lcs.LocalPosition;
                    Parent.Local.GlobalRotation = Parent.Local.GlobalRotation + lcs.LocalRotation;
                    Parent.Local.GlobalScale = Parent.Local.GlobalScale + lcs.LocalScale;
                }
            }
        }

        public void SetParent(Object3D parent, bool keepGlobalTransformation = false)
        {
            if (keepGlobalTransformation)
            {
                Vector3 p = Local.GlobalPosition;
                RotationVector3 r = Local.GlobalRotation;
                Vector3 s = Local.GlobalScale;
                this.parent = parent;
                Init(true, p, r, s);
            }
            else
            {
                this.parent = parent;
                Init(false);
            }
        }

        public T AddComponent<T>(T component) where T : Component
        {
            component.Dependency = this;
            Components.Add(component);
            return component;
        }

        public void RemoveComponent<T>(T component) where T: Component
        {
            if (typeof(T) != typeof(LCS3))
            {
                component.Dependency = null;
                Components.Remove(component);
            }
        }

        public T GetComponent<T>(T component) where T : Component
        {
            return (T)Components.Find(x => x == component); 
        }
        public T GetComponent<T>() where T : Component
        {
            return (T)Components.Find(x => x.GetType() == typeof(T));
        }

        public List<T> GetComponents<T>(T component) where T : Component
        {
            return Components.FindAll(x => x == component) as List<T>;
        }

        public List<T> GetComponents<T>() where T : Component
        {
            return Components.FindAll(x => x.GetType() == typeof(T)) as List<T>;
        }

        /// <summary>
        /// Finds and returns the first child found with the inputted <paramref name="name"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Object3D FindChild(string name)
        {
            Object3D res = null;
            foreach (LCS3 child in Local)
            {
                if (res == null && child.Dependency.Name == name)
                {
                    res = child.Dependency;
                }
            }
            return res;
        }

        public static implicit operator LCS3(Object3D o)
        {
            return o.Local;
        }
    }
}
