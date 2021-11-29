using TME.Scenario.Default.Interfaces;

namespace TME.Scenario.Default.Commands.Interfaces
{
    public interface IDropObjectCommand : ICharacterCommand
    {
        IResult Execute(ICharacter character, IObject carriedObject);
        bool CanExecute(ICharacter character, IObject carriedObject);
    }
}