using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanShapeComponent: FireComponent {
    public FanShapeComponent(Transform rocketTrans) : base(rocketTrans)
    {
        _rockTrans = rocketTrans;
    }

    public override void Init()
    {
        _fire_interval = 0.25f;
    }
    Vector3 Offset = new Vector3(0, 0, 2.5f);
    public override void Fire(int powerUpLv)
    {
        if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;
            int BulletNum = powerUpLv * 2 + 1;
            Vector3 startRotation = Vector3.zero - powerUpLv* Offset;
            for (int i = 0; i < BulletNum; i++)
            {
                var b = Rocket.current._bulletPool.GetUnusedOne();
                b.SetType(BulletTypeEnum.Blue);
                b.transform.eulerAngles = startRotation +  i * Offset;
                b.transform.position = _rockTrans.position;

            }

        }



    }
}
