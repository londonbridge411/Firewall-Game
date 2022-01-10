using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_Bar : MonoBehaviour
{
    private Image abilityBar;
    public float currentAbilityAmount;
    //public float MAX_ABILITY;

    #region Singleton
    public static Ability_Bar instance;

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
        abilityBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAbilityAmount = PlayerStats.instance.abilityAmount;
        abilityBar.fillAmount = currentAbilityAmount / 100f;
    }
}
