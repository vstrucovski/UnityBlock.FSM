namespace UnityBlocks.FSM
{
    public class BaseState
    {
        private StateMachineContext _context;

        public StateMachineContext Context => _context;

        public void SetContext(StateMachineContext value)
        {
            _context = value;
        }

        public virtual void Init()
        {
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void Update()
        {
        }
    }
}