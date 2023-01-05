using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RocketLauncher : MonoBehaviour
{
    [SerializeField] private MainEngine _maingEngine;
    [SerializeField] private MinorEngine _minorEngine;
    [SerializeField] private GameObject _rocketBase;
    private FixedJoint _fixedJoint;

    private void Awake()
    {
        _fixedJoint = _rocketBase.GetComponent<FixedJoint>();
    }

    public void LaunchRocket()
    {
        _fixedJoint.breakForce = 0;
        _maingEngine.rigidBody.freezeRotation = false;
        _minorEngine.rigidBody.freezeRotation = false;
        _maingEngine.RunEngineMethod(_maingEngine.timeToDeactivateEngine);
    }
}
