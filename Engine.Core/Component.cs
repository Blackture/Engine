using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class Component
    {
        private Object3D dependency;
        public Object3D Dependency { get => dependency; set => dependency = value ?? throw new ArgumentNullException(); }

        public Component(Object3D dependency)
        {
            Dependency = dependency;
        }
    }
}
