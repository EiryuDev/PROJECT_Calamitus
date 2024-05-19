using UnityEngine;

namespace Nutbusterz.Calamitus
{

    public class AICharacterManager : CharacterManager
    {
        [HideInInspector] public Transform player;  // Reference to the player's transform
        [HideInInspector] public AICharacterLocomotionManager aiCharacterLocomotionManager;
        [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager; 
        [HideInInspector] public AICharacterInventoryManager aiCharacterInventoryManager;
        private AIState currentState = AIState.Patrol;
        protected override void Awake()
        {
            base.Awake();   
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

                // Update state based on distance to player
                if (distanceToPlayer <= aiCharacterInventoryManager.currentAIDataBeingUsed.aiAttackRange)
                {
                    currentState = AIState.Attack;
                }
                else
                {
                    currentState = AIState.Patrol;
                }

                // Perform actions based on the current state
                switch (currentState)
                {
                    case AIState.Attack:
                        UseAttack();
                        break;
                    case AIState.Patrol:
                        UsePatrol();
                        break;
                }

                // Make the object look at the player
                Vector3 lookDirection = player.position - transform.position;
                lookDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }

        void UseAttack()
        {
            aiCharacterCombatManager.AttemptToAttack(Camera.main.transform);
        }

        void UsePatrol()
        {
            aiCharacterLocomotionManager.AttemptToPatrol();
        }
    }
}