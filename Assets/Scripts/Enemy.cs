using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached to the enemy prefab
//script taken from one of my other games, so some of the variables may not be used
//such as health, since in this game, the enemy cant be damaged because that's not the point of the game

public class Enemy : MonoBehaviour
{
    private int damage;
    private float speed;
    private float __enemyRange;
    [SerializeField] private DataEnemy data;
    [SerializeField] private Collider2D playerCollider;
    // [SerializeField] public GameObject particlesOnDamage;
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

        //Enemy behavior
        if(player && Vector3.Distance(transform.position, player.transform.position) < __enemyRange)
            {Swarm();}
        else{StartCoroutine(Patrol());}


        DamagePlayerCheck();
    }

    private void SetEnemyValues()
    {
        GetComponent<EnemyHealth>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
        __enemyRange = data.__enemyRange;
    }

    //lock to player if player is in range
    private void Swarm()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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

    private IEnumerator Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + Random.Range(-10f, 10f), transform.position.y + Random.Range(-10f, 10f), transform.position.z), speed / 2 * Time.deltaTime);
        yield return new WaitForSeconds(5f);
    }

    //enemy takes damage
    // public void TakeDamage(float damage){
    //     this.GetComponent<EnemyHealth>().Damage((int)damage);
    // }
}
