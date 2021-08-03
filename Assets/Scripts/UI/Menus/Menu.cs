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
        }

        virtual public void CloseMenu()
        {
            OnExit();
            gameObject.SetActive(false);
        }

        virtual public void Pause()
        {
            OpenMenu();
            AudioManager.instance.Pause(AudioManager.instance.backgroundMusic);
            MenuControl.instance.isPaused = true;
            Time.timeScale = 0f;
        }

        virtual public void Resume()
        {
            AudioManager.instance.Play(AudioManager.instance.backgroundMusic);
            MenuControl.instance.isPaused = false;
            Time.timeScale = 1f;
            CloseMenu();
        }
        #endregion

        abstract public void OnEnter();
        abstract public void OnExit();
        abstract public void Execute();
    }
}
