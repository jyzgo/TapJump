using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FireComponent {
    protected float _fire_interval = 0.07f;
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
        if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;

            Vector3 startPos = _rockTrans.position + Vector3.left * (float)(powerUpLevel) / 2f * DISTANCE;
            for (int i = 0; i < powerUpLevel + 1; i++)
            {
                var b = Rocket.current._bulletPool.GetUnusedOne();
                b.transform.rotation = Quaternion.identity;
                b.SetType(BulletTypeEnum.Red);
                b.transform.position = startPos + Vector3.right * DISTANCE * i;
            }
        }

    }
    const float DISTANCE = 0.16f;
}
