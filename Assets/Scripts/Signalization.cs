using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signalization : MonoBehaviour
{
    private SignalizationZone[] _signalizationZones;
    private AudioSource _audioSource;
    private bool _isOn;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _signalizationZones = GetComponentsInChildren<SignalizationZone>();
    }

    private void Start()
    {
        Initialize();
    }

    public void UpdateSignalization()
    {
        if (IsDetected())
            SetOn();
        else
            SetOff();
    }
    
    private void Initialize()
    {
        foreach (var signalizationZone in _signalizationZones)
        {
            signalizationZone.Initialize();
        }
    }

    private void SetOff()
    {
        _isOn = false;
    }

    private void SetOn()
    {
        if (_isOn)
            return;

        _isOn = true;
        StartCoroutine(PlaySignalization());
    }

    private bool IsDetected()
    {
        bool isLeastOneDetect = false;
        
        foreach (var signalizationZone in _signalizationZones)
        {
            isLeastOneDetect = isLeastOneDetect || signalizationZone.IsDetecting;
        }

        return isLeastOneDetect;
    }

    private IEnumerator PlaySignalization()
    {
        int maxVolume = 1;
        int minVolume = 0;
        float speed = 1;
        
        _audioSource.Play();
        
        while (_isOn)
        {
            _audioSource.volume = minVolume;

            while (_audioSource.volume < maxVolume)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, maxVolume, Time.deltaTime * speed);
                yield return null;
            }

            while (_audioSource.volume > minVolume)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, minVolume, Time.deltaTime * speed);   
                yield return null;
            }
        }
        
        _audioSource.Stop();
    }
}
