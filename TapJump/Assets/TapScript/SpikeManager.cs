﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour,IPlayState {
    public GameObject SpikePrefab;
    Vector3 max;
    Vector3 min;
    Vector3 maxL;
    Vector3 maxR;
    public static SpikeManager current;

    ComponentPool<Obstacle> _obstaclePool;

	// Use this for initialization
	void Start () {
        current = this;
        _obstaclePool = new ComponentPool<Obstacle>(5, SpikePrefab, transform);
        LevelMgr.current.RegisterPlayState(this);
        max = new Vector3(Screen.width, Screen.height, 10f);
        min = new Vector3(0, 0, 10f);

        max = Camera.main.ScreenToWorldPoint(max);
        min = Camera.main.ScreenToWorldPoint(min);
        maxL = new Vector3(min.x, max.y, max.z);
        maxR = max;
        _originalPos = transform.position;
		
	}

    readonly Vector3 MANAGER_SPEED = new Vector3(0,0.02f,0);        

    Vector3 _originalPos;

    public void Play_Update()
    {
        transform.position -= MANAGER_SPEED;
       if(Time.time - _intervalGen > _lastGen)
        {
            int RAN = MTUnity.Actions.MTRandom.Next();
            _lastGen = Time.time;
            if (RAN % 2 == 0)
            {
                GenerateSpike(RAN);
            }
            GenerateStatic();
        } 
    }
    float _lastGen = 0f;
    float _intervalGen = 0.5f;


    public void Play_Enter()
    {
    }

    public void Play_Exit()
    {
    }

    void GenerateSpike(int side)
    {
        Obstacle spike =_obstaclePool.GetUnusedOne();
        Vector3 x;
        if (side % 2 == 0)
        {
            x = maxR;
        }else
        {
            x = maxL;
        }
        spike.transform.position = x;
        int life = MTUnity.Actions.MTRandom.GetRandomInt(1, 10);
        spike.Init(x.x,life);
        spike.transform.parent = transform;
    }

    void GenerateStatic()
    {
        Obstacle spike = _obstaclePool.GetUnusedOne();
        float scale = MTUnity.Actions.MTRandom.GetRandomFloat(0, 1f);
        Vector3 pos = (maxR - maxL) * scale + maxL;
        spike.transform.position = pos;
        int life = MTUnity.Actions.MTRandom.GetRandomInt(1, 10);
        spike.Init(pos.x,life,0);
        spike.transform.parent = transform;
    }

     public void RetriveObstacle(Obstacle ob)
    {
        ob.gameObject.SetActive(false);
        _obstaclePool.Retrive(ob);
    }
}
