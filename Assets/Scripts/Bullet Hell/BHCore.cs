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
        public bool canFire;
        //bool SwitchPattern() (set parent)

        private void Update()
        {
            if (currentPattern.GetParent() == null)
            {
                currentPattern.SetParent(gameObject);
            }

            if (!onCooldown && canFire)
            {
                currentPattern.Fire();
                StartCoroutine(Cooldown());
            }
        }

        private IEnumerator Cooldown()
        {
            float startTime = 0;
            onCooldown = true;

            while (startTime < currentPattern.cooldownTime)
            {
                while (GameManager.stoppedTime)
                {
                    yield return new WaitUntil(() => GameManager.stoppedTime);
                }

                startTime += Time.deltaTime;
                yield return null;
            }
            onCooldown = false;
        }

        private void Start()
        {
            GameManager.instance.OnTimeStop += OnTimeStop;
            GameManager.instance.OnTimeResume += OnTimeResume;

            canFire = true;
        }

        void OnTimeStop()
        {
            canFire = false;
        }

        void OnTimeResume()
        {
            canFire = true;
        }
    }
}
