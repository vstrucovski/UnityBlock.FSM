namespace UnityBlocks.FSM
{
    public class BaseState
    {
        private SharedContext _context;

        public SharedContext Context => _context;

        public void SetContext(SharedContext value)
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