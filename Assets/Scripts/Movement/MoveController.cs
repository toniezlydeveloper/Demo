using Internal.Runtime.Utilities;
using Movement.Composites;
using Movement.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class MoveController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private References references;
        [SerializeField] private Settings settings;
        [SerializeField] private State state;
        
        [Header("Input")]
        [SerializeField] public InputActionReference moveInput;
        [SerializeField] public InputActionReference jumpInput;
        [SerializeField] public InputActionReference dashInput;
        
        [Header("Animation")]
        [SerializeField] public Rigidbody2D playerRigidbody;
        [SerializeField] public Animator playerAnimator;
    
        [Header("Grounding")]
        [SerializeField] public float groundCheckRadius;
        [SerializeField] public LayerMask groundLayer;
        [SerializeField] public Transform groundCheck;

        private Jumper _jumper = new();
        private Dasher _dasher = new();
        private Mover _mover = new();

        private static readonly int HorizontalId = Animator.StringToHash("Horizontal");
        private static readonly int VerticalId = Animator.StringToHash("Vertical");
        private static readonly int GroundedId = Animator.StringToHash("Grounded");
        private static readonly int DashId = Animator.StringToHash("Dash");

        private void Start() => InitControllers();

        private void Update()
        {
            RefreshGrounding(GetIsGrounded());
            ReadInput();
            HandleRealtimeMovement();
            Animate();
        }

        private void FixedUpdate() => HandlePhysicsMovement();

        private bool GetIsGrounded() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        private void RefreshGrounding(bool isGrounded) => state.IsGrounded = isGrounded;

        private void ReadInput()
        {
            state.GotFinishJumpInput = jumpInput.action.WasReleasedThisFrame();
            state.GotJumpInput = jumpInput.action.WasPressedThisFrame();
            state.GotDashInput = dashInput.action.WasPressedThisFrame();
            state.HorizontalInput = moveInput.action.ReadValue<float>();
        }

        private void Animate()
        {
            playerAnimator.SetFloat(HorizontalId, playerRigidbody.velocity.x.AbsoluteValue());
            playerAnimator.SetFloat(VerticalId, playerRigidbody.velocity.y);
            playerAnimator.SetBool(GroundedId, state.IsGrounded);
            playerAnimator.SetBool(DashId, state.IsDashing);
        }

        private void HandleRealtimeMovement()
        {
            _mover.HandleFacingDirection();
            _dasher.HandleDashing();
            _jumper.HandleJumping();
        }

        private void HandlePhysicsMovement() => _mover.HandleHorizontalMovement();

        private void InitControllers()
        {
            _jumper.Init(references, settings, state);
            _dasher.Init(references, settings, state);
            _mover.Init(references, settings, state);
        }
    }
}