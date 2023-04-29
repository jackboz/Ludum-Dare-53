using UnityEngine;

public class Cart : MonoBehaviour, IPokeble
{
    Rigidbody body;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float strength = 1000;
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private Transform cube;

    private void Awake()
    {
        body = GetComponentInParent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach(var wheelCollider in wheelColliders)
        {
            // Get the current speed of the wheel collider
            float currentSpeed = wheelCollider.radius * wheelCollider.rpm * 2f * Mathf.PI / 60f;
            // Check if the current speed exceeds the maximum speed
            if (currentSpeed > maxSpeed)
            {
                // Calculate the target rpm to limit the speed
                float targetRpm = maxSpeed * 60f / (wheelCollider.radius * 2f * Mathf.PI);
                // Apply the target rpm to the wheel collider
                wheelCollider.motorTorque *= targetRpm / wheelCollider.rpm;
            }
        }
    }

    public void OnPoke(Vector3 impulse)
    {
        Debug.Log("OnPoke");
        body.AddForce(transform.forward.normalized * strength, ForceMode.Impulse);
    }
}
