using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.AI;
using UnityEngine;
using bowen.AI;

public class AiStats : MonoBehaviour
{
    public float Health;
    public float Damage;
    public float Critdamage;
    public float Critchance;
    public int Points;

    public Vector3 startPosition { get; private set; }
    private Quaternion startRotation;

    //private Vector3 startPosition;
    private float _health;

    public enum EnemyRank
    {
        Red,
        Orange,
        Yellow,
        Boss,
    }

    public EnemyRank rank;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        _health = Health;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            if (GetComponentInParent<ItemDrop>())
            {
                GetComponentInParent<ItemDrop>().Drop();
            }

            if (rank == EnemyRank.Boss)
            {
                GetComponentInParent<ObjectManager>().DisableObject();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void PrintStats()
    {
        print(Health);
    }

    public void TakeDamage(float dmgValue)
    {
        if (GameManager.overclocked)
            Health -= dmgValue* 2f;
        else
            Health -= dmgValue;
    }

    public void ResetAI()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
        Health = _health;
    }
}

