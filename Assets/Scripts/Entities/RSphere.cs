using System.Runtime.InteropServices;
using UnityEngine;

namespace GpuRayTracing.Entities
{
    public struct RSphere
    {
        public float Radius;
        public float Smooth;
        public Vector3 Albedo;
        public Vector3 Specular;

        public static int GetSize()
        {
            return Marshal.SizeOf<RSphere>();
        }
    }
}
