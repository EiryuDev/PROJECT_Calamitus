using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_DAMAGE_HITBOX : MonoBehaviour
    {
        [HideInInspector] public PlayerManager player;

        public float deflectionMultiplier = 1.5f;

        private void Awake()
        {
            player = FindFirstObjectByType<PlayerManager>();
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                // Reverse the bullet's velocity for deflection
                Rigidbody rb = collision.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 incomingDirection = rb.linearVelocity.normalized;
                    Vector3 deflectionDirection = -incomingDirection; // Opposite direction

                    rb.linearVelocity = deflectionDirection * rb.linearVelocity.magnitude * deflectionMultiplier;
                }
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                AICharacterManager aiCharacter = collision.gameObject.GetComponentInParent<AICharacterManager>();
                aiCharacter.aiCharacterStatsManager.TakeDamage(player.playerInventoryManager.currentWeaponDataBeingUsed.physicalDamage);
            }
        }
    }
}
