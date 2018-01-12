using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour,IPlayState {
    public GameObject SpikePrefab;
    Vector3 max;
    Vector3 min;
    Vector3 maxL;
    Vector3 maxR;

	// Use this for initialization
	void Start () {

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
        } 
    }
    float _lastGen = 0f;
    float _intervalGen = 1f;


    public void Play_Enter()
    {
    }

    public void Play_Exit()
    {
    }

    void GenerateSpike(int side)
    {
        GameObject spike = Instantiate(SpikePrefab);
        if (side % 4 == 0)
        {
            spike.transform.position = maxR;
        }else
        {
            spike.transform.position = maxL;
        }
        spike.transform.parent = transform;
    }
}
