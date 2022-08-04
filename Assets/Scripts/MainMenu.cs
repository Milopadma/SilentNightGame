using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start(){
        //it seems whenever the player goes to mainmenu, the game needs to be unpaused (duh)
        Time.timeScale = 1;
    }

    public void PlayGame()
    {
        //calls the scenemanager class to use LoadScene method and get the current scene, added the +1 to get to the next scene (reffering to the buildIndex in project settings)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame(){
        Debug.Log("game quit");
        //just quits the application when called
        Application.Quit();
    }
}
