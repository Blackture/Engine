using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths;
using Engine.Core.Components;

namespace Engine.Core
{
    public class Object3D : LCS
    {
        public string name;
        public List<Component> Components = new List<Component>();

        public Object3D() 
        {
            GlobalPosition = new Vector3(0, 0, 0);
            Rotation = new Vector3(0, 0, 0);
            Scale = new Vector3(1, 1, 1);
        }

        public void AddComponent<T>(T component) where T : Component
        {
            component.dependency = this;
            Components.Add(component);
        }

        public void RemoveComponent<T>(T component) where T: Component
        {
            component.dependency = null;
            Components.Remove(component);
        }
    }
}
