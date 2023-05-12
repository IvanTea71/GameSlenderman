using Kino;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnalogGlitch))]
public class CameraGlitch : MonoBehaviour
{
    [SerializeField] private Slenderman _slenderman;

    private AnalogGlitch _cameraGlitch;
    private Coroutine _controlGlitch;

    private void Awake()
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

        while (_cameraGlitch.scanLineJitter != target)
        {
            _cameraGlitch.scanLineJitter = 
                Mathf.MoveTowards(_cameraGlitch.scanLineJitter, target, recoveryRate * Time.deltaTime);
            _cameraGlitch.horizontalShake = 
                Mathf.MoveTowards(_cameraGlitch.horizontalShake, target, recoveryRate * Time.deltaTime);
            _cameraGlitch.colorDrift = 
                Mathf.MoveTowards(_cameraGlitch.colorDrift, target, recoveryRate * Time.deltaTime);

            yield return null;
        }
    }
}
