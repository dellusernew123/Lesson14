namespace _Project.Scripts.Gameplay.Core.Conditions
{
    public interface ICompositeCondition : ICondition
    {
        ICompositeCondition Add(ICondition condition);

        ICompositeCondition Remove(ICondition condition);
    }
}