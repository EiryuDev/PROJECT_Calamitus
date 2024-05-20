using UnityEngine;

namespace Nutbusterz.Calamitus
{
    [CreateAssetMenu(menuName = "FFPS/Item/AI/AI Data")]
    public class WRLD_AI_ITEM : WRLD_ITEM
    {
        [Header("AI TYPE")]
        [Tooltip("What type of AI is this")]
        public AITypes aiTypes;

        [Header("WEAPON MODEL")]
        [Tooltip("What type of weapon AI is using")]
        public GameObject weaponModel;

        [Header("MOVEMENT DATA")]
        [Tooltip("How much is the movement speed")]
        public float movementSpeed = 5.0f; 
        [Tooltip("How much is the ai height offset or amplitude")]
        public float aiHeight = 1.0f;
        [Tooltip("How much is the ai stopping distance")]
        public float aiStoppingDistance = 2.0f;

        [Header("COMBAT DATA")]
        [Tooltip("How much is the damage of ai attack")]
        public int aiDamage = 1;
        [Tooltip("How much is the range for ai detection")]
        public float aiDetectionRange = 5f;
        [Tooltip("How much is the range for ai attack")]
        public float aiAttackRange = 5f;
        [Tooltip("How much time for ai attack")]
        public float aiAttackTime = 2f;
    }
}
