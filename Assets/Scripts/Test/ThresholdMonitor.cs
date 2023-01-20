using UnityEngine;

[System.Serializable]
public class ThresholdMonitor
{
    [SerializeField, Range(0.0f, 1.0f)]
    private float _upperThreshold;
    public float UpperThreshold
    {
        get { return _upperThreshold; }
        set
        {
            _upperThreshold = value;
            CheckThresholds();
        }
    }

    [SerializeField, Range(0.0f, 1.0f)]
    private float _lowerThreshold;
    public float LowerThreshold
    {
        get { return _lowerThreshold; }
        set
        {
            _lowerThreshold = value;
            CheckThresholds();
        }
    }

    [SerializeField, Range(0.0f, 1.0f)]
    private float _percent;
    public float Percent
    {
        get { return _percent; }
        set
        {
            _percent = value;
            CheckThresholds();
        }
    }

    [SerializeField]
    private bool _enabled;
    public bool Enabled
    {
        get { return _enabled; }
        set
        {
            _enabled = value;
            CheckThresholds();
        }
    }

    public ThresholdMonitor(float upperThreshold, float lowerThreshold, bool defaultEnabled = false, float defaultPercent = 0.0f)
    {
        UpperThreshold = upperThreshold;
        LowerThreshold = lowerThreshold;
        _enabled = defaultEnabled;
        _percent = defaultPercent;
    }

    private void CheckThresholds()
    {
        if (_percent >= UpperThreshold || _enabled)
        {
            _enabled = true;
        }
        else if (_percent <= LowerThreshold || !_enabled)
        {
            _enabled = false;
        }
    }
}

