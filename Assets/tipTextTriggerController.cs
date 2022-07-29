using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipTextTriggerController : MonoBehaviour
{
    //when player touches this box collider, the tip text will appear

    public string _textToShow;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player"){
            GameObject tipText = GameObject.FindGameObjectWithTag("TipText");
            tipText.GetComponent<TipTextController>().__showOnTipText(_textToShow);
        }    
    }
}
