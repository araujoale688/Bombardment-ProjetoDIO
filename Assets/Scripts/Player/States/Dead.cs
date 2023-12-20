using ExtensionMethods;
using StateMachine;
using UnityEngine;

namespace Player.States
{
    public class Dead : State
    {
        private PlayerController playerController;

        public Dead(PlayerController playerController) : base("Dead")
        {
            this.playerController = playerController;
        }

        public override void Enter()
        {
            base.Enter();
            playerController.animatorPlayer.SetTrigger("tGameOver");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
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