using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class Slenderman : MonoBehaviour
{
    [SerializeField] private Camera _deathCamera;
    [SerializeField] private Player _player;
    [SerializeField] private float _timer;
    [SerializeField] private float _deathTime;
    [SerializeField] private Point _currentPoint;
    [SerializeField] private GameObject[] _levels;

    private UnityEvent<float> _glitched;
    private int _currentZone;
    private bool _isDefault = true;

    public event UnityAction<float> Glitched
    {
        add => _glitched.AddListener(value);
        remove => _glitched.RemoveListener(value);
    }

    public void ChangeLocation(int zoneNumber)
    {
        _currentZone = zoneNumber;
    }

    private void Update()
    {
        Play();        
    }

    private void SetTeleportTimer()
    {
        int stayTime = 5;

        if (_timer < stayTime)
        {
            _timer += Time.deltaTime;
        }
        else
        {
           Teleporting(_player._score);

            _timer = 0;
        }
    }

    private void SetDeathTimer()
    {
        int deathRange = 40;

        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < deathRange)
        {
            _isDefault = false;
            _deathTime += Time.deltaTime;
        }
        else
        {
            _isDefault = true;
            _deathTime = 0;
        }
    }

    private void Teleporting(int score)
    {        
        if (score == 1)
        {
            CheckingLevel(score);
        }
        else if (score > 1)
        {
            CheckingLevel(score);
            _levels[score - 2].SetActive(false);
        }
    }

    private void CheckingLevel(int score) 
    {
        _levels[score - 1].SetActive(true);
        _levels[score - 1].TryGetComponent<Level>(out Level level);
        ChangePoint(level);
    }
    
    private void ChangePoint(Level level)
    {
        int firstPoint = 0;

        if (_isDefault == true)
        {
            _currentPoint = level._zones[_currentZone]._childrenPoints[Random.Range(firstPoint, level._zones[_currentZone]._childrenPoints.Length)];
            gameObject.transform.position = _currentPoint.transform.position;
        }
        else
        {
            _currentPoint = _player._pointsBehindPlayer[Random.Range(firstPoint, _player._pointsBehindPlayer.Length)];
            gameObject.transform.position = _currentPoint.transform.position;
        }
    }
    
    private void Play()
    {
        int time = 5;

        if (_deathTime < time)
        {
            SetTeleportTimer();
            SetDeathTimer();
        }
        else 
        {
            _deathCamera.enabled = true;
            Destroy(_player);
        }

        gameObject.transform.LookAt(_player.transform);
    }
}
