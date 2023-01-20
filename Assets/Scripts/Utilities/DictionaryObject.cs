using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DictionaryObject<TKey, TValue> : SerializedScriptableObject
{
    [SerializeField]
    Dictionary<TKey, TValue> _dictionary;
    public Dictionary<TKey, TValue> Dictionary => _dictionary;

    [SerializeField]
    TValue _default;
    public TValue Default => _default;

    public TValue this[TKey key] 
    { 
        get
        {
            if (_dictionary.ContainsKey(key)) return _dictionary[key];
            return _default;
        }
    }

    public ICollection<TKey> Keys => _dictionary.Keys;

    public ICollection<TValue> Values => _dictionary.Values;

    public int Count => _dictionary.Count;

    public void Add(TKey key, TValue value) => _dictionary.Add(key, value);
}