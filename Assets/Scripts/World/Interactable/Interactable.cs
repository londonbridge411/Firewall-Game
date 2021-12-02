using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool byInteractable;

        private void Update()
        {
            if (byInteractable)
            {
                if (Input.GetButtonDown("Interact") && transform.GetChild(0).gameObject.activeSelf)
                    Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
                byInteractable = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
                byInteractable = false;
        }

        public abstract void Interact();
    }
}
