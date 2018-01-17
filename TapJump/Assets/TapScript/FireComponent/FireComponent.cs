using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FireComponent {


    protected float _fire_interval = 0.1f;
    protected float _last_fire = 0f;

    protected Transform _rockTrans;
    public virtual void Init()
    {

    }

    public virtual FireComponentEnum FireType()
    {
        return FireComponentEnum.Single;
    }

    public FireComponent(Transform rocketTrans)
    {
        _rockTrans = rocketTrans;
    }

    public virtual void Fire(int powerUpLevel)
    {

        switch(powerUpLevel)
        {
            case 0:
                Fire0();
                break;
            case 1:
                Fire1();
                break;
            case 2:
                Fire2();
                break;
            case 3:
                Fire3();
                break;
            default:
                Fire3();
                break;
                
        }
    }

    protected virtual void Fire0()
    {
        if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;
            var b = GetBullet();
            b.transform.position = _rockTrans.position;
        }
    }
     protected virtual void Fire1()
    {
        if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;
            FireSuite(1);
           
        }
    }
     protected virtual void Fire2()
    {
        if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;
            FireSuite(2);
        }
    }
     protected virtual void Fire3()
    {
        if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;
            FireSuite(3);
        }
    }

    const float DISTANCE = 0.16f;
    void FireSuite(int index)
    {

        Vector3 startPos = _rockTrans.position +  Vector3.left*(float)(index)/2f * DISTANCE;
        for(int i = 0; i <index + 1; i++)
        {
            var b = GetBullet();
            b.transform.position = startPos + Vector3.right * DISTANCE * i;

        }
    }

    Bullet GetBullet()
    {
        var b = Rocket.current._bulletPool.GetUnusedOne();
        b.transform.rotation = Quaternion.identity;
        b.SetType(BulletTypeEnum.Red);
        return b;
    }


}
