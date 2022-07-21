using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private DataEnemy data;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] public GameObject particlesOnDamage;
    private bool canTakeDamage = true;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if player is dead, stop the script from updating
        if(!player){
            return;
        }
        Swarm();
        DamagePlayerCheck();
    }

    private void SetEnemyValues()
    {
        GetComponent<EnemyHealth>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }

    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }


    private void DamagePlayerCheck()
    {
        if(player && canTakeDamage == true)
        {
            if(playerCollider.IsTouching(GetComponent<Collider2D>())){
                canTakeDamage = true;
                playerCollider.GetComponent<Health>().Damage(damage);
                canTakeDamage = false;
                StartCoroutine(canTakeDamageWait());
            }
        }
    }

    private IEnumerator canTakeDamageWait()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }

    //enemy takes damage
    public void TakeDamage(float damage){
        this.GetComponent<EnemyHealth>().Damage((int)damage);
        GameObject blood = (GameObject)Instantiate(particlesOnDamage, transform.position, Quaternion.identity);
        // Destroy(blood, 0.2f);

    }


    // private void OnTriggerEnter2D()
    // {
    //     if (playerCollider)
    //     {
    //         if (playerCollider.GetComponent<Health>() != null)
    //         {
    //             playerCollider.GetComponent<Health>().Damage(damage);
    //             Debug.Log(damage);
    //         }
    //     }
    // }
}
