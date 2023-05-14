using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using IJunior.TypedScenes;
using UnityEditor;

public class Slenderman : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _deathTime;
    [SerializeField] private int _currentZone;
    [SerializeField] private Point _currentPoint;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _deathVideo;
    [SerializeField] private GameObject _deathRenderer;

    private static int _waitSeconds = 7;
    private Coroutine _teleport;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(_waitSeconds);

    public event UnityAction<float> Glitched;   

    private void Update()
    {
        Play();        
    }

    private void Start()
    {
        CoroutineControl();
    }

    public void ChangeLocation(int zoneNumber)
    {
        _currentZone = zoneNumber;
    }

    private void CoroutineControl()
    {
        if (_teleport != null)
        {
            StopCoroutine(_teleport);
        }

        _teleport = StartCoroutine(Detain());
    }

    private IEnumerator Detain()
    {
        bool isPlaying = true;

        while (isPlaying)
        {
            Teleport(_player._score);
            yield return _waitForSeconds;
        }
    }

    private void SetDeathTimer()
    {
        int deathRange = 30;
        int maxTarget = 1;
        int minTarget = 0;

        if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < deathRange)
        {            
            _deathTime += Time.deltaTime;
            Glitched?.Invoke(maxTarget);
        }
        else
        {
            _deathTime = 0;
            Glitched?.Invoke(minTarget);
        }
    }

    private void Teleport(int score)
    {
        int firstLevel = 0;
        int secondLevel = 1;
        int onePoint = 1;
        int fourPoint = 4;

        if (score == onePoint)
        {
            SwitchLevel(firstLevel);
        }
        else if (score == fourPoint)
        {
            SwitchLevel(secondLevel);
            _levels[firstLevel].SetActive(false);
        }
    }
    
    private void SwitchLevel(int numberLevel) 
    {
        _levels[numberLevel].SetActive(true);
        _levels[numberLevel].TryGetComponent<Level>(out Level level);
        ChangePoint(level);
    }
    
    private void ChangePoint(Level level)
    {
        int firstPoint = 0;

        _currentPoint = level._zones[_currentZone]._childrenPoints[Random.Range(firstPoint, level._zones[_currentZone]._childrenPoints.Length)];
        gameObject.transform.position = _currentPoint.transform.position;        
    }
    
    private void Play()
    {
        int time = 5;

        if (_deathTime < time)
        {
            SetDeathTimer();
        }
        else 
        {
            _deathVideo.SetActive(true);
            _deathRenderer.SetActive(true);
        }

        gameObject.transform.LookAt(_player.transform);
    }
}
