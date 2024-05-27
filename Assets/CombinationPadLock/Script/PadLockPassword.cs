using System;
using System.Linq;
using Runtime.TaskStateSystem;
using Runtime.TaskStateSystem.TaskStates;
using Runtime.TaskSystem;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    MoveRuller _moveRull;

    public int[] _numberPassword = {0,0,0,0};
    public event Action OnPasswordCorrect;

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
    }
    
    public void Password()
    {
        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            // Notify subscribers that the password is correct
            OnPasswordCorrect?.Invoke();

            Debug.Log("Password correct");
            Debug.Log("OPEN");

            // Disable Blinking Material after the correct password
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
            }
        }
    }
}