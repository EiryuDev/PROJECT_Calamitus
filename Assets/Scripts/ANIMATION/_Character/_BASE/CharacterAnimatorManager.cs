using UnityEngine;
using System.Collections.Generic;

namespace Nutbusterz.Calamitus
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        [HideInInspector] public CharacterManager character; // Reference to the Character Manager script

        int horizontal; // For horizontal value
        int vertical; // For vertical value

        [Header("DAMAGE ANIMATION")]
        [HideInInspector] public string Damage_Forward_Small_01 = "Damage_Forward_Small_01";
        [HideInInspector] public string Damage_Back_Small_01 = "Damage_Back_Small_01";
        [HideInInspector] public string Damage_Left_Small_01 = "Damage_Left_Small_01";
        [HideInInspector] public string Damage_Right_Small_01 = "Damage_Right_Small_01";

        [HideInInspector] public List<string> Damage_Animations_Small_Forward = new List<string>();
        [HideInInspector] public List<string> Damage_Animations_Small_Backward = new List<string>();
        [HideInInspector] public List<string> Damage_Animations_Small_Left = new List<string>();
        [HideInInspector] public List<string> Damage_Animations_Small_Right = new List<string>();
        
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();

            horizontal = Animator.StringToHash("Horizontal");
            vertical = Animator.StringToHash("Vertical");
        }
        protected virtual void Start()
        {
            Damage_Animations_Small_Forward.Add(Damage_Forward_Small_01);
            //Damage_Animations_Small_Forward.Add(Damage_Forward_Small_02); //If you have variations of Damage Animation do like thid
            Damage_Animations_Small_Backward.Add(Damage_Back_Small_01);
            Damage_Animations_Small_Left.Add(Damage_Left_Small_01);
            Damage_Animations_Small_Right.Add(Damage_Right_Small_01);
        }
        public string GetRandomDamageAnimationFromList(List<string> animationList)
        {
            int randomValue = Random.Range(0, animationList.Count);

            return animationList[randomValue];
            //if (animationList[randomValue] == lastDamageAnimationPlayed) //If you don't want same animation to played you can use this method
        }
        public virtual void OnAnimatorMove()
        {
            if (character.isPerformingAction == false)
                return;

            if (character.applyRootMotion)
            {
                // BELOW CODE: Take the rotation from particular animation and apply to the character rotation
                Vector3 velocity = character.animator.deltaPosition;
                character.controller.Move(velocity);
                character.transform.rotation *= character.animator.deltaRotation;
            }
        }
        public void UpdateAnimatorMovementParameters(float horizontalMovement, float verticalMovement, bool isSprinting)
        {
            // BELOW CODE: Adding the values
            float horizontalAmount = horizontalMovement;
            float verticalAmount = verticalMovement;  

            if(isSprinting)
            {
                verticalAmount = 2;
            }

            character.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
            character.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
            // BELOW CODE: Adding the values (Different method)
            //float snappedHorizontal = 0;
            //float snappedVertical = 0;

            #region Horizontal
            // BELOW CODE: CHAIN AROUND THE HORIZONTAL MOVEMENT
            //if (horizontalMovement > 0 && horizontalMovement <= 0.5f)
            //{
            //    snappedHorizontal = 0.5f;
            //}
            //else if(horizontalMovement > 0.5f && horizontalMovement <= 1)
            //{
            //    snappedHorizontal = 1;
            //}
            //else if(horizontalMovement < 0 && horizontalMovement >= -0.5f)
            //{
            //    snappedHorizontal = -0.5f;
            //}
            //else if(horizontalMovement < -0.5f && horizontalMovement >= -1)
            //{
            //    snappedHorizontal = -1;
            //}
            //else
            //{
            //    snappedHorizontal = 0;
            //}
            #endregion

            #region Vertical
            // BELOW CODE: CHAIN AROUND THE VERTICAL MOVEMENT
            //if (verticalMovement > 0 && verticalMovement <= 0.5f)
            //{
            //    snappedVertical = 0.5f;
            //}
            //else if (verticalMovement > 0.5f && verticalMovement <= 1)
            //{
            //    snappedVertical = 1;
            //}
            //else if (verticalMovement < 0 && verticalMovement >= -0.5f)
            //{
            //    verticalMovement = -0.5f;
            //}
            //else if (verticalMovement < -0.5f && verticalMovement >= -1)
            //{
            //    snappedVertical = -1;
            //}
            //else
            //{
            //    snappedVertical = 0;
            //}

            #endregion
            //character.animator.SetFloat("Horizontal", snappedHorizontal);
            //character.animator.SetFloat("Vertical", snappedVertical);
        }
        public void PlayTargetActionAnimation(
            string targetAnim,
            bool isPerformingAction,
            bool applyRootMotion = true,
            bool canMove = false)
        {
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(targetAnim, 0.2f);
            // BELOW CODE: Can be used to stop character from attempting a new action
            // BELOW CODE: Example if you get damaged and perform damage animation
            // BELOW CODE: The below flag will turn true id player is stunned
            // BELOW CODE: We can then check for the flag before attempting a new action
            character.isPerformingAction = isPerformingAction;
            character.canMove = canMove;
        }
        public void EnableCanMove()
        {
            character.canMove = true;
        }
        public virtual void EnableCombo()
        {
            character.animator.SetBool("canDoCombo", true);
        }
        public virtual void DisableCombo()
        {
            character.animator.SetBool("canDoCombo", false);
        }
        public void EnableIsInvulnerable()
        {
            character.animator.SetBool("isInvulnerable", true);
        }
        public void DisableIsInvulnerable()
        {
            character.animator.SetBool("isInvulnerable", false);
        }
        public void DisableCollision()
        {
            character.controller.enabled = false;
        }
        public void EnableCollision()
        {
            character.controller.enabled = true;
        }
    }
}
