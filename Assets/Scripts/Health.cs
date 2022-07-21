using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public int health = 100;
    Vector3 localScale;

    public HealthBar_new healthBar; //for healthbar

    public int MAX_HEALTH = 100;

    public void SetHealth(int maxHealth, int health)
    {
        this.MAX_HEALTH = maxHealth;
        this.health = health;
    }

    public void Damage(int amount)
    {
        if (amount < 0) {throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");}
        this.health = health - amount;
        Debug.Log("(player)damaged by: " + amount);
        healthBar.SetHealthBar(health, MAX_HEALTH); //just for the healthbar display
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

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException(" Cannot have negative healing");
        }

        bool wouldBeOverMaxHealth = health + amount > MAX_HEALTH;

        if (wouldBeOverMaxHealth)
        {
            this.health = MAX_HEALTH;
        }
        else
        {
            this.health += amount;
        }
    }

    private void Die()
    {
        Debug.Log("You Die");
        LevelManager.instance.GameOver(); //get the instance of LevelManager and call GameOver()
        gameObject.SetActive(false);
    }

    void FixedUpdate(){
        if (health <= 0)
        {
            Die();
        }
    }
}


