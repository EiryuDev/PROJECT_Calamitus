using UnityEngine;

namespace Nutbusterz.Calamitus
{
    public class PlayerAnimatorManager : CharacterAnimatorManager
    {
        private PlayerManager player; // Reference to the Player Manager script

        protected override void Awake()
        {
            base.Awake();
            player = GetComponent<PlayerManager>();
        }

        public virtual void OnAnimatorMove()
        {
            if (player.isPerformingAction == false)
                return;

            if (player.applyRootMotion)
            {
                // BELOW CODE: Take the rotation from particular animation and apply to the character rotation
                Vector3 velocity = player.animator.deltaPosition;
                player.playerController.Move(velocity);
                player.transform.rotation *= player.animator.deltaRotation;
            }
        }
        protected override void DisableCollision()
        {
            player.playerController.enabled = false;
        }
        protected override void EnableCollision()
        {
            player.playerController.enabled = true;
        }
    }
}