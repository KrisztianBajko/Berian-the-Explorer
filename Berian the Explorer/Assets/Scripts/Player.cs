using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    public int playerHealth;
    public  int totalScore;
    private Vector3 startingPosition;
    // move speed
    public float speed;
    // jump height
    public float jumpForce;

    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI failScore;
    public LayerMask groundLayer;
    public Animator anim;
    //distance between player and the ground
    private float raduis = 0.4f;
    private bool isGrounded;
    private Rigidbody rb;
    private GameObject groundCheck;
    private GameObject berian;

    public float timer = 8f;
    public bool hasPowerUp;

    public GameObject failScreen;
    public GameObject finishScreen;
    void Start()
    {
        Cursor.visible = false;
        startingPosition = new Vector3(-17, -7, 0);
        playerHealth = 3;
        rb = GetComponent<Rigidbody>();
        berian = GameObject.Find("Berian");
        groundCheck = GameObject.Find("GroundCheck");
    }

    void Update()
    {
        
        if (hasPowerUp)
        {
            timer -= Time.deltaTime;
            if(timer  <= 0)
            {
                hasPowerUp = false;
                timer = 8f;
            }
        }
        Movement();  
        Jumping();
    }

    void Movement()
    {
        float horizontalinput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

       
        if (horizontalinput != 0 )
        {
            anim.SetBool("isRunning", true);
            Vector3 movement = new Vector3(horizontalinput, 0, 0);
            berian.transform.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
            

        transform.Translate(horizontalinput, 0, 0);
    }
    void Jumping()
    {
        //check if the player on the ground
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, raduis, groundLayer);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetTrigger("jump");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Diamond"))
        {
            totalScore++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            finalScore.text = "Your Score : " + totalScore.ToString();
            Time.timeScale = 0;
            finishScreen.SetActive(true);
            Cursor.visible = true;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerUp)
            {
                collision.gameObject.GetComponent<Enemy>().isDead = true;
                Animator enemyAnim = collision.gameObject.GetComponentInChildren<Animator>();
                enemyAnim.SetBool("isDead", true);
                Destroy(collision.gameObject,3);
            }
            else
            {
                if(playerHealth > 0)
                {
                    playerHealth--;
                    transform.position = startingPosition;
                }
                else
                {

                    Destroy(gameObject);
                    failScreen.SetActive(true);
                    failScore.text = "Your score : " + totalScore.ToString();

                }
                
               
            }


        }
    }
}
