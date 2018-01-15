using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IPlayState {

    readonly Vector3 SPEED = new Vector3(0,0.2f,0);

    private void Start()
    {
        LevelMgr.current.RegisterPlayState(this);
    }
    public void Play_Enter()
    {
    }

    public void Play_Exit()
    {
        Rocket.current.RetriveBullet(this);
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



