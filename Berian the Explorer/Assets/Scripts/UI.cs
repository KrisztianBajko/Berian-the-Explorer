using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UI : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private int sceneIndex;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject powerUp;
    [SerializeField] private GameObject health1,health2,health3;
    [SerializeField] private Player player;
    #endregion


    #region MonoBehaviour Callbacks
    void Update()
    {
        UpdateScore();
        OnPoweredUp();
        UpdateHealthUI();
      
       
    }
    #endregion

    #region Public Methods
    public void StartAgain()
    {
        SceneManager.LoadScene(sceneIndex); 
    }

    public void OnNextButtonClicked()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region Private Methods
    private void UpdateScore()
    {
        //update the score every time we collect a new gem
        score.text = player.totalScore.ToString();
    }

    private void OnPoweredUp()
    {
        //turn on/off the power up icon
        if (player.hasPowerUp)
        {
            powerUp.SetActive(true);
        }
        else
        {
            powerUp.SetActive(false);
        }
    }

    private void UpdateHealthUI()
    {
        //update the lives icons depending on how many times we died
        if (player.playerHealth == 3)
        {
            health3.SetActive(true);
            health2.SetActive(true);
            health1.SetActive(true);
        }
        else if (player.playerHealth == 2)
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


    #endregion
}
