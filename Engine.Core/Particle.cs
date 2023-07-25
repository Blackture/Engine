using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class Particle
    {
        private Vector3 position;

        public Particle(bool destroyOnHit = false)
        {

        }

        public void SetPosition(Vector3 newPosition)
        {
            position = newPosition;
            CheckPosition();
        }

        public void CheckPosition()
        {

        }
    }
}
