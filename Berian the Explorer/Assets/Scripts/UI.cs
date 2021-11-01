using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject powerUp;
    public GameObject health1,health2,health3;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = player.totalScore.ToString();

        if (player.hasPowerUp)
        {
            powerUp.SetActive(true);
        }
        else
        {
            powerUp.SetActive(false);
        }

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
