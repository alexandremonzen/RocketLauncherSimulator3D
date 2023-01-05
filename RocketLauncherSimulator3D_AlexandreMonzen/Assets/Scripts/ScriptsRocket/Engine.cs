using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Engine : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    private AudioSource _audioSource;

    protected const float _minImpulseForce = 0;

    [Header("General Values")]
    [SerializeField] protected float _maxImpulseForce;
    [SerializeField] protected float _actualImpulseForce;
    [SerializeField] protected float _timeToDeactivateEngine;

    [Header("Smooth Impulse Force")]
    [SerializeField] protected float _timeToReachMaxForce;
    [SerializeField] protected float _timeToReachMinForce;

    [Header("Visuals and SFX")]
    [SerializeField] protected ParticleSystem _fireParticle;
    [SerializeField] protected float _timeFadeSound;
    [SerializeField] protected float _minVolume;
    [SerializeField] protected float _maxVolume;

    protected float _maxAltitudeY;
    protected bool _canCheckAltitude;
    protected bool _checkedAltitude;

    private float _actualFuel;
    private float _maxFuel;

    protected bool _engineOn;
    protected bool _runEngine;

    public Rigidbody rigidBody { get => _rigidbody; private set => _rigidbody = value; }
    public float actualImpulseForce { get => _actualImpulseForce; private set => _actualImpulseForce = value; }
    public float timeToDeactivateEngine { get => _timeToDeactivateEngine; private set => _timeToDeactivateEngine = value; }
    public float maxAltitudeY { get => _maxAltitudeY; }
    public float actualFuel { get => _actualFuel; set => _actualFuel = value; }
    public float maxFuel { get => _maxFuel; set => _maxFuel = value; }

    public event EventHandler UpdateFuelStatus;

    public virtual void RunEngineMethod(float timeToDeactivateEngine)
    {
        StartCoroutine(RunEngine(timeToDeactivateEngine));
    }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _actualImpulseForce = _minImpulseForce;
        _maxFuel = _timeToDeactivateEngine;
        _actualFuel = _timeToDeactivateEngine;

        _rigidbody.freezeRotation = true;
        _canCheckAltitude = false;
        _checkedAltitude = false;

        _audioSource.volume = 0;
    }

    protected void Update()
    {
        UpdateMaxAltitude();
    }

    protected void FixedUpdate()
    {
        if (_runEngine)
        {
            _rigidbody.AddRelativeForce(Vector3.forward * _actualImpulseForce, ForceMode.Force);
        }
    }

    protected virtual IEnumerator RunEngine(float timeToDeactivateEngine)
    {
        if (timeToDeactivateEngine > 0)
        {
            StartCoroutine(StopEngine());
        }

        if (!_fireParticle.isPlaying)
        {
            _fireParticle.Play();
        }

        _engineOn = true;
        _rigidbody.freezeRotation = false;
        _actualImpulseForce = _minImpulseForce;
        StartCoroutine(FadeInSound());

        float tempImpulseForce = _actualImpulseForce;
        float forceElapsedTime = 0;

        while (_engineOn)
        {
            while (forceElapsedTime < _timeToReachMaxForce && _engineOn)
            {
                tempImpulseForce = Mathf.Lerp(tempImpulseForce, _maxImpulseForce, forceElapsedTime / _timeToReachMaxForce);
                forceElapsedTime += Time.deltaTime * 1;
                _actualImpulseForce = tempImpulseForce;

                _runEngine = true;

                _actualFuel -= Time.deltaTime * 1;
                UpdateFuelStatus.Invoke(this, EventArgs.Empty);
                yield return null;
            }

            _runEngine = true;
            _canCheckAltitude = true;
            yield return null;
        }

        yield return null;
    }

    protected virtual IEnumerator StopEngine()
    {
        yield return new WaitForSeconds(_timeToDeactivateEngine);
        _engineOn = false;
        _fireParticle.Stop();
        StartCoroutine(FadeOutSound());
        _runEngine = false;
        StopCoroutine(RunEngine(_timeToDeactivateEngine));

        float tempImpulseForce = _actualImpulseForce;
        float elapsedTime = 0;

        while (elapsedTime < _timeToReachMinForce)
        {
            tempImpulseForce = Mathf.Lerp(tempImpulseForce, _minImpulseForce, elapsedTime / _timeToReachMinForce);
            elapsedTime += Time.deltaTime * 1;
            _actualImpulseForce = tempImpulseForce;
            yield return null;
        }
        _actualImpulseForce = _minImpulseForce;

        _engineOn = false;
        yield return null;
    }

    protected void UpdateMaxAltitude()
    {
        if (_canCheckAltitude)
        {
            if (!_checkedAltitude)
            {
                if (_rigidbody.velocity.y <= 0)
                {
                    _maxAltitudeY = transform.position.y;
                    _checkedAltitude = true;
                }
            }
        }
    }

    private IEnumerator FadeOutSound()
    {
        _audioSource.volume = _maxVolume;
        float actualVolume = _audioSource.volume;
        float tempVolume = actualVolume;
        float elapsedTime = 0;
        

        while (elapsedTime < _timeFadeSound)
        {
            tempVolume = Mathf.Lerp(tempVolume, _minVolume, elapsedTime / _timeFadeSound);
            elapsedTime += Time.deltaTime * 1;
            _audioSource.volume = tempVolume;
            yield return null;
        }
        _audioSource.volume = _minVolume;
        _audioSource.Stop();  
    }

    private IEnumerator FadeInSound()
    {
        _audioSource.volume = _minVolume;
        float tempVolume = _audioSource.volume;
        float elapsedTime = 0;
        _audioSource.Play();

        while (elapsedTime < _timeFadeSound)
        {
            tempVolume = Mathf.Lerp(tempVolume, _maxVolume, elapsedTime / _timeFadeSound);
            elapsedTime += Time.deltaTime * 1;
            _audioSource.volume = tempVolume;
            yield return null;
        }
        _audioSource.volume = _maxVolume;
    }
}
