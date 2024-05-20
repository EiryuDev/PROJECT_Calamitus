using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_DAMAGE_HITBOX : MonoBehaviour
    {
        public float deflectionMultiplier = 1.5f;
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
        }
    }
}
