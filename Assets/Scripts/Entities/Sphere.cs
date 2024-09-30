﻿using System.Runtime.InteropServices;
using UnityEngine;

namespace GpuRayTracing.Entities
{
    public struct Sphere
    {
        public Vector3 Position;
        public float Radius;
        public Vector3 Albedo;
        public Vector3 Specular;

        public static int GetSize()
        {
            return Marshal.SizeOf<Sphere>();
        }
    }
}