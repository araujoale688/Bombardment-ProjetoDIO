using ExtensionMethods;
using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class Jump : State
    {
        private PlayerController playerController;

        private bool hasJumped;
        private float cooldown;

        public Jump(PlayerController playerController) : base("Jump")
        {
            this.playerController = playerController;
        }

        public override void Enter()
        {
            base.Enter();

            //Resetar Váriaveis.
            hasJumped = false;
            cooldown = 0.5f;

            //Ativar Animação.
            playerController.animatorPlayer.SetBool("bJumping", true);
        }

        public override void Exit()
        {
            base.Exit();

            playerController.animatorPlayer.SetBool("bJumping", false);
        }

        public override void Update()
        {
            base.Update();

            cooldown -= Time.deltaTime;

            if (hasJumped && playerController.isGrounded)
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

            if (!hasJumped)
            {
                hasJumped = true;

                ApplyImpulse();
            }         
        }

        private void ApplyImpulse()
        {
            Vector3 forceVector = Vector3.up * playerController.jumpPower;

            playerController.rigidbodyPlayer.AddForce(forceVector, ForceMode.Impulse);
        }

        private void MovementJump()
        {
            float moverX = playerController.movementVector.x
                * playerController.movementSpeed;
            float moverZ = playerController.movementVector.y
                * playerController.movementSpeed;

            Vector3 walkVector = new Vector3(moverX, 0, moverZ);
            walkVector = playerController.getForward() * walkVector;

            walkVector *= playerController.movementSpeed * playerController.jumpMovementFactor;

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