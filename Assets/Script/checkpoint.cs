using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isFinishLine = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Car")) return;

        LapCounter lapCounter = other.GetComponent<LapCounter>();
        if (lapCounter != null)
        {
            if (isFinishLine)
                lapCounter.CrossFinishLine();
            else
                lapCounter.CheckpointPassed(transform);
        }
    }
}
