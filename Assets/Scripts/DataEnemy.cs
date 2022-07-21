using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class DataEnemy : ScriptableObject
{
    public int hp;
    public int damage;
    public float speed;
}
