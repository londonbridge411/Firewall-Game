using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bowen.Saving;

namespace bowen.Interactable
{
    public abstract class Interactable : MonoBehaviour, ISaveable
    {
        public bool byInteractable;
        public bool canInteract = true;

        private void Update()
        {
            if (byInteractable && canInteract && Input.GetButtonDown("Interact") && transform.GetChild(0).gameObject.activeSelf)
                Interact();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                byInteractable = true;              
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                byInteractable = false;
            }
        }

        public abstract void Interact();

        public object CaptureState()
        {
            return new SaveData
            {
                canInteract = canInteract
            };
        }

        public void RestoreState(object state)
        {
            var saveData = (SaveData) state;

            canInteract = saveData.canInteract;
        }

        [Serializable]
        private struct SaveData
        {
            public bool canInteract;
        }
    }
}
