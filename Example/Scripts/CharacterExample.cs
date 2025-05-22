using UnityBlocks.FSM;
using UnityEngine;

namespace Example.Scripts
{
    public class CharacterExample : MonoBehaviour
    {
        private BaseStateMachine _brain;
        private SharedContext _context;

        private void Start()
        {
            _context = new SharedContext();
            _context.Set("characterName", "Robot_01");
            _context.Set("isGrounded", false);
            _context.Set("transform", transform);
            _context.Set("model", GetComponentInChildren<MeshRenderer>());
            _context.Set("rigidbody", GetComponent<Rigidbody>());
            _context.Set("health", 100f);

            _brain = new BaseStateMachine(_context);
            _brain.SetLogger(GetComponent<StateMachineLogger>());
            _brain.AddState(new SpawnedState());
            _brain.AddState(new IdleState());
            _brain.AddState(new JumpState());
            _brain.AddState(new DeadState());

            _brain.AddDelayedTransition<SpawnedState, IdleState>(1f);
            _brain.AddTransition<IdleState, JumpState>(() => IsGrounded && TriggerJump());
            _brain.AddTransition<JumpState, IdleState>(() => !TriggerJump() && IsGrounded);
            _brain.AddAnyTransition<DeadState>(() => IsDead);

            _brain.Enter<SpawnedState>();
        }

        private bool TriggerJump() => Input.GetKeyDown(KeyCode.Space);

        private bool TriggerDeath() => Input.GetKeyDown(KeyCode.Q);

        private bool IsGrounded => _context.Get<bool>("isGrounded");

        private bool IsDead => _context.Get<float>("health") <= 0;

        private void Update()
        {
            _brain.Update();

            CheckIsGrounded();

            if (TriggerDeath())
            {
                _context.Set("health", -1f);
            }
        }

        private void CheckIsGrounded()
        {
            var isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, 3,
                1 << LayerMask.NameToLayer("Ground"));
            _context.Set("isGrounded", isGrounded);
        }
    }
}