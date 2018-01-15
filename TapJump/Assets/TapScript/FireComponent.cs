using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FireComponent : MonoBehaviour {


    protected float _fire_interval = 0.1f;
    protected float _last_fire = 0f;

    public virtual void Init()
    {

    }

    public virtual void Fire()
    {
        if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;
            var b =Rocket.current._bulletPool.GetUnusedOne();
            b.transform.rotation = Quaternion.identity;
            b.transform.position = transform.position;
        }

    }
    
    
}
