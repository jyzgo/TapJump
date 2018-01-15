﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleComponent : FireComponent {

    public override void Init()
    {
        _fire_interval = 0.25f;
    }
    public override void Fire()
    {
        if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;
            for (int i = 0; i < 3; i++)
            {
                var b = Rocket.current._bulletPool.GetUnusedOne();
                b.transform.rotation = Quaternion.identity;
                b.transform.position = transform.position - Vector3.left * (-0.2f + 0.2f * i);

            }

        }



    }
}
