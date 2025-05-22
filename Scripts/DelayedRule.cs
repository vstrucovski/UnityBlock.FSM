using System;

namespace UnityBlocks.FSM
{
    public class DelayedRule
    {
        public Type From;
        public Type To;
        public float Delay;
        public float StartTime;
        public bool Started;
    }
}