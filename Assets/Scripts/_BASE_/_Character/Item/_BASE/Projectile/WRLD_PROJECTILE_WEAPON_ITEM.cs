using UnityEngine;

namespace Nutbusterz.Calamitus
{
    [CreateAssetMenu(menuName = "FFPS/Item/Weapon/Projectile")]
    public class WRLD_PROJECTILE_WEAPON_ITEM : WRLD_ITEM
    {
        [Header("PROJECTILE DATA")]
        public float projectileSpeed = 30f;
        public float destroyAfterTime = 3f;
    }
}
