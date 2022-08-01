using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//signed //I GUSTI BAGUS MILO PADMA WIJAYA 

//script not mine, taken from STIKOM Lecturer (Mr. Surya Putra Aditya)

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class DataEnemy : ScriptableObject
{
    public int hp;
    public int damage;
    public float speed;
    //float for enemy range
    public float __enemyRange; //this line is mine, added after.
}
