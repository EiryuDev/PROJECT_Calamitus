using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Nutbusterz.Calamitus
{

    public class AICharacterManager : CharacterManager
    {
        [HideInInspector] public Transform player;  // Reference to the player's transform
        [HideInInspector] public AICharacterLocomotionManager aiCharacterLocomotionManager;
        [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager; 
        [HideInInspector] public AICharacterInventoryManager aiCharacterInventoryManager;
        [HideInInspector] public AICharacterAnimatorManager aiCharacterAnimatorManager;
        private AIState currentState = AIState.Patrol;

        protected override void Awake()
        {
            base.Awake();   
            player = FindFirstObjectByType<PlayerManager>().transform;
            animator = GetComponentInChildren<Animator>();
            aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();
            aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
            aiCharacterInventoryManager = GetComponent<AICharacterInventoryManager>();  
            aiCharacterAnimatorManager = GetComponent<AICharacterAnimatorManager>();
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
                else if (distanceToPlayer <= aiCharacterInventoryManager.currentAIDataBeingUsed.aiDetectionRange)
                {
                    switch(aiCharacterInventoryManager.currentAIDataBeingUsed.aiTypes)
                    {
                        case AITypes.Grounded:
                            currentState = AIState.Chase;
                            break;
                    }
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
                    case AIState.Chase:
                        UseChase();
                        break;
                    case AIState.Patrol:
                        UsePatrol();
                        break;
                }

                switch (aiCharacterInventoryManager.currentAIDataBeingUsed.aiTypes)
                {
                    case AITypes.Grounded:
                        UpdateAnimator();
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
        void UseChase()
        {
            aiCharacterLocomotionManager.AttemptToChase(player);
        }

        void UsePatrol()
        {
            aiCharacterLocomotionManager.AttemptToPatrol();
        }
        void UpdateAnimator()
        {
            switch (currentState)
            {
                case AIState.Patrol:
                    animator.SetFloat("Speed", 0f);
                    break;
                case AIState.Chase:
                    animator.SetFloat("Speed", 1f);
                    break;
                case AIState.Attack:
                    animator.SetFloat("Speed", 0f);
                    break;
            }
        }
    }
}