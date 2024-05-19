using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class AICharacterManager : CharacterManager
    {
        [HideInInspector] public Transform player;  // Reference to the player's transform
        [HideInInspector] public AICharacterLocomotionManager aiCharacterLocomotionManager;
        [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager; 
        [HideInInspector] public AICharacterInventoryManager aiCharacterInventoryManager;
        protected override void Awake()
        {
            player = FindFirstObjectByType<PlayerManager>().transform;
            aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();
            aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
            aiCharacterInventoryManager = GetComponent<AICharacterInventoryManager>();  
        }
        void Update()
        {
            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (distanceToPlayer <= aiCharacterInventoryManager.currentAIDataBeingUsed.aiAttackRange)
                {
                    // Player is in range, engage combat
                    aiCharacterCombatManager.AttemptToAttack(Camera.main.transform);
                }
                else
                {
                    // Patrol
                    aiCharacterLocomotionManager.AttemptToPatrol();
                }

                // Make the object look at the player
                Vector3 lookDirection = player.position - transform.position;
                lookDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }
}