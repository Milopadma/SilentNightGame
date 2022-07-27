using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipTextController : MonoBehaviour
{
    private string _tip;
    private Animator _anim;
    TextMeshProUGUI _thisText;
    //other gameObjects can call on an instance of this script and show it in this gameObject's TextMeshPro component

    void Awake()
    {
        //when this script is awake, get the animator component from this GameObject
        _anim = GetComponent<Animator>();
        //hide the tip text by setting text Alpha to 0
        _thisText = this.GetComponent<TextMeshProUGUI>();
    }

    public void __showOnTipText(string _tip)
    {
        //set the text of the TMProUGUI to the current objective
        _thisText.text = _tip;
        //show the tip text by setting Text Alpha to 1
        _thisText.alpha = 1;
        StartCoroutine(playTipTextAnimations());
    }
    IEnumerator playTipTextAnimations() //show tipTextShow animation clip and after 8 seconds, play tipTextShow animation clip
    {
        _anim.Play("tipTextShow");
        yield return new WaitForSeconds(8);
        _anim.Play("tipTextHide");
        yield return new WaitForSeconds(1);
    }
}

//note. double underscore functions means the function only runs once when called, evading Update()s