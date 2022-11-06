using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionCoroutine<T>
{
    List<Func<T, IEnumerator>> asyncActions;
    public Action<T> Action;

    public void Subscribe(Func<T, IEnumerator> action)
    {
        if (asyncActions is null)
            asyncActions = new();
        asyncActions.Add(action);
    }

    public void Unsubscribe(Func<T, IEnumerator> action)
    {
        if (asyncActions is null)
            asyncActions = new();
        asyncActions.Remove(action);
    }

    public void Clear()
    {
        Action = null;
        if (asyncActions is not null)
            asyncActions.Clear();
    }

    public IEnumerator Invoke(T value)
    {
        Action?.Invoke(value);

        foreach (var aAction in asyncActions)
        {
            yield return aAction.Invoke(value);
        }
    }
}
