using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletTypeEnum
{
    Red,
    Blue,
    Purple
}
public class Bullet : MonoBehaviour, IPlayState {

    readonly Vector3 SPEED = new Vector3(0, 0.2f, 0);
    public GameObject Red;
    public GameObject Blue;
    public GameObject Purple;

    BulletTypeEnum _bulletType = BulletTypeEnum.Red;
    public void SetType(BulletTypeEnum en)
    {
        _bulletType = en;
        Red.SetActive(false);
        Blue.SetActive(false);
        Purple.SetActive(false);
        switch (_bulletType)
        {
            case BulletTypeEnum.Red:
                Red.SetActive(true);
                break;
            case BulletTypeEnum.Blue:
                Blue.SetActive(true);
                break;
            case BulletTypeEnum.Purple:
                Purple.SetActive(true);
                break;
            default:
                Red.SetActive(true);
                break;
        }
    }

    private void Start()
    {
        LevelMgr.current.RegisterPlayState(this);
    }
    public void Play_Enter()
    {
    }

    public void Play_Exit()
    {
        //Rocket.current.RetriveBullet(this);
    }

    public void Play_Update()
    {
        
        transform.position += 0.2f * transform.up;
        if (transform.position.y > 6f)
        {
            Rocket.current.RetriveBullet(this);
        }


    }
}



