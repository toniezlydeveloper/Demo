using Internal.Runtime.Dependencies.Core;

namespace Winning
{
    public abstract class AWinCondition : ADependencyElement<AWinCondition>, IDependency
    {
        public abstract bool IsFulfilled { get; }
    }
}