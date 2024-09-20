namespace Minesweeper.Core.Application
{
    public class PayloadState<TValue> : IPayloadState<TValue>
    {
        public virtual void Enter(TValue value) { }
        public virtual void Exit() { }
        public virtual void Tick(float deltaTime) { }

        public virtual bool Trigger(IFSMCommand command) => false;
        public virtual bool Trigger<TArgs>(IFSMCommand<TArgs> command) => false;
    }
}