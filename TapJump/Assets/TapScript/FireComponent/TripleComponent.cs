using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleComponent : FireComponent {
    public TripleComponent(Transform rocketTrans) : base(rocketTrans)
    {
        _rockTrans = rocketTrans;
    }

    public override void Init()
    {
        _fire_interval = 0.35f;
    }
    readonly Vector3 Offset = new Vector3(0, 0, 4f);
    const float DISTANCE = 0.1f;
    public override void Fire(int powerUpLevel)
    {   if (Time.time > _last_fire + _fire_interval)
        {
            _last_fire = Time.time;
            int BulletNum = powerUpLevel * 2 + 1;
            Vector3 startRotation = Vector3.zero - powerUpLevel* Offset;

            Vector3 startPos = _rockTrans.position + Vector3.left * (float)(BulletNum-1) / 2f * DISTANCE;
            for (int i = 0; i < BulletNum; i++)
            {
                var b = Rocket.current._bulletPool.GetUnusedOne();
                b.SetType(BulletTypeEnum.Purple);
                b.transform.eulerAngles = startRotation +  i * Offset;
               // b.transform.position = _rockTrans.position;
                b.transform.position = startPos + Vector3.right * DISTANCE * i;

            }

        }

    }
}
