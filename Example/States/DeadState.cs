using UnityBlocks.FSM;
using UnityEngine;

namespace Example
{
    public class DeadState : BaseState
    {
        public override void OnEnter()
        {
            Debug.Log(Context.Get<string>("characterName") + " dead now");
            var model = Context.Get<MeshRenderer>("model");
            var newMaterial = model.material;
            newMaterial.color = Color.red;
            model.material = newMaterial;
        }
    }
}