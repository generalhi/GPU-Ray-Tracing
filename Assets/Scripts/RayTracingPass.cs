using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GpuRayTracing
{
    public class RayTracingPass : ScriptableRenderPass
    {
        private readonly RayTracingPassSettings _settings;
        private readonly SceneInitializer _sceneInitializer;
        private readonly InputMouse _mouse;

        private Material _material;
        private RenderTexture _rt;

        private readonly int Id_Result = Shader.PropertyToID("Result");
        private readonly int Id_World = Shader.PropertyToID("World");
        private readonly int Id_Projection = Shader.PropertyToID("Projection");
        private readonly int Id_DirectionalLight = Shader.PropertyToID("DirectionalLight");
        private readonly int Id_SkyBoxTexture = Shader.PropertyToID("SkyBoxTexture");
        private readonly int Id_Time = Shader.PropertyToID("Time");
        private readonly int Id_ReflectionsCount = Shader.PropertyToID("ReflectionsCount");
        private readonly int Id_MousePosition = Shader.PropertyToID("MousePosition");

        private readonly int Id_Planes = Shader.PropertyToID("Plains");
        private readonly int Id_Spheres = Shader.PropertyToID("Spheres");
        private readonly int Id_Cubes = Shader.PropertyToID("Cubes");

        public RayTracingPass(RayTracingPassSettings settings)
        {
            _settings = settings;
            renderPassEvent = _settings.renderPassEvent;
            _sceneInitializer = new SceneInitializer();
            _mouse = new InputMouse();
        }

        public override void Execute(
            ScriptableRenderContext context,
            ref RenderingData renderingData)
        {
            _mouse.Update();
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
            if (_sceneInitializer.BufferPlane != null)
            {
                shader.SetBuffer(0, Id_Planes, _sceneInitializer.BufferPlane);
            }

            if (_sceneInitializer.BufferSphere != null)
            {
                shader.SetBuffer(0, Id_Spheres, _sceneInitializer.BufferSphere);
            }

            if (_sceneInitializer.BufferCubes != null)
            {
                shader.SetBuffer(0, Id_Cubes, _sceneInitializer.BufferCubes);
            }

            // Other params
            _settings.RayTracingShader.SetTexture(0, Id_SkyBoxTexture, _settings.SkyBox);
            _settings.RayTracingShader.SetInt(Id_ReflectionsCount, _settings.ReflectionsCount);
            _settings.RayTracingShader.SetFloat(Id_Time, Time.time);
            _settings.RayTracingShader.SetVector(Id_MousePosition, _mouse.Position);
        }
    }
}
