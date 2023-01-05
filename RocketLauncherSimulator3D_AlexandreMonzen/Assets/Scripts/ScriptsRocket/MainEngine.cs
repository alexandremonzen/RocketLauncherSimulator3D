using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MainEngine : Engine
{
    private Engine _noseConeEngine;
    private FixedJoint _fixedJoint;

    [Header("Main Engine Values")]
    [Tooltip("Time to break the connection between rigidbodies")]
    [SerializeField] private float _timeToDetachment;
    
    private bool _hasJointConnected;

    public bool hasJointConnected { get => _hasJointConnected; set => _hasJointConnected = value; }

    public event EventHandler CheckJointConnection;

    protected override IEnumerator RunEngine(float timeToDeactivateEngine)
    {
        StartCoroutine(BreakJoint());

        yield return StartCoroutine(base.RunEngine(timeToDeactivateEngine));
    }

    protected override void Awake()
    {
        base.Awake();
        _fixedJoint = GetComponent<FixedJoint>();
        _noseConeEngine = _fixedJoint.connectedBody.GetComponent<Engine>();
        
        if(_fixedJoint.breakForce > 0)
        {
            hasJointConnected = true;
        }
        else
        {
            hasJointConnected = false;
        }

        CheckJointConnection?.Invoke(this, EventArgs.Empty);
    }

    private void OnJointBreak(float breakForce)
    {
        if (_fixedJoint)
        {
            transform.parent = null;
            _fixedJoint.connectedBody = null;
        }
    }

    private IEnumerator BreakJoint()
    {
        yield return new WaitForSeconds(_timeToDetachment);
        _noseConeEngine.RunEngineMethod(_noseConeEngine.timeToDeactivateEngine);
        
        if (_fixedJoint)
        {
            _fixedJoint.breakForce = 0;
            _hasJointConnected = false;
            CheckJointConnection?.Invoke(this, EventArgs.Empty);
        }

        yield return null;
    }
}
