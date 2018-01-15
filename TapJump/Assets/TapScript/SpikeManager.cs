using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour, IPlayState,IMenuState
{
    public GameObject SpikePrefab;
    Vector3 max;
    Vector3 min;
    Vector3 maxL;
    Vector3 maxR;
    public static SpikeManager current;

    ComponentPool<Obstacle> _obstaclePool;

    // Use this for initialization
    void Start()
    {
        current = this;
        _obstaclePool = new ComponentPool<Obstacle>(5, SpikePrefab, transform);
        LevelMgr.current.RegisterPlayState(this);
        LevelMgr.current.RegisterMenuState(this);
        max = new Vector3(Screen.width, Screen.height, 10f);
        min = new Vector3(0, 0, 10f);

        max = Camera.main.ScreenToWorldPoint(max);
        min = Camera.main.ScreenToWorldPoint(min);
        maxL = new Vector3(min.x, max.y, max.z);
        maxR = max;
        _originalPos = transform.position;
        float everyX = (maxR.x - maxL.x) / ROW_NUM;
        _gap = new Vector3(everyX, 0, 0);


    }
    Vector3 _gap;

    readonly Vector3 MANAGER_SPEED = new Vector3(0, 0.02f, 0);

    Vector3 _originalPos;
    const int ROW_NUM = 10;
    public void Play_Update()
    {
        transform.position -= MANAGER_SPEED;
        if (Time.time - _intervalGen > _lastGen)
        {
            _lastGen = Time.time;
            int RAN = MTUnity.Actions.MTRandom.Next();
            int dir = 1;
            if (RAN % 2 == 0)
            {
                dir = -1;
            }
            for (int i = 0; i < ROW_NUM; i++)
            {
                Obstacle ob = _obstaclePool.GetUnusedOne();

                ob.transform.position = maxL + i * _gap;
                ob.Init(dir, 10);
                ob.transform.parent = transform;

            }
        }
        var usedSet = _obstaclePool.GetUsedSet();
        foreach (var b in usedSet)
        {
            b.Play_Update();
        }
        foreach(var b in _retriveSet)
        {
            DoneRetrive(b);
        }
        _retriveSet.Clear();
    }
    float _lastGen = 0f;
    float _intervalGen = 2f;


    public void Play_Enter()
    {
    }

    public void Play_Exit()
    {
    }

    void GenerateSpike(int side)
    {
        Obstacle spike = _obstaclePool.GetUnusedOne();
        Vector3 x;
        if (side % 2 == 0)
        {
            x = maxR;
        }
        else
        {
            x = maxL;
        }
        spike.transform.position = x;
        int life = MTUnity.Actions.MTRandom.GetRandomInt(1, 10);
    }

    void GenerateStatic()
    {
        Obstacle spike = _obstaclePool.GetUnusedOne();
        float scale = MTUnity.Actions.MTRandom.GetRandomFloat(0, 1f);
        Vector3 pos = (maxR - maxL) * scale + maxL;
        spike.transform.position = pos;
        int life = MTUnity.Actions.MTRandom.GetRandomInt(1, 10);
        spike.transform.parent = transform;
    }

    HashSet<Obstacle> _retriveSet = new HashSet<Obstacle>();
    public void AddToRetriveSet (Obstacle ob)
    {
        _retriveSet.Add(ob);
    }

    void DoneRetrive(Obstacle ob)
    {
        ob.gameObject.SetActive(false);
        _obstaclePool.Retrive(ob);
 
    }

    public void Menu_Enter()
    {
        _obstaclePool.RetriveAll();
    }

    public void Menu_Update()
    {
    }

    public void Menu_Exit()
    {
    }
}
