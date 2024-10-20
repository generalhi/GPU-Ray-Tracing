using System.Runtime.InteropServices;
using UnityEngine;

namespace GpuRayTracing.Entities
{
    public struct RCube
    {
        public Vector3 Position;
        public Vector3 Move;
        public Vector3 MoveSpeed;
        public Vector3 RotationSpeed;
        public float Size;
        public float Smooth;
        public Vector3 Specular;
        public Vector3 Albedo;

        public static int GetSize()
        {
            return Marshal.SizeOf<RCube>();
        }
    }
}
