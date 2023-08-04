using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Components;

namespace Engine.Core.Physics.CollisionDetection
{
    public class BoundingVolume3D
    {
        public List<BoundingBox3D> boxes;
        public List<BoundingVolume3D> volumes;

        private BoundingBox3D bounds;
        private int boundingDepth;
        public int BoundingDepth => boundingDepth;
        public BoundingBox3D Boundings => bounds;

        public BoundingVolume3D(List<BoundingBox3D> boxes)
        {
            this.boxes = boxes;
            bounds = GetBoundingBox(boxes);
        }
        public BoundingVolume3D(List<BoundingVolume3D> volumes)
        {
            this.volumes = volumes;
            bounds = GetBoundingBox(volumes);
        }
        public BoundingVolume3D(List<BoundingVolume3D> volumes, List<BoundingBox3D> boxes)
        {
            this.volumes = volumes;
            this.boxes = boxes;
            bounds = GetBoundingBox(volumes, boxes);
        }

        private BoundingBox3D GetBoundingBox(List<BoundingBox3D> boxes)
        {
            List<BoundingBox3D> _boxes = new List<BoundingBox3D>();
            boxes.CopyTo(_boxes.ToArray());
            float maxX1 = _boxes[0].maxX1;
            float maxX2 = _boxes[0].maxX2;
            float maxX3 = _boxes[0].maxX3;
            float minX1 = _boxes[0].minX1;
            float minX2 = _boxes[0].minX2;
            float minX3 = boxes[0].minX3;
            _boxes.RemoveAt(0);

            foreach (BoundingBox3D v in _boxes)
            {
                maxX1 = (v.maxX1 > maxX1) ? v.maxX1 : maxX1;
                maxX2 = (v.maxX2 > maxX2) ? v.maxX2 : maxX2;
                maxX3 = (v.maxX3 > maxX3) ? v.maxX3 : maxX3;

                minX1 = (v.minX1 < minX1) ? v.minX1 : minX1;
                minX2 = (v.minX2 < minX2) ? v.minX2 : minX2;
                minX3 = (v.minX3 < minX3) ? v.minX3 : minX3;
            }

            return new BoundingBox3D(maxX1, maxX2, maxX3, minX1, minX2, minX3);
        }
        private BoundingBox3D GetBoundingBox(List<BoundingVolume3D> volumes)
        {
            List<BoundingBox3D> boxes = new List<BoundingBox3D>();
            foreach (BoundingVolume3D volume in volumes)
            {
                boxes.Add(volume.Boundings);
            }
            this.boxes.AddRange(boxes);
            return GetBoundingBox(boxes);
        }
        private BoundingBox3D GetBoundingBox(List<BoundingVolume3D> volumes, List<BoundingBox3D> boxes)
        {
            List<BoundingBox3D> _boxes = new List<BoundingBox3D>();
            foreach (BoundingVolume3D volume in volumes)
            {
                _boxes.Add(volume.Boundings);
            }
            this.boxes.AddRange(_boxes);
            _boxes.AddRange(boxes);
            return GetBoundingBox(_boxes);
        }
    }
}
