using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace bowen.AI
{
    //Base AI Class
    public abstract class AI : MonoBehaviour
    {
        public float abilityReturn;
        public bool canMove;

        private void Update()
        {
            /*if (GameManager.stoppedTime)
                return;*/
        }

        public float DamageRandomizer(float baseDMG, float critDMG, float critRate)
        {
            float chance = critRate; //the lowest number the roll can be to crit. 100-int=actualchance
            float roll = Random.Range(0, 100); //(1, 1) for 100%
            if (roll >= (100 - chance))
            {
                print(roll);
                print("Critical Hit!");
                StartCoroutine(CameraScript.instance.Shake(30f, 10f, .2f));
                return critDMG;
            }
            else
            {
                StartCoroutine(CameraScript.instance.Shake(7f, 3f, .2f));
                return baseDMG;
            }

        }

        public abstract void Follow();
        public abstract void Attack();

        public void OnDisable()
        {
            Score.instance.AddPoints(GetComponent<AiStats>().Points);
            PlayerStats.instance.abilityAmount += abilityReturn;
        }

        public void Rotate(Transform target, float turnSpeed)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        /// <summary>
        /// Sets the AI's destination to it's original position.
        /// </summary>
        public void ReturnPosition()
        {
            if (gameObject.GetComponent<NavMeshAgent>())
            {
                gameObject.GetComponent<NavMeshAgent>().SetDestination(gameObject.GetComponent<AiStats>().startPosition);
            }
        }

        #region Not Finished
        /// <StuffToAdd>
        /// States:
        /// Following : Substates/Functions: Circling, Attack
        /// Wandering: Most likely not implementing
        /// Idle : Substates: Alert => Following, Return back to position
        /// </StuffToAdd>
        #endregion
    }
}

