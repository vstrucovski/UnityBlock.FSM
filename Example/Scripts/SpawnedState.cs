using UnityBlocks.FSM;
using UnityEngine;

namespace Example.Scripts
{
    public class SpawnedState : BaseState
    {
        public override void OnEnter()
        {
            Debug.Log(Context.Get<string>("characterName") + " has spawned");
        }
    }
}