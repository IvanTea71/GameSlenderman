using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Zone[] _zones;

    private void Awake()
    {
        _zones = gameObject.GetComponentsInChildren<Zone>();
    }
}
