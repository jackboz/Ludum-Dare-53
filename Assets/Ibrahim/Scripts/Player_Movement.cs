using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 input;
    [SerializeField] private float speed;

    private void Start()
    {
         rb = GetComponent<Rigidbody>();    
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("OnMove");
        Vector2 temp = value.Get<Vector2>();
        input = new Vector3(temp.x, 0f, temp.y);
        Debug.Log(input);
    }

    private void Update()
    {  
        rb.velocity = speed * Time.deltaTime * input;
        Vector3 direction =  (transform.position + input)- transform.position;
        if(direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }
}
