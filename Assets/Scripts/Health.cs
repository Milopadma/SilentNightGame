using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    Vector3 localScale;
    public HealthBar_new healthBar; //for healthbar
    public int MAX_HEALTH = 100;

    keepAliveInScenes _keepAlive;

    void Start()
    {
        //get the keepAlive script
        _keepAlive = GameObject.FindGameObjectWithTag("MainManager").GetComponent<keepAliveInScenes>(); //get the keepalive script
        
        //check if theres health data in the keepAliveInScenes.cs script
        string healthData = _keepAlive.GetData("health");
        if (healthData != null) //if healthData is not null, then the game has already loaded prior
        {
            //parse the returned string to a float of health
            health = int.Parse(healthData);
            Debug.Log("healthData is not null and is " + healthData);
        }
        else //if healthData is null, its the first time the game is loaded
        {
            __initializeHealth();
        }
    }

    private void __initializeHealth()
    {
        health = MAX_HEALTH;
        _keepAlive.AddData("health", health.ToString());
        Debug.Log("healthData is null and is " + health);
    }

    // public void SetHealth(int maxHealth, int health)
    // {
    //     this.MAX_HEALTH = maxHealth;
    //     this.health = health;
    // }

    public void Damage(int amount) // called from Enemy.cs
    {
        if (amount < 0) {throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");}
        this.health = health - amount;
        //also update the health after damaged in Dictionary
        _keepAlive.SetData("health", this.health.ToString());
        //play PlayPLayerHurtSound from PlayerAudioController.cs
        GetComponent<PlayerAudioController>().PlayPlayerHurtSound();
        //check if healthBar object still active
        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < 2; i++)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(.1f);
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
    }

    // public void Heal(int amount)
    // {
    //     if (amount < 0)
    //     {
    //         throw new System.ArgumentOutOfRangeException(" Cannot have negative healing");
    //     }

    //     bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

    //     if (wouldBeOverMaxHealth)
    //     {
    //         this.health = MAX_HEALTH;
    //     }
    //     else
    //     {
    //         this.health += amount;
    //     }
    // }

    void FixedUpdate(){
        if (healthBar != null) {healthBar.SetHealthBar(health, MAX_HEALTH);} //sets the healthBar values
        if (health <= 0){Die();} //dead
    }

    private void Die()
    {
        Debug.Log("Player Died");
        LevelManager.instance.GameOver(); //get the instance of LevelManager and call GameOver()
        gameObject.SetActive(false);
        //reset player health in Dictionary
        _keepAlive.SetData("health", MAX_HEALTH.ToString());
    }
}


