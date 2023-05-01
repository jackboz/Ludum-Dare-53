using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float destroyDelay = 5f; // Delay in seconds before destroying the object

    void Start()
    {
        Destroy(gameObject, destroyDelay); // Destroy the object after the specified delay
    }
}