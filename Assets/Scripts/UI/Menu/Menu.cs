using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bowen.UI
{
    public abstract class Menu : MonoBehaviour
    {
        #region Virtual Methods
        virtual public void OpenMenu()
        {
            gameObject.SetActive(true);
            MenuControl.instance.menus.Push(this);
        }

        virtual public void CloseMenu()
        {
            MenuControl.instance.menus.Peek().OnExit();
            gameObject.SetActive(false);
            MenuControl.instance.menus.Pop();
        }
        #endregion

        abstract public void OnEnter();
        abstract public void OnExit();
        abstract public void Execute();
    }
}
