using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.Interactable
{
    public class Note : Interactable
    {
        public GameObject stickyNote;
        public bool noteActive;

        /*private void Start()
        {
            stickyNote = transform.GetChild(0).gameObject;
        }*/

        public override void Interact()
        {
            Time.timeScale = 0f;
            MenuControl.instance.isPaused = true;
            MenuControl.instance.canPause = false;
            noteActive = true;
            stickyNote.SetActive(true);

            /*if (noteActive && Input.GetButtonDown("Dash"))
            {
                    stickyNote.SetActive(false);
                    Time.timeScale = 1f;
            }*/
        }

        private void Update()
        {
            if (byInteractable && canInteract && Input.GetButtonDown("Interact") && transform.GetChild(0).gameObject.activeSelf)
                Interact();
        }

        public void Close()
        {
            stickyNote.SetActive(false);
            Time.timeScale = 1f;
            MenuControl.instance.isPaused = false;
            MenuControl.instance.canPause = true;
        }
    }
}
