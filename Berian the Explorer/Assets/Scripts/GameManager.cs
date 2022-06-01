using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public Fields
    public bool isPaused;
    #endregion

    #region Private Fields
    [SerializeField] private GameObject pauseMenu;


    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        CheckForGameState();  
    }
    #endregion

    #region Public Methods
   
    public void Resume()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    #endregion

    #region Private Methods
    private void CheckForGameState()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    #endregion
}
