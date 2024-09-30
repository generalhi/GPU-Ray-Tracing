﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GpuRayTracing
{
    public class RayTracingPass : ScriptableRenderPass
    {
        private readonly RayTracingPassSettings _passSettings;
        private Material _material;
        private RenderTexture _rt;
        private readonly int Result = Shader.PropertyToID("Result");

        public RayTracingPass(RayTracingPassSettings passPassSettings)
        {
            _passSettings = passPassSettings;
            renderPassEvent = _passSettings.renderPassEvent;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            InitRenderTexture();

            // Set render target and run compute shader
            _passSettings.RayTracingShader.SetTexture(0, Result, _rt);
            int threadGroupsX = Mathf.CeilToInt(Screen.width / 8.0f);
            int threadGroupsY = Mathf.CeilToInt(Screen.height / 8.0f);
            _passSettings.RayTracingShader.Dispatch(0, threadGroupsX, threadGroupsY, 1);

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
    }
}