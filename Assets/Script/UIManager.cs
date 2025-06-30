using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LapCounter lapCounter;
    public TextMeshProUGUI lapText;

    private void Start()
    {
        if (lapCounter != null)
        {
            lapCounter.OnLapCompleted += UpdateLapUI;
            UpdateLapUI(0);
        }
    }

    private void UpdateLapUI(int lap)
    {
        lapText.text = $"Lap: {lap} / {lapCounter.totalLaps}";
    }

    private void OnDestroy()
    {
        if (lapCounter != null)
            lapCounter.OnLapCompleted -= UpdateLapUI;
    }
}
