using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Zone[] _zones { get; private set; }

    private void Awake()
    {
        _zones = gameObject.GetComponentsInChildren<Zone>();
    }
}
