using System.Collections;
using System.Collections.Generic;
using bowen.Saving;
using bowen.AI;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            print("Saved Game");
            if (Stamina_Bar.instance.MAX_STAMINA != 100)
            {
                Stamina_Bar.instance.MAX_STAMINA = 100;
            }
    
            if (PlayerStats.instance.health != 100)
            {
                PlayerStats.instance.health = 100;
            }
            LevelData.SetLevel();
            ReloadEnemies();
            PlayerData.SetPosition(gameObject);
            PlayerData.SetScore(Score.instance.score);
            SaveLoadSystem.instance.Save();
        }
    }

    private void ReloadEnemies()
    {
        AiStats[] list = (AiStats[]) Resources.FindObjectsOfTypeAll(typeof(AiStats));

        foreach (AiStats ai in list)
        {
            ai.ResetAI();
        }
    }
}
