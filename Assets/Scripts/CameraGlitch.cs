using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnalogGlitch))]
public class CameraGlitch : MonoBehaviour
{
    [SerializeField] private float _scaleGlitch;
    private Slenderman _slenderman;
    private AnalogGlitch _cameraGlitch;
    private Coroutine _controlGlitch;

    private void Start()
    {
        _cameraGlitch = GetComponent<AnalogGlitch>();
    }

    private void OnEnable()
    {
       _slenderman.Glitched += CoroutineControl;
    }

    private void OnDisable()
    {
        _slenderman.Glitched -= CoroutineControl;
    }

    public void CoroutineControl(float target)
    {
        if (_controlGlitch != null)
        {
            StopCoroutine(_controlGlitch);
        }

        _controlGlitch = StartCoroutine(GlitchChanger(target));
    }

    private IEnumerator GlitchChanger(float target)
    {
        float recoveryRate = 0.1f;

        while (_scaleGlitch != target)
        {
            _scaleGlitch = Mathf.MoveTowards(_scaleGlitch, target, recoveryRate * Time.deltaTime);
            _cameraGlitch.scanLineJitter = _scaleGlitch;
            _cameraGlitch.horizontalShake = _scaleGlitch;
            _cameraGlitch.colorDrift = _scaleGlitch;

            yield return null;
        }
    }
}
