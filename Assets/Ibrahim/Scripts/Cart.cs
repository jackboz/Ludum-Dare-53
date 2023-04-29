using UnityEngine;

public class Cart : MonoBehaviour, IPokeble
{
    Player_Poke poke;
    Rigidbody body;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float strenght = 1000;
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private Transform cube;

    private void Awake()
    {
        body = GetComponentInParent<Rigidbody>();
        poke = FindObjectOfType<Player_Poke>();
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

    public void OnPoke()
    {
        Debug.Log("OnPoke");
        foreach (WheelCollider wheelCollider in wheelColliders)
        {
            float dotProduct = Vector3.Dot(poke.transform.forward, wheelCollider.transform.right);
            // Calculate the steering angle based on the dot product and the maximum steering angle
            float steeringAngle = 60 * dotProduct;
            // Apply the calculated steering angle to the wheel collider
            wheelCollider.steerAngle = steeringAngle;
            Debug.Log(wheelCollider.steerAngle);
        }
        body.AddForce(poke.transform.forward.normalized * strenght, ForceMode.Impulse);
    }
}
