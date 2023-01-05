using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MinorEngine : Engine
{
    private GameObject _parachuteObject;
    private Parachute _parachute;

    public event EventHandler CheckParachuteStatus;

    protected override void Awake()
    {
        base.Awake();
        _parachuteObject = transform.GetChild(0).gameObject;
        _parachute = _parachuteObject.GetComponent<Parachute>();

        _parachuteObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 9)
        {
            _parachute.FallInGround();
        }
    }

    protected override IEnumerator StopEngine()
    {
        yield return StartCoroutine(base.StopEngine());
        
        CheckParachuteStatus?.Invoke(this, EventArgs.Empty);
        _parachuteObject.SetActive(true);
    }

}
