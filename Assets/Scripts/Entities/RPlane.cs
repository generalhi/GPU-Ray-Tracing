using System.Runtime.InteropServices;
using UnityEngine;

namespace GpuRayTracing.Entities
{
    public struct RPlane
    {
        public Vector3 Normal;
        public float K;
        public float Smooth;
        public Vector3 Specular;
        public Vector3 Albedo;

        public static int GetSize()
        {
            return Marshal.SizeOf<RPlane>();
        }
    }
}
