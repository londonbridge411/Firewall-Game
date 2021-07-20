using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.BulletHell
{
    public class BHCore : MonoBehaviour
    {
        public BHPattern currentPattern;
        //public Quaternion rotation;

        public bool onCooldown;
        //bool SwitchPattern() (set parent)

        private void Update()
        {
            if (currentPattern.GetParent() == null)
            {
                currentPattern.SetParent(gameObject);
            }

            if (!onCooldown)
            {
                currentPattern.Fire();
                StartCoroutine(Cooldown());
            }
        }

        private IEnumerator Cooldown()
        {
            onCooldown = true;
            yield return new WaitForSeconds(currentPattern.cooldownTime);
            onCooldown = false;
        }
    }
}
