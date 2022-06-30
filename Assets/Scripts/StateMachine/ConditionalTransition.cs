
public abstract class ConditionalTransition : Transition
{
    private void Update()
    {
        if (NeedTransit == false)
            NeedTransit = CheckCondition();
    }

    protected abstract bool CheckCondition();
}
