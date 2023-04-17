using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Security.Policy;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private int _numberZone;
    [SerializeField] private Slenderman _slender;

    public Point[] _childrenPoints { get; private set; }

    private void Awake()
    {
        _childrenPoints = gameObject.GetComponentsInChildren<Point>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player player))
        {
            _slender.ChangeLocation(_numberZone);
        }
    }
}
