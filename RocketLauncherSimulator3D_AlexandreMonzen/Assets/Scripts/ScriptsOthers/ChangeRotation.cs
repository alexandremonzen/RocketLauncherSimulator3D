using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class ChangeRotation : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private GameObject _rocketLauncher;
    [SerializeField] private Vector3 _vectorRotation;
    private float previousValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(OnChangeRotation);
        previousValue = _slider.value;
    }

    public void OnChangeRotation(float value)
    {
        float delta = value - previousValue;
        _rocketLauncher.transform.Rotate(_vectorRotation * delta * 360, Space.Self);

        previousValue = value;
    }
}
