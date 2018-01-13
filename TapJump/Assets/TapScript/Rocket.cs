using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour,ICtrlAble {

    ComponentPool<Bullet> _bulletPool;
    public static Rocket current;

    float _speed = 0f;

    const float DELTA_SPEED = -0.005f;
    const float JUMP_SPEED = 0.09f;
    const float HONRIZONTAL_SPEED = 0.04f;
    public ParticleSystem _glow;
    public GameObject bulletPrefab;

    float _currentSpeed = 0f;
    public void Taping(float x)
    {
        if(x > 0)
        {
            _horizontalDir = -1;
        }else
        {
            _horizontalDir = 1;
        }
        _currentSpeed = JUMP_SPEED;
        _glow.enableEmission = true;
        StartCoroutine(StopGlow());

    }
    IEnumerator StopGlow()
    {
        yield return new WaitForSeconds(0.1f);
        _glow.enableEmission = false;
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
    public void Playing_Update()
    {
        if (Time.time > _last_fire + FIRE_INTERVAL)
        {
            _last_fire = Time.time;
            var b = _bulletPool.GetUnusedOne();
            b.transform.position = transform.position;
        }
        Vector3 originPos = transform.position;
        float x = transform.position.x;
        if(x < minX)
        {
            transform.position = new Vector3(maxX, originPos.y, originPos.z);
        }else if(x > maxX)
        {

            transform.position = new Vector3(minX, originPos.y, originPos.z);
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

}
