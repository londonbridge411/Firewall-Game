using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOverclock : Ability
{
    public override void Activate()
    {
        GameManager.overclocked = true;
        BuffPlayer();
        StartCoroutine(GameManager.OverclockLerp());
    }

    public override void Deactivate()
    {
        GameManager.overclocked = false;
        NerfPlayer();
        StartCoroutine(GameManager.OverclockRevert());
    }

    void BuffPlayer()
    {
        PlayerController.instance.moveSpeed = 45f;
        PlayerController.instance.dashSpeed = 150f;
    }

    void NerfPlayer()
    {
        PlayerController.instance.moveSpeed = 20f;
        PlayerController.instance.dashSpeed = 100f;
    }
}
