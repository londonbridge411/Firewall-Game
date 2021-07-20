using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using bowen.BulletHell;

[CreateAssetMenu(fileName = "Pattern", menuName = "ScriptableObjects/Bullet Hell Pattern")]
public class BHPattern : ScriptableObject
{
    public BHBullet bullet;
    public int numberOfBullets;
    public float cooldownTime;
    public float rotation;
    public float speed;
    public bool canTrack;
    public bool hasNextPattern;
    public BHPattern nextPattern;

    public bool isTracking;

    private float angle;
    private GameObject parent;

    public float strength;
    public bool inverse;

    //tracking, spiral, 2nd pattern //rotation lerp for spiral

    public enum PatternType
    {
        Full, Half, Spiral, Burst
    };

    public PatternType type;

    public void Fire()
    {
        bullet.SetSpeed(speed);
        switch (type)
        {
            case PatternType.Full:
                FireFull();
                break;
            case PatternType.Half:
                FireHalf();
                break;
            case PatternType.Spiral:
                FireSpiral();
                break;
            case PatternType.Burst:
                FireBurst();
                break;
        }
    }

    private void FireFull()
    {
        angle = 360 / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            BHBullet bulletObj = Instantiate(bullet, parent.transform.position, Quaternion.identity);
            bulletObj.transform.Rotate(new Vector3(0, i * angle + rotation, 0));
            Vector3 AiPosition = bulletObj.transform.position;
            if (canTrack)
            {
                bulletObj.SetTracking(true);
}
            else
            {
                bulletObj.SetTracking(false);
            }
        }
    }

    private void FireHalf()
    {
        angle = 180 / numberOfBullets;

        for (int i = 0; i < (numberOfBullets); i++)
        {
            Vector3 currentAngle = new Vector3(0, i * angle + rotation, 0);
            if (currentAngle.y > 45 + rotation && currentAngle.y < 315 + rotation) //The plus 45 is a skew to line everything up
            {
                BHBullet bulletObj = Instantiate(bullet, parent.transform.position, Quaternion.identity);
                bulletObj.transform.Rotate(currentAngle);
                if (canTrack)
                {
                    bulletObj.SetTracking(true);
                }
                else
                {
                    bulletObj.SetTracking(false);
                }
            }       
        }
    }

    private void FireSpiral()
    {
        angle = 360 / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            BHBullet bulletObj = Instantiate(bullet, parent.transform.position, Quaternion.identity);
            bulletObj.transform.Rotate(new Vector3(0, i * angle + rotation, 0));
            Vector3 AiPosition = bulletObj.transform.position;
            if (canTrack)
            {
                bulletObj.SetTracking(true);
            }
            else
            {
                bulletObj.SetTracking(false);
            }
        }
        if (inverse)
            rotation -= strength;
        else
            rotation += strength;
        if (rotation >= 360)
            rotation = 0;
    }

    private void FireBurst()
    {
        angle = 180 / numberOfBullets;

        for (int i = 0; i < (numberOfBullets); i++)
        {
            Vector3 currentAngle = new Vector3(0, i * angle + rotation, 0);
            if (currentAngle.y > 45 + rotation && currentAngle.y < 315 + rotation) //The plus 45 is a skew to line everything up
            {
                BHBullet bulletObj = Instantiate(bullet, parent.transform.position, Quaternion.identity);
                bulletObj.transform.Rotate(currentAngle);
                if (canTrack)
                {
                    bulletObj.SetTracking(true);
                }
                else
                {
                    bulletObj.SetTracking(false);
                }

            }
        }
        if (inverse)
            rotation -= strength;
        else
            rotation += strength;
        if (rotation >= 360)
            rotation = 0;
    }

    public void SetParent(GameObject parentObj)
    {
        parent = parentObj;
    }

    public GameObject GetParent() => parent;
}
