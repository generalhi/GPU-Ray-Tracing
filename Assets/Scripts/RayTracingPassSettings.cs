using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GpuRayTracing
{
    [Serializable]
    public class RayTracingPassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        public ComputeShader RayTracingShader;
    }
}
