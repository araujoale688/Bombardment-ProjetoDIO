using UnityEngine;

namespace StateMachine
{
    public class StateMachineScript
    {    
        public string currentStateName {  get; private set; }

        private State currentState;

        public void Update()
        {
            currentState?.Update();
        }

        public void LateUpdate()
        {
            currentState?.LateUpdate();
        }

        public void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        public void ChangeState(State newState)
        {
            currentState?.Exit();

            currentState = newState;
            currentStateName = newState?.name;

            newState?.Enter();
        }
    }
}