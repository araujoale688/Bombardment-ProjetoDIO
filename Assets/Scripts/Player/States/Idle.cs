using ExtensionMethods;
using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class Idle : State
    {
        private PlayerController playerController;

        public Idle(PlayerController playerController) : base("Idle")
        {
            this.playerController = playerController;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (playerController.hasJumpInput)
            {
                playerController.stateMachinePlayer.ChangeState(playerController.jumpState);

                return;
            }

            if (!playerController.movementVector.IsZero())
            {
                playerController.stateMachinePlayer.ChangeState(playerController.walkingState);

                return;
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}