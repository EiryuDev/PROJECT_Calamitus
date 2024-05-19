using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class AICharacterLocomotionManager : MonoBehaviour
    {
        [HideInInspector] public AICharacterManager aiCharacter;

        [Header("MOVEMENT DATA")]
        private Vector3 startingPos;
        private void Awake()
        {
            aiCharacter = GetComponent<AICharacterManager>();
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
            }           
        }
        private void UseFloatingAIPatrol()
        {
            float yOffset = Mathf.Sin(Time.time * aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.movementSpeed) * aiCharacter.aiCharacterInventoryManager.currentAIDataBeingUsed.aiHeight;
            transform.position = new Vector3(startingPos.x, startingPos.y + yOffset, startingPos.z);
        }
    }
}