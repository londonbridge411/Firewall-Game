using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwapper : MonoBehaviour
{
    public List<GameObject> weapons;
    public int WeaponNumber;

    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<GameObject>(GameObject.FindGameObjectsWithTag("Weapon"));
        Swap(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuControl.instance.isPaused)
            return;

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                WeaponNumber++;
                Swap(WeaponNumber);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                WeaponNumber--;
                Swap(WeaponNumber);
            }
        }

        if (WeaponNumber > weapons.Count - 1)
        {
            WeaponNumber = 0;
            Swap(WeaponNumber);
        }

        if (WeaponNumber < 0)
        {
            WeaponNumber = weapons.Count -1;
            Swap(WeaponNumber);
        }
    }
    public delegate void WeaponHandler();
    public event WeaponHandler OnWeaponChange;

    public void Swap(int index)
    {
        //StartCoroutine(CameraScript.instance.Shake(0, 0, 0));
        OnWeaponChange?.Invoke();
        foreach (GameObject g in weapons)
        {
            g.SetActive(false);
        }
        try
        {
            weapons[index].SetActive(true);
        }
        catch (System.Exception e)
        {
            print("Started over");
        }
    }
}
