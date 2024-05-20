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
                AICharacterManager aiCharacter = collision.gameObject.GetComponentInParent<AICharacterManager>();
                aiCharacter.aiCharacterStatsManager.TakeDamage(currentProjectileItemBeingUsed.projectileDamage);
                Destroy(gameObject);
            }
            else if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
                player.playerStatsManager.TakeDamage(currentProjectileItemBeingUsed.projectileDamage);
                Destroy(gameObject);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                Debug.Log("Hit Ground");
                Destroy(gameObject);
            }
        }
    }
}
