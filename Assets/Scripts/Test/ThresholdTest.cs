using UnityEngine;

public class ThresholdTest : MonoBehaviour
{
    public ThresholdMonitor monitor;

    private void Update()
    {
        monitor.Percent = 0.7f; // Enabled = false
        monitor.UpperThreshold = 0.6f; // Enabled = false
        monitor.LowerThreshold = 0.3f; // Enabled = true
    }
}
