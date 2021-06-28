using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AiStats : MonoBehaviour
{
    public float Health { get; private set; }
    public float Damage { get; private set; } //Physical Damage, not projectile.
    public float Critdamage { get; private set; }
    public float Critchance { get; private set; }
    public int Points { get; private set; }

    public enum EnemyType
    {
        Pawn,
        Bishop,
        Knight
    }

    public enum EnemyRank
    {
        Red,
        Orange,
        Yellow,

    }
    public EnemyType type;
    public EnemyRank rank;

    private void Start()
    {
        //Yellow should be roughly 2x has powerful has red.
        switch (type)
        {
            case EnemyType.Pawn:
                switch (rank)
                {
                    case EnemyRank.Red:
                        SetStats(20, 10, 15, 15, 10);
                        break;
                    case EnemyRank.Orange:
                        SetStats(30, 15, 20, 15, 30);
                        break;
                    case EnemyRank.Yellow:
                        SetStats(50, 20, 30, 15, 75);
                        break;
                }
                break;
            case EnemyType.Bishop:
                switch (rank)
                {
                    case EnemyRank.Red:
                        SetStats(50, 15, 35, 10, 30);
                        break;
                    case EnemyRank.Orange:
                        SetStats(70, 20, 40, 10, 75);
                        break;
                    case EnemyRank.Yellow:
                        SetStats(90, 30, 45, 12.5f, 150);
                        break;
                }
                break;
            case EnemyType.Knight:
                switch (rank)
                {
                    case EnemyRank.Red:
                        SetStats(65, 25, 40, 10, 50);
                        break;
                    case EnemyRank.Orange:
                        SetStats(80, 35, 50, 15, 100);
                        break;
                    case EnemyRank.Yellow:
                        SetStats(120, 45, 60, 25f, 200);
                        break;
                }
                break;
        }
    }

    private void Update()
    {
        if (Health <= 0)
        {
            if (GetComponentInParent<ItemDrop>())
            {
                GetComponentInParent<ItemDrop>().Drop();
            }
            GetComponentInParent<ObjectManager>().DisableObject();
        }
    }

    private void SetStats(float health, float damage, float critdamage, float critrate, int points)
    {
        Health = health;
        Damage = damage;
        Critdamage = critdamage;
        Critchance = critrate;
        Points = points;
    }

    public void PrintStats()
    {
        print(Health);
    }

    public void TakeDamage(float dmgValue)
    {
        Health -= dmgValue;
    }
}

