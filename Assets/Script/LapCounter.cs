using System;
using System.Collections.Generic;
using UnityEngine;

public class LapCounter : MonoBehaviour
{
    [Header("Lap Settings")]
    public int totalLaps = 3;
    public string CarTag = "Car";

    private int currentLap = 0;
    private HashSet<Transform> passedCheckpoints = new HashSet<Transform>();
    private bool finishLineCrossed = false;

    public event Action<int> OnLapCompleted;

    public void CheckpointPassed(Transform checkpoint)
    {
        if (!passedCheckpoints.Contains(checkpoint))
        {
            passedCheckpoints.Add(checkpoint);
        }
    }

    public void CrossFinishLine()
    {
        if (!finishLineCrossed && passedCheckpoints.Count > 0)
        {
            currentLap++;
            Debug.Log("Giro " + currentLap + " completato!");

            OnLapCompleted?.Invoke(currentLap);

            if (currentLap >= totalLaps)
            {
                Debug.Log("Gara completata!");
            }

            passedCheckpoints.Clear();
            finishLineCrossed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(CarTag))
        {
            finishLineCrossed = false;
        }
    }

    public int GetCurrentLap() => currentLap;
}
