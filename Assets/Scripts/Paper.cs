using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Paper : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UnityEvent _reached;

    public event UnityAction Reached
    {
        add => _reached.AddListener(value);
        remove => _reached.RemoveListener(value);
    }
    
    public void TakePaper()
    {
        Destroy(gameObject);
        _player.IncreasScore();
        _reached?.Invoke();
    }
}
