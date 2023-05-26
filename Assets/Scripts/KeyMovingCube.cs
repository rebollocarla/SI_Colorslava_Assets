using UnityEngine;

public class KeyMovingCube : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 100f;
    public float jumpForce = 10f;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move horizontally and vertically
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
