using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour,ICtrlAble {

    float _speed = 0f;

    const float DELTA_SPEED = -0.01f;
    const float JUMP_SPEED = 0.2f;
    const float HONRIZONTAL_SPEED = 0.03f;

    float _currentSpeed = 0f;
    public void Taping()
    {
        _currentSpeed = JUMP_SPEED;

    }
    void Awake()
    {

    }
	// Use this for initialization
	void Start () {
        LevelMgr.current.CtrlListeners += this.SetCtrlAble;
        LevelMgr.current.RegisterBall(this);
        Vector3 max = new Vector3(Screen.width, Screen.height, 10f);
        Vector3 min = new Vector3(0, 0, 10f);

        max = Camera.main.ScreenToWorldPoint(max);
        min = Camera.main.ScreenToWorldPoint(min);

        minX = min.x;
        maxX = max.x;
        minY = min.y;
        maxY = max.y;

	}
    float minX;
    float minY;
    float maxX;
    float maxY;
	
	// Update is called once per frame
	void Update () {

		
	}

    float _horizontalDir = 1f;
    public void Playing_Update()
    {
        float x = transform.position.x;
        if(x < minX)
        {
            _horizontalDir = 1;
        }else if(x > maxX)
        {
            _horizontalDir = -1;
        }
        _currentSpeed += DELTA_SPEED;
        Vector3 offset = new Vector3(HONRIZONTAL_SPEED * _horizontalDir, _currentSpeed,0f);
        transform.localPosition+= offset;


    }

    bool _isCtrlAble = false;
    public void SetCtrlAble(bool b)
    {
        _isCtrlAble = b;

    }

}
