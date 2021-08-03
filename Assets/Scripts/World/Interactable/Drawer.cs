using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.Interactable
{
    public class Drawer : Interactable, IDoor
    {
        Animator anim;
        bool isOpen;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void Close()
        {
            anim.SetBool("isOpen", false);
            isOpen = false;
        }

        public void Open()
        {
            anim.SetBool("isOpen", true);
            isOpen = true;
        }

        public override void Interact()
        {
            print("Opened Drawer");
            if (!isOpen)
                Open();
            else
                Close();
        }
    }
}