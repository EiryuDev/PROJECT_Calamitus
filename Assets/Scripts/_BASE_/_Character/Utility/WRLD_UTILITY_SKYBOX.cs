using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_UTILITY_SKYBOX : MonoBehaviour
    {
        public float rotationSpeed = 1.0f; // Speed of the skybox rotation

        void Update()
        {
            // Calculate the new rotation value
            float rotation = Time.time * rotationSpeed;

            // Set the rotation of the skybox
            RenderSettings.skybox.SetFloat("_Rotation", rotation);
        }
    }
}
