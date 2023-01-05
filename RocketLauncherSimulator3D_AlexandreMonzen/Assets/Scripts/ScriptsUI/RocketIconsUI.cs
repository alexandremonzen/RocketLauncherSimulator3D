using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class RocketIconsUI : MonoBehaviour
{
    [Header("Engines")]
    [SerializeField] private MainEngine _mainEngine;
    [SerializeField] private MinorEngine _minorEngine;

    [Header("Joint Connection")]
    [SerializeField] private Sprite _connectedJointImage;
    [SerializeField] private Color _connectedColor;
    [SerializeField] private Sprite _disconnectedJointImage;
    [SerializeField] private Color _disconnectedColor;
    [SerializeField] private Image _actualJointImage;

    [Header("Fuel Status")]
    [SerializeField] private Image _fillFuelMainEngine;
    [SerializeField] private Image _fillFuelMinorEngine;

    [Header("Parachute Status")]
    [SerializeField] private Image _actualParachuteImage;
    [SerializeField] private Color _readyParachute;
    [SerializeField] private Color _usingParachute;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnscribeEvents();
    }

    private void SubscribeEvents()
    {
        _mainEngine.CheckJointConnection += ChangeJointStatusIcon;
        _minorEngine.CheckParachuteStatus += ChangeParachuteIcon;

        _mainEngine.UpdateFuelStatus += UpdateFuelMainEngineUI;
        _minorEngine.UpdateFuelStatus += UpdateFuelMinorEngineUI;
    }

    private void UnscribeEvents()
    {
        _mainEngine.CheckJointConnection -= ChangeJointStatusIcon;
        _minorEngine.CheckParachuteStatus -= ChangeParachuteIcon;

        _mainEngine.UpdateFuelStatus -= UpdateFuelMainEngineUI;
        _minorEngine.UpdateFuelStatus -= UpdateFuelMinorEngineUI;
    }

    private void ChangeJointStatusIcon(object sender, EventArgs e)
    {
        ChangeActualJointImage();
    }

    private void UpdateFuelMinorEngineUI(object sender, EventArgs e)
    {
        _fillFuelMinorEngine.fillAmount = _minorEngine.actualFuel / _minorEngine.maxFuel;
    }

    private void UpdateFuelMainEngineUI(object sender, EventArgs e)
    {
        _fillFuelMainEngine.fillAmount = _mainEngine.actualFuel / _mainEngine.maxFuel;
    }

    private void ChangeParachuteIcon(object sender, EventArgs e)
    {
        ChangeActualParachuteImage();
    }

    private void ChangeActualJointImage()
    {
        if (_mainEngine.hasJointConnected)
        {
            _actualJointImage.sprite = _connectedJointImage;
            _actualJointImage.color = _connectedColor;
        }
        else
        {
            _actualJointImage.sprite = _disconnectedJointImage;
            _actualJointImage.color = _disconnectedColor;
        }
    }

    private void ChangeActualParachuteImage()
    {
        _actualParachuteImage.color = _usingParachute;
    }
}
