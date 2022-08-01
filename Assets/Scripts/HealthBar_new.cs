using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 

public class HealthBar_new : MonoBehaviour
{
    //using Sliders for healthbar
    public Slider healthBarSlider; //the slider component   
    public Color lowHealth; //color of the healthbar when health is low
    public Color highHealth; //color when the health is high
    public Vector3 Offset; //for the healthbar to be above the player, different heights

    void Awake(){
        SetHealthBar(75f, 100f); //set the healthbar to the current health
    }
    
    public void SetHealthBar(float health, float maxHealth)
    {
        // healthBarSlider.gameObject.SetActive(true); //make the slider invisible when health is less than max health
        healthBarSlider.value = health;
        healthBarSlider.maxValue = maxHealth;
        // healthBarSlider.fillRect.GetComponent<Image>().color = Color.Lerp(lowHealth, highHealth, healthBarSlider.normalizedValue); //change the color of the healthbar depending on the health
    }

    // Update is called once per frame
    void Update()
    {
        // healthBarSlider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset); //move the healthbar to the object's position
    }
}
