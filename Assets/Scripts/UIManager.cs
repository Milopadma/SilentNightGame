using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;

    public void ToggleDeathPanel(){
        deathPanel.SetActive(!deathPanel.activeSelf); //turn this to whatever it currently isnt
    }
}
