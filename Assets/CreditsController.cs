using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{

    private void Start() 
    {
        StartCoroutine(after20Seconds());
    }
    private void Update()
    {
        //whenever user presses ESC key, bring back to main menu
        if(Input.GetKeyDown(KeyCode.Escape)){
            //load the main menu
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
    //after 20 seconds, bring back the user to the mainmenu
    private IEnumerator after20Seconds()
    {
        yield return new WaitForSeconds(20f);
        //load the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
