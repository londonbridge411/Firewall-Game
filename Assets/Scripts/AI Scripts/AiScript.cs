using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace bowen.AI
{
    public class AiScript : AI
    {
        public NavMeshAgent nav;
        private Vector3 spawnPos;

        private void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            spawnPos = transform.position;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                nav.SetDestination(other.transform.position);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                nav.SetDestination(spawnPos);
            }
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.collider.tag == "Player")
            {
                GameObject player = col.collider.gameObject;
                player.GetComponent<PlayerStats>().health -= DamageRandomizer(GetComponent<AiStats>().Damage, GetComponent<AiStats>().Critdamage, GetComponent<AiStats>().Critchance);
                //StartCoroutine(CameraScript.instance.Shake(10f, 3f, .2f));
            }
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Follow()
        {
            throw new System.NotImplementedException();
        }
    }
}

