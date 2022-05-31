using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    #region Public Fields
    public int playerHealth;
    public int totalScore;
    public bool hasPowerUp;
    #endregion

    #region Private Fields
    [SerializeField] private ParticleSystem[] effects;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI failScore;
    [SerializeField] private GameObject[] trophy;
    [SerializeField] private GameObject[] trophyProgressBar;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private GameObject berian;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject powerUpPosition;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;

    private ParticleSystem powerUp;   
    private Rigidbody rb;
    private float raduis = 0.4f;
    private float timer = 10f;
    private Vector3 startingPosition;
    private bool isGrounded;
    private float horizontalMove;
    private bool isMovingRight;
    private bool isMovingLeft;
    #endregion

    #region MonoBehaviour Callbacks
    void Start()
    {
        Cursor.visible = false;
        startingPosition = new Vector3(-17, -7, 0);
        playerHealth = 3;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        CheckForPowerUp();

        UpdateTrophy();
        
       
       
        //enable when is bilt for Mobile
        // Move();
        //  MobileMovement();



        //enable when is built for PC 
        Movement();  

        //enable when is built for PC
        Jumping();
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
        if (other.gameObject.CompareTag("Diamond"))
        {
            Instantiate(effects[4], other.gameObject.transform.position, Quaternion.identity);
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
                Instantiate(effects[3], collision.gameObject.transform.position, Quaternion.identity);
                audioSource.PlayOneShot(audioClips[2]);
                collision.gameObject.GetComponent<Enemy>().isDead = true;
                Animator enemyAnim = collision.gameObject.GetComponentInChildren<Animator>();
                enemyAnim.SetBool("isDead", true);
                Destroy(collision.gameObject, 3);
            }
            else
            {
                if (playerHealth > 0)
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

    #endregion

    #region Mobile Input Controls

    private void Move()
    {
        if (isMovingLeft)
        {
            horizontalMove = -speed * Time.deltaTime;
        }
        else if (isMovingRight)
        {
            horizontalMove = speed * Time.deltaTime;
        }
        else
        {
            horizontalMove = 0;
        }
    }

    private void MobileMovement()
    {
        // if the player moves then we turn on the run animation
        if (horizontalMove != 0)
        {
            anim.SetBool("isRunning", true);
            Vector3 movement = new Vector3(horizontalMove, 0, 0);
            berian.transform.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        //move the player on the horizontal axes
        transform.Translate(horizontalMove, 0, 0);
    }
    
    public void LeftButtonDown()
    {
        isMovingLeft = true;
    }
    public void RightButtonDown()
    {
        isMovingRight = true;
    }
    public void LeftButtonUp()
    {
        isMovingLeft = false;
    }
    public void RightButtonUp()
    {
        isMovingRight = false;
    }
    #endregion

    #region PC Input Controls
    public void Movement()
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
    #endregion

    #region Public Methods
    public void Jumping()

  


    {
        #region PC Built
        
        //check if the player on the ground
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, raduis, groundLayer);
        // if the player on the ground then can jump/play jump animation/ play jump sound
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetTrigger("jump");
           // audioSource.PlayOneShot(audioClips[6]);
        }
        
        #endregion

        #region Mobile Built
        /*
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, raduis, groundLayer);
        // if the player on the ground then can jump/play jump animation/ play jump sound
        if ( isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetTrigger("jump");
            audioSource.PlayOneShot(audioClips[6]);
        }
        */
        #endregion


    }
    #endregion

    #region Private Methods
    private void CheckForPowerUp()
    {
        //if the player has power up change the speed of the background music
        if (hasPowerUp)
        {
            audioSource.pitch = 1.3f;
            timer -= Time.deltaTime;
            if (timer <= 0)
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
    }

    private void UpdateTrophy()
    {
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
        else if (totalScore >= 45)
        {
            trophyProgressBar[0].SetActive(true);
        }
    }

    #endregion




}
