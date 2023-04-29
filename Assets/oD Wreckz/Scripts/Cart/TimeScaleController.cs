using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            Time.timeScale = 0f; // Set the time scale to 0
        }
    }
}