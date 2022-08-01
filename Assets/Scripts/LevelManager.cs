using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake(){ //stops duplicates
        if(LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
    }

    //GameOver func
    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>(); //get the ui manager script
         if(_ui != null) { //if the ui manager script is not null
            _ui.ToggleDeathPanel(); //toggle the death panel
         }
    }
}
