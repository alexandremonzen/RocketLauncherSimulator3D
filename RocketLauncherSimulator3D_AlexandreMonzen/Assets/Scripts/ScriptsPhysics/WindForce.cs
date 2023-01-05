using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WindForce : MonoBehaviour
{
    [SerializeField] private WindManager _windManager;
    private Rigidbody _rigidbody;
    private Vector2 _windDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _windDirection = _windManager.globalWindVector;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_windDirection.x, 0, _windDirection.y, ForceMode.Force);
    }
}
