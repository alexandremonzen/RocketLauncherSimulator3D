using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class CenterOfMass : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _centerOfMass;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = _centerOfMass;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position + transform.rotation * _centerOfMass, 0.1f);
    }
}
