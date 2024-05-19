using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class WRLD_RESET_ANIMATOR_BOOL : StateMachineBehaviour
    {
        CharacterManager character; // Reference to the Character Manager script
        override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if(character == null)
            {
                character = animator.GetComponentInParent<CharacterManager>(); 
            }

            // BELOW CODE: This is called when actions ends, and the states return to "empty"
            character.canMove = true;
            character.isGrounded = true;
            character.applyRootMotion = false;
            character.isPerformingAction = false;
        }
    }
}