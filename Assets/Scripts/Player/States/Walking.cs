using ExtensionMethods;
using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class Walking : State
    {
        private PlayerController playerController;

        public Walking(PlayerController playerController) : base("Walking")
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

            if (playerController.movementVector.IsZero())
            {
                playerController.stateMachinePlayer.ChangeState(playerController.idleState);

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

            Movement();

            playerController.RotateBodyToFaceInput();
        }

        void Movement()
        {
            float moverX = playerController.movementVector.x 
                * playerController.movementSpeed;

            float moverZ = playerController.movementVector.y 
                * playerController.movementSpeed;

            Vector3 walkVector = new Vector3(moverX, 0, moverZ);

            walkVector = playerController.getForward() * walkVector;

            //Verificar se o player está andando na diagonal.
            if (moverX != 0 && moverZ != 0)
            {
                playerController.rigidbodyPlayer.AddForce(walkVector / 2, ForceMode.Force);
            }
            else
            {
                playerController.rigidbodyPlayer.AddForce(walkVector, ForceMode.Force);
            }
        }
    }
}