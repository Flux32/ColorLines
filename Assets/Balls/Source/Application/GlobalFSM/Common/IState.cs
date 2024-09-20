using System.Windows.Input;

namespace Minesweeper.Core.Application
{
    public interface IState
    {
        public bool Trigger(IFSMCommand command);
        public bool Trigger<TArgs>(IFSMCommand<TArgs> command);
    }
}