using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoroutineValue<T>
{
    IEnumerator WaitForCoroutine();

    T Value {get;}
}

public interface ICoroutineValue<TIn, TOut>
{
    IEnumerator WaitForCoroutine(TIn value);

    TOut Value {get;}
}

public static class ValueCoroutine
{
    public static IEnumerator AwaitValueCoroutine<T>(
        this ICoroutineValue<T> coroutine,
        System.Func<T, IEnumerator> callback)
    {
        yield return coroutine.WaitForCoroutine();
        yield return callback(coroutine.Value);
    }

    public static IEnumerator AwaitValueCoroutine<TIn, TOut>(
        this ICoroutineValue<TIn, TOut> coroutine,
        TIn value,
        System.Func<TOut, IEnumerator> callback)
    {
        yield return coroutine.WaitForCoroutine(value);
        yield return callback(coroutine.Value);
    }
}