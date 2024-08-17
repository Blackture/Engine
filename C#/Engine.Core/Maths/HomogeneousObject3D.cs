using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Maths
{
    public class HomogeneousObject3D
    {
        private Vector3 vector;
        private Vector vector4;

        public float W;

        public float X1 { get => vector.X1; set => vector.X1 = value; }
        public float X2 { get => vector.X2; set => vector.X2 = value; }
        public float X3 { get => vector.X3; set => vector.X3 = value; }


        public HomogeneousObject3D(Vector3 vector, HomogeneneousType type)
        {
            this.vector = vector;
            W = (int)type;
            vector4 = GetVector4();
            if (vector4.IsZeroVector()) throw new Exception("A mathematically senseless operation occured."); 
        }
        public HomogeneousObject3D(Vector3 vector, float w)
        {
            this.vector = vector * w;
            W = w;
            vector4 = GetVector4();
            if (vector4.IsZeroVector()) throw new Exception("A mathematically senseless operation occured.");

        }
        public HomogeneousObject3D(float x1, float x2, float x3, float w)
        {
            X1 = x1;
            X2 = x2;
            X3 = x3;
            W = w;
            vector4 = GetVector4();
            if (vector4.IsZeroVector()) throw new Exception("A mathematically senseless operation occured.");

        }

        private Vector GetVector4()
        {
            return new Vector(X1, X2, X3, W);
        }

        public Vector3 ToEuclideanSpace()
        {
            return (W != 0) ? new Vector3(X1 / W, X2 / W, X3 / W) : null;
        }

        public static implicit operator Vector(HomogeneousObject3D v)
        {
            return v.vector4;
        }
    }
}
