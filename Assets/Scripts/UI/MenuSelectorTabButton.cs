using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class MenuSelectorTabButton : MonoBehaviour
{
    [SerializeField]
    [Required]
    TabButton _tabButton;

    [SerializeField]
    [Required]
    Text _buttonLabel;

    [SerializeField]
    Vector3 _before;

    [SerializeField]
    Vector3 _after;

    void OnEnable()
    {
        _tabButton.OnTabSelected.AddListener(OnSelect);
        _tabButton.OnTabDeselected.AddListener(OnDeselect);
    }

    void OnDisable()
    {
        _tabButton.OnTabSelected.RemoveListener(OnSelect);
        _tabButton.OnTabDeselected.RemoveListener(OnDeselect);
    }

    void OnSelect(TabButton button)
    {
        _buttonLabel.transform.localPosition = _after;
    }

    void OnDeselect(TabButton button)
    {
        _buttonLabel.transform.localPosition = _before;
    }
}