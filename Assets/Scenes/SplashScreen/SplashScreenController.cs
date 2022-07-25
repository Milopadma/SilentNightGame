using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    public GameObject logoCanvas;
    public GameObject splashCanvas;

    public Animator _animSubtitle;
    public Animator _animLoadBar;

    // awake is called before the first frame update
    void Awake()
    {
        StartCoroutine(loadSplashScreen());
    }

    // Update is called once per frame
    void Update()
    {
        //if user presses spacebar, load MainMenu scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadMainMenu());
        }
    }

    IEnumerator loadSplashScreen()
    {
        //wait for 2 seconds
        logoCanvas.SetActive(false);
        yield return new WaitForSeconds(3f);
            //disable Text+HELPLogo gameObject
            splashCanvas.SetActive(false);
            //enable LogoCanvas gameObject
            logoCanvas.SetActive(true);
    }

    IEnumerator LoadMainMenu()
    {
        //play loadScene animation
        _animSubtitle.Play("spaceBarPressed");
        _animLoadBar.Play("onLoad");
        //wait for 2 seconds
        yield return new WaitForSeconds(2f);
        //load MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }
}
