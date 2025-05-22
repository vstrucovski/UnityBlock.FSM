using UnityBlocks.FSM;
using UnityEngine;

namespace Example.States
{
    public class JumpState : BaseState
    {
        public override void OnEnter()
        {
            var rb = Context.Get<Rigidbody>("rigidbody");
            rb.AddForce(Vector3.up * 300, ForceMode.Impulse);
        }
    }
}