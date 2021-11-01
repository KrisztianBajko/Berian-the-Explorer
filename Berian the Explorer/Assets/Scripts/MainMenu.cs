using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip mouseHover, click;
    public AudioSource audioSource;
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
}
