using System;

namespace FoodLens.Core.ModeDetection
{
    /// <summary>
    /// Device thresholds for assigning AR visualization mode.
    /// </summary>
    [Serializable]
    public class DeviceThresholds
    {
        // Minimum system memory in megabytes (MB)
        public int minRAM_Full = 5000;
        public int minRAM_Lite = 3000;

        // Minimum graphics (GPU) memory in megabytes (MB)
        public int minGPU_Full = 1500;
        public int minGPU_Lite = 800;
    }
}
