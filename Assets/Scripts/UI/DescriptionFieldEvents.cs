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
        yield return Test(Callback);
        IEnumerator Callback()
        {
            yield break;
        }
    }

    IEnumerator Test(System.Func<IEnumerator> test)
    {
        yield return test();
    }
}
