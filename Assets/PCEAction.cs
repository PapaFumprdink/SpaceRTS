using System.Collections;
using UnityEngine;

public abstract class PCEAction : ScriptableObject
{
    public string ActionName { get; }
    public string ActionDescription { get; }

    public void Perform (PlayerControlledEntity ctx, System.Action FinishAction)
    {
        ctx.StartCoroutine(ActionRoutine(ctx, FinishAction));
    }

    protected abstract IEnumerator ActionRoutine(PlayerControlledEntity ctx, System.Action FinishAction);

    public abstract bool IsValidInContext(PlayerControlledEntity ctx);

    public abstract bool IsEntityValid(PlayerControlledEntity ctx, out string reason);
}
