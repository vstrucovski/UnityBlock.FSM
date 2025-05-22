using System;
using JetBrains.Annotations;

namespace UnityBlocks.FSM
{
    public class TransitionRule
    {
        [CanBeNull] public Type From;
        public Type To;
        public Func<bool> Condition;

        public TransitionRule(Type from, Type to, Func<bool> condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}