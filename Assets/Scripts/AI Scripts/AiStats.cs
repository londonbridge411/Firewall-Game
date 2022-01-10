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

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        _health = Health;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            if (GetComponentInParent<ItemDrop>())
            {
                GetComponentInParent<ItemDrop>().Drop();
            }
            else
            {
                Death();
            }
        }
    }

    public void TakeDamage(float dmgValue)
    {
        if (GameManager.overclocked)
            _health -= dmgValue* 2f;
        else
            _health -= dmgValue;
    }

    public void ResetAI()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
        _health = Health;
    }

    public delegate void EnemyHandler();

    public event EnemyHandler OnDeath;

    void Death()
    {
        gameObject.SetActive(false);
        OnDeath?.Invoke();
    }
}

