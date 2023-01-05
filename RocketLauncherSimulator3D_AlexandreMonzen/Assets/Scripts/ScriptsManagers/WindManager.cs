using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class WindManager : MonoBehaviour
{
    [Header("Default")]
    [SerializeField] private Vector2 _globalWindVector;
    
    [Header("Configurable")]
    [SerializeField] private bool _randomWind;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    public Vector2 globalWindVector { get => _globalWindVector; private set => _globalWindVector = value; }

    private void Awake()
    {
        if(_randomWind)
        {
            _globalWindVector = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
    }
}
