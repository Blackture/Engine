using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Components
{
    public class Vertex3D : Component
    {
        private Vector3 localPosition;
        private Vector3 globalPosition;

        public Vector3 LocalPosition { get => localPosition; set { SetPosition(value); } }
        public Vector3 GlobalPosition { get => globalPosition; }

        public Vertex3D(Vector3 localPosition, Object3D dependency) : base(dependency)
        {
            this.Dependency = dependency;
            SetPosition(localPosition);
        }

        [Obsolete("Global Position is got wrongly.")]
        public void SetPosition(Vector3 localPosition)
        {
            this.localPosition = localPosition;
            globalPosition = Dependency.Local.GlobalPosition + localPosition;
        }
    }
}
