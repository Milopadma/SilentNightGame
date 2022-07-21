using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{   
    public GameObject parentObject;
    private Health Health;
    private EnemyHealth EnemyHealth;

    Vector3 localScale;
    Vector3 localPosition;
    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
        
        if(parentObject.tag == "Player")
        {
            Health = parentObject.GetComponent<Health>();
        }
        else if(parentObject.tag == "Enemy")
        {
            EnemyHealth = parentObject.GetComponent<EnemyHealth>();
        }
        
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        changeLocalScale();
    }

    void changeLocalScale()
    {
        if(parentObject.tag == "Player")
        {
            localScale.x = Health.health * 0.68f;
            // localPosition.x = -142.678f + (Health.health * 0.68f);
            localPosition.x = -102.7f + (Health.health * 0.8f);
            // localPosition.x = -92.7f;
            localPosition.y = -3.04f;
            transform.localScale = localScale;
            transform.localPosition = localPosition;

        }
        else if(parentObject.tag == "Enemy")
        {
            localScale.x = EnemyHealth.health;
            localPosition.x = -51f + (EnemyHealth.health * 0.5f);
            localPosition.y = -2.8f;
            transform.localScale = localScale;
            transform.localPosition = localPosition;
        }
    }
    
}
