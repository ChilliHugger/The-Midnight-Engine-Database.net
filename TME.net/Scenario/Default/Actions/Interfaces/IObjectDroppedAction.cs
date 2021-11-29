using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Actions.Interfaces
{
    public interface IObjectDroppedAction : IObjectAction
    {
        IResult Execute(IObject arg);
        bool CanExecute(IObject arg);
    }

}