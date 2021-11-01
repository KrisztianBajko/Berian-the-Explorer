using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject powerUp;
    public GameObject health1,health2,health3;
    private Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        //update the score every time we collect a new gem
        score.text = player.totalScore.ToString();
        //turn on/off the power up icon
        if (player.hasPowerUp)
        {
            powerUp.SetActive(true);
        }
        else
        {
            powerUp.SetActive(false);
        }
        //update the lives icons depending on how many times we died
        if (player.playerHealth == 3)
        {
            health3.SetActive(true);
            health2.SetActive(true);
            health1.SetActive(true);
        }
        else if(player.playerHealth == 2)
        {
            health3.SetActive(false);
            health2.SetActive(true);
            health1.SetActive(true);
        }
        else if (player.playerHealth == 1)
        {
            health3.SetActive(false);
            health2.SetActive(false);
            health1.SetActive(true);
        }
        else
        {
            health3.SetActive(false);
            health2.SetActive(false);
            health1.SetActive(false);
        }
    }
    public void StartAgain()
    {
        SceneManager.LoadScene(1); 
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
