using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoroutineValue<T>
{
    IEnumerator WaitForCoroutine();

    T Value {get;}
}

public class ValueCoroutine<T>
{
    public IEnumerator Run(MonoBehaviour subject, ICoroutineValue<T> coroutine, System.Func<T, IEnumerator> callback)
    {
        yield return subject.StartCoroutine(coroutine.WaitForCoroutine());
        yield return callback(coroutine.Value);
    }
}