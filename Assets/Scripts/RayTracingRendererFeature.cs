using UnityEngine.Rendering.Universal;

namespace GpuRayTracing
{
    public class RayTracingRendererFeature : ScriptableRendererFeature
    {
        public RayTracingPassSettings Settings = new();

        private RayTracingPass _pass;

        public override void Create()
        {
            _pass = new RayTracingPass(Settings);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_pass);
        }
    }
}
