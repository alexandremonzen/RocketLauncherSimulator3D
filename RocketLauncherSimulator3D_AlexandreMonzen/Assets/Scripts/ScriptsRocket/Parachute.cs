using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class Parachute : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInputActions playerInputActions;
    private Vector2 inputVector;
    private AudioSource _audioSource;

    [SerializeField] private Camera _cameraTransform;

    [Header("On open Parachute Settings")]
    [SerializeField] private float _velocity;
    [SerializeField] private float _minBlowForce;
    [SerializeField] private float _maxBlowForce;

    private bool _bufferRandomForceApply;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerActionMap.Enable();
        _audioSource = GetComponent<AudioSource>();

        _bufferRandomForceApply = false;
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();

        if(_bufferRandomForceApply)
        {
            _bufferRandomForceApply = false;
            ApplyRandomForce();
        }
    }

    private void OnEnable()
    {
        _bufferRandomForceApply = true;
        _audioSource.Play();
    }

    private void HandleInput()
    {
        inputVector = playerInputActions.PlayerActionMap.Movement.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        _rigidbody.AddForce(_cameraTransform.transform.forward * inputVector.y * _velocity, ForceMode.Force);
        _rigidbody.AddForce(_cameraTransform.transform.right * inputVector.x * _velocity, ForceMode.Force);
    }

    private void ApplyRandomForce()
    {
        Vector2 vectorBlow = new Vector2(UnityEngine.Random.Range(_minBlowForce, _maxBlowForce), UnityEngine.Random.Range(_minBlowForce, _maxBlowForce));

        _rigidbody.AddRelativeForce(vectorBlow.x, vectorBlow.y, 0, ForceMode.Impulse);
    }

    public void FallInGround()
    {
        playerInputActions.PlayerActionMap.Disable();
        _audioSource.pitch = 0.5f;
        _audioSource.Play();
    }
}
