using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class AICharacterAnimatorManager : CharacterAnimatorManager
    {
        [HideInInspector] public AICharacterManager aiCharacter;

        protected override void Awake()
        {
            base.Awake();
            aiCharacter = GetComponent<AICharacterManager>();
        }
    }
}
