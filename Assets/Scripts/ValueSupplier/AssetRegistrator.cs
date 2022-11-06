using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LS.SearchWindows;
using Sirenix.OdinInspector;

public class AssetRegistrator<T> : MonoBehaviour
{
    [SerializeField]
    [AssetSearch]
    ValueAsset<T> _asset;

    [SerializeField]
    [Required]
    T _actor;

    void Awake() 
    {
        _asset.Value = _actor;    
    }

    void OnDisable() 
    {
        if (_asset.Value.Equals(_actor))
            _asset.Value = default;    
    }
}
