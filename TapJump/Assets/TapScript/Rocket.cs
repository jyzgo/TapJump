using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour,ICtrlAble,IPlayState,IMenuState {

    ComponentPool<Bullet> _bulletPool;
    public static Rocket current;

    float _speed = 0f;

    const float DELTA_SPEED = -0.005f;
    const float JUMP_SPEED = 0.09f;
    const float HONRIZONTAL_SPEED = 0.04f;
    public ParticleSystem _glow;
    public GameObject bulletPrefab;

    float _currentSpeed = 0f;
    IEnumerator StopGlow()
    {
        yield return new WaitForSeconds(0.1f);
        _glow.enableEmission = false;
    }
    Vector3 originPos;
    void Awake()
    {
        originPos = transform.position;
    }
	// Use this for initialization
	void Start () {
        LevelMgr.current.CtrlListeners += this.SetCtrlAble;
        LevelMgr.current.RegisterBall(this);
        LevelMgr.current.RegisterPlayState(this);
        LevelMgr.current.RegisterMenuState(this);
        Vector3 max = new Vector3(Screen.width, Screen.height, 10f);
        Vector3 min = new Vector3(0, 0, 10f);
        _bulletPool = new ComponentPool<Bullet>(5, bulletPrefab);
        current = this;
        max = Camera.main.ScreenToWorldPoint(max);
        min = Camera.main.ScreenToWorldPoint(min);

        minX = min.x;
        maxX = max.x;
        minY = min.y;
        maxY = max.y;
        _glow.Play();
        _glow.enableEmission = false;


	}
    float minX;
    float minY;
    float maxX;
    float maxY;
    const float FIRE_INTERVAL = 0.1f;
    float _last_fire = 0f;
    float _horizontalDir = 1f;

    bool _isCtrlAble = false;
    public void SetCtrlAble(bool b)
    {
        _isCtrlAble = b;

    }

    public void RetriveBullet(Bullet b)
    {
        _bulletPool.Retrive(b);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle sp = collision.GetComponent<Obstacle>();
        if(sp != null)
        {
            LevelMgr.current.ToLose();

        }
    }

    public void Play_Enter()
    {
        transform.position = originPos;
    }

    public void Play_Update()
    {
        if (Time.time > _last_fire + FIRE_INTERVAL)
        {
            _last_fire = Time.time;
            var b = _bulletPool.GetUnusedOne();
            b.transform.position = transform.position;
        }
        //Vector3 originPos = transform.position;
        //float x = transform.position.x;
        //if(x < minX)
        //{
        //    transform.position = new Vector3(maxX, originPos.y, originPos.z);
        //}else if(x > maxX)
        //{

        //    transform.position = new Vector3(minX, originPos.y, originPos.z);
        //}
        //_currentSpeed += DELTA_SPEED;
        //Vector3 offset = new Vector3(HONRIZONTAL_SPEED * _horizontalDir, _currentSpeed,0f);
        //transform.localPosition+= offset;

       //}


    }

    public void Play_Exit()
    {
    }

    public void Menu_Enter()
    {
        transform.position = originPos;
    }

    public void Menu_Update()
    {
    }

    public void Menu_Exit()
    {
    }

    internal void SetPosInScreen(Vector3 vector3,Vector3 offset)
    {
        Vector3 endPos = vector3 + offset;
        if (endPos.y > LevelMgr.current.minY && endPos.y < LevelMgr.current.maxY && endPos.x > LevelMgr.current.minX && endPos.x < LevelMgr.current.maxX)
        {
            transform.position = endPos;
        }

    }
}
