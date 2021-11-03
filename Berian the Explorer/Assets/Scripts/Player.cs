using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public ParticleSystem[] effects;
    private ParticleSystem powerUp;
    private GameObject powerUpPosition;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public LayerMask groundLayer;
    public Animator anim;
    private Rigidbody rb;
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI failScore;
    public GameObject[] trophy;
    public GameObject[] trophyProgressBar;
    public Image progressBar;
    public GameObject failScreen;
    public GameObject finishScreen;
    private GameObject groundCheck;
    private GameObject berian;
    // move speed
    public float speed;
    // jump height
    public float jumpForce;
    //distance between player and the ground
    private float raduis = 0.4f;
    // timer for the power up
    private float timer = 10f;
    public int playerHealth;
    public  int totalScore;
    private Vector3 startingPosition;
    public bool hasPowerUp;
    private bool isGrounded;
    void Start()
    {
        powerUpPosition = GameObject.Find("PowerUpPosition");
        Cursor.visible = false;
        startingPosition = new Vector3(-17, -7, 0);
        playerHealth = 3;
        rb = GetComponent<Rigidbody>();
        berian = GameObject.Find("Berian");
        groundCheck = GameObject.Find("GroundCheck");
    }
    void Update()
    {
        //if the player has power up change the speed of the background music
        if (hasPowerUp)
        {
            audioSource.pitch = 1.3f;
            timer -= Time.deltaTime;
            if(timer  <= 0)
            {
                powerUp.Stop();
                audioSource.pitch = 1;
                hasPowerUp = false;
                timer = 8f;
            }
        }
        if (powerUp != null)
        {
            powerUp.transform.position = powerUpPosition.transform.position;
        }
            if (totalScore >= 70)
        {
            trophyProgressBar[0].SetActive(false);
            trophyProgressBar[1].SetActive(false);
            trophyProgressBar[2].SetActive(true);
        }
        else if (totalScore >= 60)
        {
            trophyProgressBar[0].SetActive(false);
            trophyProgressBar[1].SetActive(true);
        }
        else if(totalScore >= 45)
        {
            trophyProgressBar[0].SetActive(true);
        }
        Movement();  
        Jumping();
    }
    void Movement()
    {
        float horizontalinput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        // if the player moves then we turn on the run animation
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
        //move the player on the horizontal axes
        transform.Translate(horizontalinput, 0, 0);
    }
    void Jumping()
    {
        //check if the player on the ground
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, raduis, groundLayer);
        // if the player on the ground then can jump/play jump animation/ play jump sound
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetTrigger("jump");
            audioSource.PlayOneShot(audioClips[6]);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // depend on what we collide we do play different sound effects
        if (other.gameObject.CompareTag("PowerUp"))
        {
            
            //turn on the power up for the player wich allaws the player to kill the enemies by touching them
            audioSource.PlayOneShot(audioClips[1]);
            hasPowerUp = true;
            Instantiate(effects[2], other.gameObject.transform.position, Quaternion.identity);
            powerUp = Instantiate(effects[1], transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Diamond"))
        {
            Instantiate(effects[4],  other.gameObject.transform.position, Quaternion.identity);
            //increase the score whenever we collect a gem
            audioSource.PlayOneShot(audioClips[5]);
            totalScore++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            // turn on the finish screen and show the best score
            audioSource.PlayOneShot(audioClips[0]);
            finalScore.text = "Your Score : " + totalScore.ToString();
            Time.timeScale = 0;
            finishScreen.SetActive(true);
            Cursor.visible = true;
        }
        // depend on our score we turn on the right trophy for the player
        if (other.gameObject.CompareTag("Trophy"))
        {
            if (totalScore >= 70)
            {
                trophy[2].SetActive(true);
            }
            else if (totalScore >= 60)
            {
                trophy[1].SetActive(true);
            }
            else
            {
                trophy[0].SetActive(true);
            }
        }
    } 
    private void OnCollisionEnter(Collision collision)
    {
        // if the player collides with an enemy then the player will die, if the player has power up then the enemy will die instead
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerUp)
            {
                Instantiate(effects[3],collision.gameObject.transform.position , Quaternion.identity);
                audioSource.PlayOneShot(audioClips[2]);
                collision.gameObject.GetComponent<Enemy>().isDead = true;
                Animator enemyAnim = collision.gameObject.GetComponentInChildren<Animator>();
                enemyAnim.SetBool("isDead", true);
                Destroy(collision.gameObject,3);
            }
            else
            {
                if(playerHealth > 0)
                {
                    audioSource.PlayOneShot(audioClips[4]);
                    playerHealth--;
                    transform.position = startingPosition;
                }
                else
                {
                    Cursor.visible = true;
                    audioSource.PlayOneShot(audioClips[3]);
                    Destroy(gameObject);
                    failScreen.SetActive(true);
                    failScore.text = "Your score : " + totalScore.ToString();
                }
            }
        }
    }
    
    
}
