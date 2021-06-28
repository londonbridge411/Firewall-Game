using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina_Bar : MonoBehaviour
{
    private Image staminiaBar;
    public Image background;
    public float currentStamina;
    public float MAX_STAMINA;

    #region Singleton
    public static Stamina_Bar instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        staminiaBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentStamina = PlayerStats.instance.stamina;
        staminiaBar.fillAmount = currentStamina / 100;
        background.fillAmount = MAX_STAMINA / 100;
    }
}
