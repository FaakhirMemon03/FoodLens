using UnityEngine;

namespace FoodLens.Assets
{
    /// <summary>
    /// Utility class to programmatically validate that food model prefabs
    /// adhere to target LOD triangle limits (LOD0: <15k, LOD1: <7k, LOD2: <3k).
    /// </summary>
    public class LODUtility : MonoBehaviour
    {
        /// <summary>
        /// Validates that the given prefab has exactly 3 LOD levels and fits poly count budgets.
        /// </summary>
        /// <returns>True if all levels pass limits, false otherwise.</returns>
        public static bool ValidateAssetLOD(GameObject prefab)
        {
            if (prefab == null)
            {
                Debug.LogError("[FoodLens LODUtility] Prefab reference is null.");
                return false;
            }

            LODGroup lodGroup = prefab.GetComponent<LODGroup>();
            if (lodGroup == null)
            {
                Debug.LogWarning($"[FoodLens LODUtility] Prefab '{prefab.name}' is missing an LODGroup component!");
                return false;
            }

            LOD[] lods = lodGroup.GetLODs();
            if (lods.Length < 3)
            {
                Debug.LogWarning($"[FoodLens LODUtility] Prefab '{prefab.name}' contains only {lods.Length} LODs (expected at least 3).");
                return false;
            }

            bool isAllLODLevelsValid = true;

            // 1. LOD0 Check: must be less than 15,000 triangles.
            int lod0Tris = CalculateLODTriangles(lods[0]);
            if (lod0Tris > 15000)
            {
                Debug.LogWarning($"[FoodLens LODUtility] WARNING: '{prefab.name}' LOD0 exceeds limit! Triangles: {lod0Tris} (Max Allowed: 15,000)");
                isAllLODLevelsValid = false;
            }
            else
            {
                Debug.Log($"[FoodLens LODUtility] '{prefab.name}' LOD0 Pass. Triangles: {lod0Tris}");
            }

            // 2. LOD1 Check: must be less than 7,000 triangles.
            int lod1Tris = CalculateLODTriangles(lods[1]);
            if (lod1Tris > 7000)
            {
                Debug.LogWarning($"[FoodLens LODUtility] WARNING: '{prefab.name}' LOD1 exceeds limit! Triangles: {lod1Tris} (Max Allowed: 7,000)");
                isAllLODLevelsValid = false;
            }
            else
            {
                Debug.Log($"[FoodLens LODUtility] '{prefab.name}' LOD1 Pass. Triangles: {lod1Tris}");
            }

            // 3. LOD2 Check: must be less than 3,000 triangles.
            int lod2Tris = CalculateLODTriangles(lods[2]);
            if (lod2Tris > 3000)
            {
                Debug.LogWarning($"[FoodLens LODUtility] WARNING: '{prefab.name}' LOD2 exceeds limit! Triangles: {lod2Tris} (Max Allowed: 3,000)");
                isAllLODLevelsValid = false;
            }
            else
            {
                Debug.Log($"[FoodLens LODUtility] '{prefab.name}' LOD2 Pass. Triangles: {lod2Tris}");
            }

            return isAllLODLevelsValid;
        }

        private static int CalculateLODTriangles(LOD lod)
        {
            int totalTriangles = 0;
            foreach (Renderer renderer in lod.renderers)
            {
                if (renderer == null) continue;

                // Handle static MeshFilter meshes
                MeshFilter filter = renderer.GetComponent<MeshFilter>();
                if (filter != null && filter.sharedMesh != null)
                {
                    totalTriangles += filter.sharedMesh.triangles.Length / 3;
                }

                // Handle animated/skinned meshes if present
                SkinnedMeshRenderer skinnedRenderer = renderer as SkinnedMeshRenderer;
                if (skinnedRenderer != null && skinnedRenderer.sharedMesh != null)
                {
                    totalTriangles += skinnedRenderer.sharedMesh.triangles.Length / 3;
                }
            }
            return totalTriangles;
        }
    }
}
