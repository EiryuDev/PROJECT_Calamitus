using UnityEngine;
using UnityEngine.AI;

namespace Nutbusterz.Calamitus
{
    public class AICharacterLocomotionManager : CharacterLocomotionManager
    {
        [HideInInspector] public AICharacterManager aiCharacter;
        private NavMeshAgent navMeshAgent;

        [Header("MOVEMENT DATA")]
        private Vector3 startingPos;
        protected override void Awake()
        {
            base.Awake();
            aiCharacter = GetComponent<AICharacterManager>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            startingPos = transform.position;
        }
        // Update is called once per frame
        public void AttemptToPatrol()
        {
            switch(aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.aiTypes)
            {
                case AITypes.Floating:
                    UseFloatingAIPatrol();
                    break;
                case AITypes.Grounded:
                    UseGroundedAIPatrol();
                    break;
            }           
        }
        private void UseFloatingAIPatrol()
        {
            float yOffset = Mathf.Sin(Time.time * aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.movementSpeed) * aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.aiHeight;
            transform.position = new Vector3(startingPos.x, startingPos.y + yOffset, startingPos.z);
        }
        private void UseGroundedAIPatrol()
        {
            // Implement patrol logic for grounded AI (e.g., waypoint navigation)
            // This can be a simple back-and-forth or a waypoint system
        }
        public void AttemptToChase(Transform target)
        {
            if (aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.aiTypes == AITypes.Grounded)
            {
                aiCharacter.aiCharacterAnimatorManager.PlayTargetActionAnimation("Walk", false, false, true);
                navMeshAgent.speed = aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.movementSpeed;
                navMeshAgent.stoppingDistance = aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.aiStoppingDistance;
                navMeshAgent.SetDestination(target.position);
            }
        }
    }
}