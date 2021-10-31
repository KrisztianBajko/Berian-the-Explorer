using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;

    public float jumpForce;
    public float raduis;
    public LayerMask groundLayer;
    public bool isGrounded;

    Rigidbody rb;
    GameObject groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GameObject.Find("GroundCheck");
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, raduis, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        float horizontalinput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;


        transform.Translate(horizontalinput, 0, 0);
    }
}
