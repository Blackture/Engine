using Engine.Core.Components;
using Engine.Core.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Physics.Optics
{
    public class Lens
    {
        private LensType lens;

        private float fovX;
        private float fovXRad;
        private float fovY;
        private float fovYRad;

        private float thickness;
        private float minThickness = 2E-3f;

        public LensType Type { get { return lens; } }
        public float FovX { get { return fovX; } }
        public float FovY { get { return fovY; } }
        public float FovXRad { get { return fovXRad; } }
        public float FovYRad { get { return fovYRad; } }

        public Lens() 
        {
            lens = LensType.None;
            fovX = 60;
            fovY = fovX / 2;
            fovXRad = Mathf.Deg2Rad(fovX);
            fovYRad = Mathf.Deg2Rad(fovY);
        }

        /// <summary>
        /// Creates the ray from the image plane to the near plane, and outputs a float "r" defined for when the ray hits the nearplane.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="cam"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public Straight CreateRay(float s, float t, Camera cam, out float r)
        {
            Straight res = null;
            r = 0;
            if (LensType.None == Type)
            {
                Vector3 OB = cam.NearPlane.plane.GetPositionVectorAt(s, t);
                r = 1f;
                res = new Straight(cam.Dependency.Local.GlobalPosition, OB, Straight.LineSetupType._2Points);
            }
            else
            { 
                Vector3 OB = cam.ImagePlane.plane.GetPositionVectorAt(-s, -t);
                Straight toLens = new Straight(cam.Dependency.Local.GlobalPosition, OB, Straight.LineSetupType._2Points);
                Vector3 toLensDir = toLens.Dir;
                Vector3 outDir = LensRefraction(toLensDir);
                res = new Straight(cam.Dependency.Local.GlobalPosition, outDir, Straight.LineSetupType._1Point1Dir);
                if (Plane.Intersection(cam.NearPlane.plane, res, out Straight st, out Vector3 point))
                {
                    if (st != null) throw new Exception("Something went wrong.");
                    r = (point.X1 - cam.Dependency.Local.GlobalPosition.X1) / outDir.X1;
                }
                else throw new Exception("Something went wrong.");

            }
            return res;
        }

        [Obsolete("Not Implemented")]
        private Vector3 LensRefraction(Vector3 originalDir)
        {
            return originalDir; ;
        }
    }
}
