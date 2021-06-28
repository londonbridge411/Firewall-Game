using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.AI
{
    //Base AI Class
    public abstract class AI : MonoBehaviour
    {

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
        }

        public void Rotate(Transform target, float turnSpeed)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
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

