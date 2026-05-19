namespace FoodLens.Core.ModeDetection
{
    /// <summary>
    /// Supported AR modes based on device capability checking.
    /// </summary>
    public enum ARMode
    {
        FullAR,  // High-end devices: ARCore, real-time lighting estimation, shadow catcher, 60 FPS.
        LiteAR,  // Mid-range devices: ARCore, lower poly models, simple lighting, shadows disabled.
        FakeAR   // Low-end devices: Camera background feed + fixed 3D overlay rendering.
    }
}
