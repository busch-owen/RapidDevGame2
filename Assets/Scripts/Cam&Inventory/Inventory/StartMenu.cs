using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip StartSFX;
    public AudioClip ButtonHover;
    public AudioClip ButtonPressed;
    public GameObject OptionsMenu;
    private void Start()
    {
    }
    public void OnPlayButton()
    {
        Source.PlayOneShot(ButtonPressed);
        Source.PlayOneShot(StartSFX);
        StartCoroutine(StartScene());
    }

    public void OnHover()
    {
        Source.PlayOneShot(ButtonHover);
    }

    public void OnOptionsButton()
    {
        Source.PlayOneShot(ButtonPressed);
    }

    public void OnExitButton()
    {
        Source.PlayOneShot(ButtonPressed);
        StartCoroutine(ExitGame());
    }

    //functions
    private IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("TheForrest");
    }

    private IEnumerator OpenOptions()
    {
        yield return new WaitForSeconds(.2f);
        OptionsMenu.SetActive(true);
    }

    private IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(.5f);
        Debug.Log("Exited Game");
        Application.Quit();
    }
}