using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class WindDetailsUI : MonoBehaviour
{
    [SerializeField] private WindManager _windManager;
    private Text _text;

    private void Awake()
    {
        _text = transform.GetChild(0).GetComponent<Text>();
    }

    private void Start()
    {
        _text.text = "Wind Forces-> X: " + _windManager.globalWindVector.x.ToString("n2") + " Z: " + _windManager.globalWindVector.y.ToString("n2");
    }
}
