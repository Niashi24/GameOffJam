using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _players;
    public List<GameObject> Players => _players;

    [SerializeField]
    [Required]
    PlayerInput _input;

    [SerializeField]
    int _initalActiveIndex;

    [ShowInInspector, ReadOnly]
    int _currentlyActiveIndex = -1;

    public int CurrentlyActiveIndex => _currentlyActiveIndex;

    void OnEnable() 
    {
        _input.OnPress1 += Press1;
        _input.OnPress2 += Press2;
        _input.OnPress3 += Press3;

        SwitchToCharacter(_initalActiveIndex);
    }

    void OnDisable()
    {
        _input.OnPress1 -= Press1;
        _input.OnPress2 -= Press2;
        _input.OnPress3 -= Press3;
    }

    void Press1()
    {
        SwitchToCharacter(0);
    }

    void Press2()
    {
        SwitchToCharacter(1);
    }

    void Press3()
    {
        SwitchToCharacter(2);
    }

    public void SwitchToCharacter(int index)
    {
        if (index < 0 || index >= _players.Count)
        {
            Debug.LogWarning($"No character exists at index {index}", this);
            return;
        }

        if (index == _currentlyActiveIndex) return;

        ResetCharacters();
        
        _players[index].SetActive(true);

        if (_currentlyActiveIndex != -1)
            _players[index].transform.position = _players[_currentlyActiveIndex].transform.position;

        _currentlyActiveIndex = index;
    }

    void ResetCharacters()
    {
        foreach (var character in _players)
        {
            character.SetActive(false);
        }
    }
}
