using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_UTILITY_FACE_PLAYER : MonoBehaviour
    {
        [HideInInspector] public Transform player;  // Reference to the player's transform

        private void Awake()
        {
            player = FindFirstObjectByType<PlayerManager>().transform;
        }
        void Update()
        {
            if (player != null)
            {
                // Make the object look at the player
                transform.LookAt(player);

                // Optionally, you can lock the rotation on specific axes.
                // For example, to only rotate on the Y axis, you can use:
                Vector3 lookDirection = player.position - transform.position;
                lookDirection.y = 0; // Keep the y component zero to lock the y axis rotation
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}