using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class Component
    {
        public Object3D dependency;

        public Component(Object3D dependency)
        {
            this.dependency = dependency;
        }
    }
}
