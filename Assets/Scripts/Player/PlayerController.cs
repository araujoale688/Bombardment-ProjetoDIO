using ExtensionMethods;
using Player.States;
using StateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Public Propeties
        public float movementSpeed = 10f;
        public float jumpPower = 10f;
        public float jumpMovementFactor = 1f;
        #endregion

        #region State Machine
        [HideInInspector]
        public StateMachineScript stateMachinePlayer;

        [HideInInspector]
        public Idle idleState;

        [HideInInspector]
        public Walking walkingState;

        [HideInInspector]
        public Jump jumpState;

        [HideInInspector]
        public Dead deadState;
        #endregion

        #region Internal Propeties
        [HideInInspector]
        public Vector2 movementVector;

        [HideInInspector]
        public Rigidbody rigidbodyPlayer;

        [HideInInspector]
        public Animator animatorPlayer;

        public Collider colliderPlayer;

        [HideInInspector]
        public bool hasJumpInput;

        [HideInInspector]
        public bool isGrounded;
        #endregion

        void Awake()
        {
            rigidbodyPlayer = GetComponent<Rigidbody>();
            animatorPlayer = GetComponent<Animator>();
            colliderPlayer = GetComponent<Collider>();
        }

        void Start()
        {
            stateMachinePlayer = new StateMachineScript();

            idleState = new Idle(this);
            walkingState = new Walking(this);
            jumpState = new Jump(this);
            deadState = new Dead(this);

            stateMachinePlayer.ChangeState(idleState);
        }

        void Update()
        {
            if (GameManager.Instance.isGameOver)
            {
                if(stateMachinePlayer.currentStateName != deadState.name)
                {
                    stateMachinePlayer.ChangeState(deadState);
                }
            }

            InputMovement();

            //Input de Pulo.
            hasJumpInput = Input.GetKeyDown(KeyCode.Space);

            DetectGround();

            stateMachinePlayer.Update();
        }

        void LateUpdate()
        {
            stateMachinePlayer.LateUpdate();
        }

        void FixedUpdate()
        {
            stateMachinePlayer.FixedUpdate();
        }

        private void InputMovement()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputZ = Input.GetAxis("Vertical");

            movementVector = new Vector2(inputX, inputZ);

            //velocidade para o Animator.
            float velocity = rigidbodyPlayer.velocity.magnitude;
            float velocityRate = velocity / movementSpeed;
            animatorPlayer.SetFloat("fVelocity", velocityRate);
        }

        public Quaternion getForward()
        {
            Camera camera = Camera.main;

            return Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        }

        public void RotateBodyToFaceInput()
        {
            if (movementVector.IsZero())
            {
                return;
            }

            Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
            Camera camera = Camera.main;

            Quaternion quaternion1 = Quaternion.LookRotation(inputVector, Vector3.up);
            Quaternion quaternion2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);

            Quaternion toRotation = quaternion1 * quaternion2;
            Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.15f);

            rigidbodyPlayer.MoveRotation(newRotation);
        }

        private void DetectGround()
        {
            isGrounded = false;

            Vector3 origin = transform.position;
            Vector3 direction = Vector3.down;
            Bounds bounds = colliderPlayer.bounds;
            float radius = bounds.size.x * 0.33f;
            float maxDistance = bounds.size.y * 0.05f;

           if(Physics.SphereCast(origin, radius, direction, out var hitInfo, maxDistance))
           {
                GameObject hitObject = hitInfo.transform.gameObject;

                if (hitObject.CompareTag("Platform"))
                {
                    isGrounded = true;
                }
           }
        }
    }
}