using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private AudioClip mouseHover, click;
    [SerializeField] private AudioSource audioSource;


    #endregion

    #region Public Methods
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MouseHover()
    {
        audioSource.PlayOneShot(mouseHover);
    }
    public void MouseClick()
    {
        audioSource.PlayOneShot(click);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
