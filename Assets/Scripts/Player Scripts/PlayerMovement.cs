
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpPower = 10f;
    public float secondJumpPower = 10f;
    public Transform groundCheckPosition;
    public float radius = 0.3f;
    public LayerMask layerGround;
    
    private Rigidbody rb;
    private bool isGrounded;
    private bool playerJumped;
    private bool canDoubleJump; 
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
       rb.velocity = new Vector3(movementSpeed, rb.velocity.y, 0f);
    }

    void PlayerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround) > 0;
    }
}
