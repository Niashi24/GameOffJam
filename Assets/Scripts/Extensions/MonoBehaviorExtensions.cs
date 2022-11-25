using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public static class MonoBehaviourExtensions
{
    public static CoroutineHandle RunCoroutine(this MonoBehaviour owner, IEnumerator coroutine)
    {
        return new CoroutineHandle(owner, coroutine);
    }

    public static ObjectPool<T> CreateMonoPool<T>(this T prefab, Vector3? position = null, Quaternion? rotation = null, Transform parent = null)
        where T : MonoBehaviour
    {
        return new ObjectPool<T>(
            createFunc: () => GameObject.Instantiate<T>(
                prefab,
                position ?? Vector3.zero,
                rotation ?? Quaternion.identity,
                parent
            ),
            actionOnGet: (x) => x.gameObject.SetActive(true),
            actionOnDestroy: (x) => GameObject.Destroy(x.gameObject),
            actionOnRelease: (x) => x.gameObject.SetActive(false)
        );
    }
}