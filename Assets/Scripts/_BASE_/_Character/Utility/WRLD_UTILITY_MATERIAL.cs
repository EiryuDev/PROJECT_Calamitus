using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_UTILITY_MATERIAL : MonoBehaviour
    {
        public float flowSpeed = 1.0f; // Adjust the speed of the flow

        private Material material;
        private Vector2 flowOffset = Vector2.zero;

        void Start()
        {
            // Get the material attached to the game object
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                material = renderer.material;
            }
            else
            {
                Debug.LogError("Renderer component not found.");
                enabled = false; // Disable the script if no renderer is found
            }
        }

        void Update()
        {
            // Calculate the new offset based on time and speed
            flowOffset.x += Time.deltaTime * flowSpeed;
            flowOffset.y += Time.deltaTime * (flowSpeed * 0.5f); // You can adjust the y offset to create a different flow pattern

            // Apply the offset to the material
            material.SetTextureOffset("_BaseMap", flowOffset); // "_BaseMap" is the property name for the main texture in URP
        }
    }
}
