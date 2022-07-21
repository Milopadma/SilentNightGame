using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //for the event subscription methods of ScoreSystem.cs

public class EnemyHealth : MonoBehaviour
{
    public static event Action Killed = delegate { }; //event for when an enemy is killed
    [SerializeField] public int health = 100;
    public GameObject bloodSpray;
    Vector3 localScale;
    // public HealthBar_new healthBar; //for healthbar

    public int MAX_HEALTH = 100;

    public void SetHealth(int maxHealth, int health)
    {
        this.MAX_HEALTH = maxHealth; //actual health of the enemy
        this.health = health; //actual health of the enemy
        // healthBar.SetHealthBar(health, maxHealth); //just for the healthbar display
    }

    public void Damage(int amount)
    {
        if (amount < 0) {throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");}
        this.health = health - amount;
        Debug.Log("(enemy)damaged by: " + amount);
        StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < 1; i++)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(.1f);
            transform.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
        transform.GetComponent<SpriteRenderer>().color = Color.red;
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

    private void Die()
    {
        Debug.Log("Enemy Dead");
        Instantiate(bloodSpray, transform.position, Quaternion.identity);
        Killed(); //call the global event Killed()
        Destroy(gameObject); //destroy enemy gameobject
    }

    void FixedUpdate(){
        if (health <= 0)
        {
            Die();
        }
    }
}


