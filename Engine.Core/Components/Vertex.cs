using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Components
{
    public class Vertex : Component
    {
        private Vector localPosition;
        private Vector globalPosition;

        public Vector LocalPosition { get => localPosition; set { SetPosition(value); } }
        public Vector GlobalPosition { get => globalPosition; }

        public Vertex(Vector localPosition, Object3D dependency) : base(dependency)
        {
            this.dependency = dependency;
            SetPosition(localPosition);
        }

        public void SetPosition(Vector localPosition)
        {
            this.localPosition = localPosition;
            globalPosition = this.dependency.GlobalPosition + localPosition;
        }
    }
}
