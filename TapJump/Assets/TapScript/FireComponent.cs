using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FireComponent : MonoBehaviour {


    const float FIRE_INTERVAL = 0.1f;
    float _last_fire = 0f;
    private void Start()
    {

    }

    public void Fire()
    {
        if (Time.time > _last_fire + FIRE_INTERVAL)
        {
            _last_fire = Time.time;
            var b =Rocket.current._bulletPool.GetUnusedOne();
            b.transform.position = transform.position;
        }

    }
    
    
}
