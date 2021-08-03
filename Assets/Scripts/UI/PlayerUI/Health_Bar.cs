using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health_Bar : MonoBehaviour
{

    private Image healthBar;
    public float currentHealth;
    private float MAX_HEALTH = 100f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = PlayerStats.instance.health;
        healthBar.fillAmount = currentHealth / MAX_HEALTH;
    }
}
