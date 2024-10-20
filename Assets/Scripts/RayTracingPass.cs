using System.Collections.Generic;
using System.Runtime.InteropServices;
using GpuRayTracing.Entities;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GpuRayTracing
{
    public class RayTracingPass : ScriptableRenderPass
    {
        private readonly RayTracingPassSettings _settings;
        private Material _material;
        private RenderTexture _rt;

        private ComputeBuffer _bufferPlane;
        private ComputeBuffer _bufferSphere;
        private ComputeBuffer _bufferCubes;

        private readonly int Id_Result = Shader.PropertyToID("Result");
        private readonly int Id_World = Shader.PropertyToID("World");
        private readonly int Id_Projection = Shader.PropertyToID("Projection");
        private readonly int Id_DirectionalLight = Shader.PropertyToID("DirectionalLight");
        private readonly int Id_SkyBoxTexture = Shader.PropertyToID("SkyBoxTexture");
        private readonly int Id_Time = Shader.PropertyToID("Time");
        private readonly int Id_ReflectionsCount = Shader.PropertyToID("ReflectionsCount");

        private readonly int Id_Planes = Shader.PropertyToID("Plains");
        private readonly int Id_Spheres = Shader.PropertyToID("Spheres");
        private readonly int Id_Cubes = Shader.PropertyToID("Cubes");

        public RayTracingPass(RayTracingPassSettings settings)
        {
            _settings = settings;
            renderPassEvent = _settings.renderPassEvent;
            InitScene();
        }

        public override void Execute(
            ScriptableRenderContext context,
            ref RenderingData renderingData)
        {
            InitRenderTexture();

            // Set render target and run compute shader
            SetShaderParams(ref renderingData);
            int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
            int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
            _settings.RayTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

            // Blit material
            if (_material == null)
            {
                _material = new Material(Shader.Find("Hidden/CopyHDR"));
            }

            // Copy render target to screen
            var cb = CommandBufferPool.Get();
            Blit(cb, _rt, renderingData.cameraData.renderer.cameraColorTarget, _material);
            context.ExecuteCommandBuffer(cb);
            CommandBufferPool.Release(cb);
        }

        private void InitRenderTexture()
        {
            if (_rt == null || _rt.width != Screen.width || _rt.height != Screen.height)
            {
                if (_rt != null)
                {
                    _rt.Release();
                }

                _rt = new RenderTexture(
                    Screen.width,
                    Screen.height,
                    0,
                    RenderTextureFormat.ARGBFloat,
                    RenderTextureReadWrite.Linear)
                {
                    enableRandomWrite = true
                };

                _rt.Create();
            }
        }

        private void InitScene()
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
                    K = 0.6f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0f, 0f, 0f),
                    Albedo = new Vector3(0f, 0f, 0f)
                }
            };

            if (_bufferPlane != null && _bufferPlane.count > 0)
            {
                _bufferPlane.Release();
                _bufferPlane = null;
            }

            if (_bufferPlane == null)
            {
                _bufferPlane = new ComputeBuffer(planes.Count, RPlane.GetSize());
            }

            _bufferPlane.SetData(planes);
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
                    Radius = 0.9f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0f, 0f, 0f),
                    Albedo = new Vector3(0f, 0f, 0f)
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

            if (_bufferSphere != null && _bufferSphere.count > 0)
            {
                _bufferSphere.Release();
                _bufferSphere = null;
            }

            if (_bufferSphere == null)
            {
                _bufferSphere = new ComputeBuffer(spheres.Count, RSphere.GetSize());
            }

            _bufferSphere.SetData(spheres);
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
                    Specular = new Vector3(0f, 0f, 0f),
                    Albedo = new Vector3(0f, 0f, 0f)
                },
                new RCube
                {
                    Position = new Vector3(0f, 0f, 0f),
                    Move = new Vector3(0f, 1.2f, 0f),
                    MoveSpeed = new Vector3(0f, 0.4f, 0f),
                    RotationSpeed = new Vector3(0.3f, 0f, 0.1f),
                    Size = 0.5f,
                    Smooth = 0.2f,
                    Specular = new Vector3(0f, 0f, 0f),
                    Albedo = new Vector3(0f, 0f, 0f)
                },
            };

            if (_bufferCubes != null && _bufferCubes.count > 0)
            {
                _bufferCubes.Release();
                _bufferCubes = null;
            }

            if (_bufferCubes == null)
            {
                _bufferCubes = new ComputeBuffer(cubes.Count, RCube.GetSize());
            }

            _bufferCubes.SetData(cubes);
        }

        private void SetShaderParams(ref RenderingData renderingData)
        {
            var shader = _settings.RayTracingShader;

            // Textures
            shader.SetTexture(0, Id_Result, _rt);

            // Matrix
            shader.SetMatrix(Id_World, renderingData.cameraData.camera.cameraToWorldMatrix);
            shader.SetMatrix(
                Id_Projection,
                renderingData.cameraData.camera.projectionMatrix.inverse);

            // Lights
            shader.SetVector(Id_DirectionalLight, _settings.DirectionLight);

            // Primitives
            if (_bufferPlane != null)
            {
                shader.SetBuffer(0, Id_Planes, _bufferPlane);
            }

            if (_bufferSphere != null)
            {
                shader.SetBuffer(0, Id_Spheres, _bufferSphere);
            }

            if (_bufferCubes != null)
            {
                shader.SetBuffer(0, Id_Cubes, _bufferCubes);
            }

            // Other params
            _settings.RayTracingShader.SetTexture(0, Id_SkyBoxTexture, _settings.SkyBox);
            _settings.RayTracingShader.SetInt(Id_ReflectionsCount, _settings.ReflectionsCount);
            _settings.RayTracingShader.SetFloat(Id_Time, Time.time);
        }
    }
}
