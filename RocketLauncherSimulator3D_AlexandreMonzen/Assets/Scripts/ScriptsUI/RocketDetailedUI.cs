using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class RocketDetailedUI : MonoBehaviour
{
    [SerializeField] private Engine _engine;

    [Header("Engine Details")]
    private Text _positionMinorEngineText;
    private Text _velocityMinorEngineText;
    private Text _actualEngineForce;
    private Text _maxAltitudeY;

    private void Awake()
    {
        _positionMinorEngineText = transform.GetChild(0).GetComponent<Text>();
        _velocityMinorEngineText = transform.GetChild(1).GetComponent<Text>();
        _actualEngineForce = transform.GetChild(2).GetComponent<Text>();
        _maxAltitudeY = transform.GetChild(3).GetComponent<Text>();
    }

    private void Update()
    {
        UpdateTextsUI();
    }

    private void UpdateTextsUI()
    {
        _positionMinorEngineText.text = "Position - X: " + _engine.transform.position.x.ToString("n3") +
                             " Y: " + _engine.transform.position.y.ToString("n3") +
                             " Z: " + _engine.transform.position.z.ToString("n3");

        _velocityMinorEngineText.text = "Velocity - X: " + _engine.rigidBody.velocity.x.ToString("n3") +
                             " Y: " + _engine.rigidBody.velocity.y.ToString("n3") +
                             " Z: " + _engine.rigidBody.velocity.z.ToString("n3");

        _actualEngineForce.text = "Actual Engine Force: " + _engine.actualImpulseForce.ToString("n3");

        _maxAltitudeY.text = "Max Altitude Reached (Y): " + _engine.maxAltitudeY.ToString("n3");
    }
}
