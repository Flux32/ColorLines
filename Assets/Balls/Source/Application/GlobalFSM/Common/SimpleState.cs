namespace Minesweeper.Core.Application
{
    public abstract class SimpleState : ISimpleState
    {
        public virtual void Enter() { }
        public virtual void Exit() { }

        public virtual bool Trigger(IFSMCommand command) => false;
        public virtual bool Trigger<TArgs>(IFSMCommand<TArgs> command) => false;

        public virtual void Tick(float deltaTime) { }
    }
}