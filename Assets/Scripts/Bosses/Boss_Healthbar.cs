using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Healthbar : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float MAX_HEALTH = 3000f;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = XZ10Script.instance.gameObject.GetComponent<BossStats>().health;
        healthBar.fillAmount = currentHealth / MAX_HEALTH;
    }
}
