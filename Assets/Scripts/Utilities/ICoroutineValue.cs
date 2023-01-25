using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoroutineValue<T>
{
    IEnumerator WaitForCoroutine();

    T Value {get;}
}

public static class ValueCoroutine
{
    public static IEnumerator AwaitValueCoroutine<T>(
        this MonoBehaviour subject, 
        ICoroutineValue<T> coroutine,
        System.Func<T, IEnumerator> callback)
    {
        yield return subject.StartCoroutine(coroutine.WaitForCoroutine());
        yield return callback(coroutine.Value);
    }

    // public IEnumerator Run(MonoBehaviour subject, ICoroutineValue<T> coroutine, System.Func<T, IEnumerator> callback)
    // {
    //     yield return subject.StartCoroutine(coroutine.WaitForCoroutine());
    //     yield return callback(coroutine.Value);
    // }
}