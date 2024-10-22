using System.Collections.Generic;
using GpuRayTracing.Entities;
using UnityEngine;

namespace GpuRayTracing
{
    public class SceneInitializer
    {
        public ComputeBuffer BufferPlane;
        public ComputeBuffer BufferSphere;
        public ComputeBuffer BufferCubes;

        public SceneInitializer()
        {
            InitPlanes();
            InitSpheres();
            InitCubes();
        }

        private void InitPlanes()
        {
            var planes = new List<RPlane>
            {
                new RPlane
                {
                    Normal = new Vector3(0f, 1f, 0f),
                    K = 1.8f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0.8f, 0.8f, 0.8f),
                    Albedo = new Vector3(0.1f, 0.1f, 0.1f)
                }
            };

            if (BufferPlane != null && BufferPlane.count > 0)
            {
                BufferPlane.Release();
                BufferPlane = null;
            }

            if (BufferPlane == null)
            {
                BufferPlane = new ComputeBuffer(planes.Count, RPlane.GetSize());
            }

            BufferPlane.SetData(planes);
        }

        private void InitSpheres()
        {
            var spheres = new List<RSphere>
            {
                new RSphere
                {
                    Position = new Vector3(0f, 0f, 0f),
                    Move = new Vector3(2.5f, 0f, 0f),
                    MoveSpeed = new Vector3(1.0f, 0f, 0f),
                    Radius = 0.3f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0.01f, 0.01f, 0.01f),
                    Albedo = new Vector3(0.01f, 0.01f, 0.01f)
                },
                /*
                new RSphere
                {
                    Position = new Vector3(0f, 0f, 0f),
                    Move = new Vector3(2.5f, 0f, 0f),
                    MoveSpeed = new Vector3(0.3f, 0f, 0f),
                    Radius = 0.5f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0f, 0f, 0f),
                    Albedo = new Vector3(0f, 0f, 0f)
                },
                new RSphere
                {
                    Position = new Vector3(0f, 0f, 0f),
                    Move = new Vector3(2.0f, 0f, 0f),
                    MoveSpeed = new Vector3(0.7f, 0f, 0f),
                    Radius = 0.4f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0f, 0f, 0f),
                    Albedo = new Vector3(0f, 0f, 0f)
                },
            */
            };

            if (BufferSphere != null && BufferSphere.count > 0)
            {
                BufferSphere.Release();
                BufferSphere = null;
            }

            if (BufferSphere == null)
            {
                BufferSphere = new ComputeBuffer(spheres.Count, RSphere.GetSize());
            }

            BufferSphere.SetData(spheres);
        }

        private void InitCubes()
        {
            var cubes = new List<RCube>
            {
                new RCube
                {
                    Position = new Vector3(0f, 0f, 0f),
                    Move = new Vector3(0f, 1.2f, 0f),
                    MoveSpeed = new Vector3(0f, 0.3f, 0f),
                    RotationSpeed = new Vector3(0.5f, 0f, 0.25f),
                    Size = 0.5f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0.9f, 0.8f, 0.3f),
                    Albedo = new Vector3(0.9f, 0.8f, 0.01f)
                },
                new RCube
                {
                    Position = new Vector3(0f, 0f, 0f),
                    Move = new Vector3(0f, 1.2f, 0f),
                    MoveSpeed = new Vector3(0f, 0.4f, 0f),
                    RotationSpeed = new Vector3(0.3f, 0f, 0.1f),
                    Size = 0.5f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0.9f, 0.3f, 0.3f),
                    Albedo = new Vector3(0.9f, 0.01f, 0.01f)
                },
            };

            if (BufferCubes != null && BufferCubes.count > 0)
            {
                BufferCubes.Release();
                BufferCubes = null;
            }

            if (BufferCubes == null)
            {
                BufferCubes = new ComputeBuffer(cubes.Count, RCube.GetSize());
            }

            BufferCubes.SetData(cubes);
        }
    }
}
