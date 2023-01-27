using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class DescriptionFieldEvents : MonoBehaviour
{
    [SerializeField]
    [Required]
    DescriptionField _descriptionField;

    [SerializeField]
    BattleManager _battleManager;

    IEnumerator Start()
    {
        // This might be a serialized object for the MonoBehavior
        Test _serializedTest = new();

        yield return ValueCoroutine.AwaitValueCoroutine(_serializedTest, Callback);
        IEnumerator Callback(int s)
        {
            Debug.Log(s);
            yield break;
        }
    }

    private class Test : ICoroutineValue<int>
    {
        private int _value = 0;
        public int Value => _value;

        public IEnumerator WaitForCoroutine()
        {
            //Do stuff/wait to get value
            _value = 5;
            yield break;
        }
    }
}
