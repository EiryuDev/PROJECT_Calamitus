using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_PROJECTILE_MANAGER : MonoBehaviour
    {
        [Header("PROJECTILE DATA")]
        public WRLD_PROJECTILE_WEAPON_ITEM currentProjectileItemBeingUsed;

        private void Start()
        {
            Destroy(gameObject, currentProjectileItemBeingUsed.destroyAfterTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("Hit Enemy");
                Destroy(gameObject);
            }
        }
    }
}
