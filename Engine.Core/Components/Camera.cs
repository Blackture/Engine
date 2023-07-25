using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Maths;
using Engine.Core.Physics.Optics;
using Engine.Core.SceneManagement;

namespace Engine.Core.Components
{
    public class Camera : Component
    {
        private CameraPlane nearPlane;
        private CameraPlane farPlane;
        private CameraPlane imagePlane;
        private float nearPlaneDistance;
        private float imagePlaneDistance;
        private float farPlaneDistance;
        private Lens lens;
        

        public float NearPlaneDistance
        {
            get { return nearPlaneDistance; }
            set { nearPlaneDistance = value; nearPlane = new CameraPlane(value, Dependency); }
        }
        public float FarPlaneDistance
        {
            get { return farPlaneDistance; }
            set { farPlaneDistance = value; farPlane = new CameraPlane(value, Dependency); }
        }
        public CameraPlane NearPlane { get { return nearPlane; } }
        public CameraPlane FarPlane { get { return farPlane; } }
        public CameraPlane ImagePlane { get { return imagePlane; } }

        public Camera(Object3D dependency, float distanceToNearPlane = 0.25f, float distanceToFarPlane = 1000f) : base(dependency)
        {
            nearPlaneDistance = distanceToNearPlane;
            nearPlane = new CameraPlane(nearPlaneDistance, dependency);
            imagePlaneDistance = -distanceToNearPlane;
            imagePlane = new CameraPlane(imagePlaneDistance, dependency);
            farPlaneDistance = distanceToFarPlane;
            farPlane = new CameraPlane(farPlaneDistance, dependency);
            lens = new Lens();
        }

        public Pixel RenderPoint(float ds, float dt)
        {
            float s = ds * nearPlane.BoundaryS[1];
            float t = dt * nearPlane.BoundaryT[1];

            if (AnyLightSource(out List<LightSource> sources))
            {
                Straight sray = lens.CreateRay(s, t, this, out float r);
                Ray ray = new Ray(sray, r);
                Particle p = ray.Emit(1000);
            }



            return new Pixel();
        }

        public class CameraPlane
        {
            private readonly Object3D dependency;

            private Plane _plane;
            private Vector3 globalVertical;
            private Vector3 globalHorizontal;
            private Vector3 normal;

            public Plane plane { get { return _plane; } }
            public Vector3 GlobalVertical { get { return globalVertical; } }
            public Vector3 GlobalHorizontal { get { return globalHorizontal; } }
            public Vector3 Normal { get { return normal; } }

            float[] boundaryS;
            float[] boundaryT;

            public float[] BoundaryS { get { return boundaryS; } }
            public float[] BoundaryT { get { return boundaryT; } }

            public CameraPlane(float distance, Object3D dependency)
            {
                this.dependency = dependency;
                InitPlane(distance);
                GetBoundaries();
            }

            private void InitPlane(float distance)
            {
                Vector3[] axisAsGlobalUnitVectors = dependency.Local.AxisToGlobalUnitVectors();
                normal = axisAsGlobalUnitVectors[0];
                Straight normalS = new Straight(dependency.Local.GlobalPosition, normal, Straight.LineSetupType._1Point1Dir);
                Vector3 pos = normalS.GetPointAt(distance);
                _plane = new Plane(pos, axisAsGlobalUnitVectors[0]);
                globalHorizontal = axisAsGlobalUnitVectors[1];
                globalVertical = axisAsGlobalUnitVectors[2];
            }

            private bool GetBoundaries()
            {
                bool res = true;
                Lens lens = dependency.GetComponent<Camera>().lens;
                if (lens.Type == LensType.None)
                {
                    MatrixMxN R = dependency.Local.GlobalRotation;
                    if ((R * new RotationMatrix3x3(0, lens.FovXRad, lens.FovYRad) * GCS3.Axes[0].Dir.ToMatrix()).ToVector3(out Vector3 dirC1))
                    {
                        if ((R * new RotationMatrix3x3(0, lens.FovXRad, -lens.FovYRad) * GCS3.Axes[0].Dir.ToMatrix()).ToVector3(out Vector3 dirC2))
                        {
                            if ((R * new RotationMatrix3x3(0, -lens.FovXRad, lens.FovYRad) * GCS3.Axes[0].Dir.ToMatrix()).ToVector3(out Vector3 dirC3))
                            {
                                if ((R * new RotationMatrix3x3(0, lens.FovXRad, -lens.FovYRad) * GCS3.Axes[0].Dir.ToMatrix()).ToVector3(out Vector3 dirC4))
                                {
                                    List<Vector3> points = new List<Vector3>();

                                    Straight sC1 = new Straight(dependency.Local.GlobalPosition, dirC1, Straight.LineSetupType._1Point1Dir);
                                    Straight sC2 = new Straight(dependency.Local.GlobalPosition, dirC2, Straight.LineSetupType._1Point1Dir);
                                    Straight sC3 = new Straight(dependency.Local.GlobalPosition, dirC3, Straight.LineSetupType._1Point1Dir);
                                    Straight sC4 = new Straight(dependency.Local.GlobalPosition, dirC4, Straight.LineSetupType._1Point1Dir);

                                    if (Plane.Intersection(_plane, sC1, out Straight ilC1, out Vector3 C1))
                                    {
                                        if (C1 == null) res = false;
                                        else
                                        {
                                            points.Add(C1);
                                        }
                                    }
                                    else res = false;

                                    if (Plane.Intersection(_plane, sC2, out Straight ilC2, out Vector3 C2))
                                    {
                                        if (C2 == null) res = false;
                                        else
                                        {
                                            points.Add(C2);
                                        }
                                    }
                                    else res = false;

                                    if (Plane.Intersection(_plane, sC3, out Straight ilC3, out Vector3 C3))
                                    {
                                        if (C3 == null) res = false;
                                        else
                                        {
                                            points.Add(C3);
                                        }
                                    }
                                    else res = false;


                                    if (Plane.Intersection(_plane, sC4, out Straight ilC4, out Vector3 C4))
                                    {
                                        if (C4 == null) res = false;
                                        else
                                        {
                                            points.Add(C4);
                                        }
                                    }
                                    else res = false;

                                    float[] stLimits = GetSTLimits(points.ToArray());
                                    boundaryS = new float[] { stLimits[0], stLimits[1] };
                                    boundaryT = new float[] { stLimits[2], stLimits[3] };
                                }
                                else res = false;
                            }
                            else res = false;
                        }
                        else res = false;
                    }
                    else res = false;
                }
                else res = false;

                return res;
            }

            private float[] GetSTLimits(Vector3[] points)
            {
                float[] limits = new float[4];

                // normal = normalized _plane normal
                Vector3 normal = plane.N.Normalized;

                // calculate the limits for s and t
                limits[0] = float.MaxValue;
                limits[1] = float.MinValue;
                limits[2] = float.MaxValue;
                limits[3] = float.MinValue;
                for (int i = 0; i < points.Length; i++)
                {
                    float s = (normal.X1 * points[i].X1 + normal.X2 * points[i].X2 + normal.X3 * points[i].X3 + plane.B) / (normal.X1 * plane.N.X1 + normal.X2 * plane.N.X2 + normal.X3 * plane.N.X3);
                    float t = (plane.N.X2 * points[i].X1 - plane.N.X1 * points[i].X2) / (plane.N.X1 * normal.X2 - plane.N.X2 * normal.X1);
                    limits[0] = Math.Min(limits[0], s);
                    limits[1] = Math.Max(limits[1], s);
                    limits[2] = Math.Min(limits[2], t);
                    limits[3] = Math.Max(limits[3], t);
                }
                return limits;
            }

            public Vector3 GetPoint(float s, float t)
            {
                Vector3 res = Vector3.Zero;
                if (s >= boundaryS[0] && s <= boundaryS[1] && t >= boundaryT[0] && t <= boundaryT[1])
                {
                    res = plane.GetPositionVectorAt(s, t);
                }
                return res;
            }
        }

        /// <summary>
        /// True if any light source affects the scene part that is rendered.
        /// Outputs all light sources that affect the scene.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Not implemented")]
        public bool AnyLightSource(out List<LightSource> lightSources)
        {
            lightSources = (SceneManager.Instance.ActiveScene as Scene3D).GlobalCoordinateSystem.LightSources;
            return false;
        }
    }
}
