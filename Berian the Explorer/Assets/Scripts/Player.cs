using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // move speed
    public float speed;
    // jump height
    public float jumpForce;
    
    public LayerMask groundLayer;

    //distance between player and the ground
    private float raduis = 0.4f;
    private bool isGrounded;
    private Rigidbody rb;
    private GameObject groundCheck;

    public bool hasPowerUp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GameObject.Find("GroundCheck");
    }

    void Update()
    {
        Movement();
        Jumping();
    }

    void Movement()
    {
        float horizontalinput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        
        transform.Translate(horizontalinput, 0, 0);
    }
    void Jumping()
    {
        //check if the player on the ground
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, raduis, groundLayer);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerUp)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }


        }
    }
}
