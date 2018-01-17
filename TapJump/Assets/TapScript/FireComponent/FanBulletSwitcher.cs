using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class FanBulletSwitcher: MonoBehaviour,IPlayState {

	// Use this for initialization
	void Start () {
        LevelMgr.current.RegisterPlayState(this);
	}
	
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rocket = collision.GetComponent<Rocket>();
        if(rocket != null)
        {
            rocket.SwitchFireComponent(FireComponentEnum.FanShaped);
            LevelMgr.current.UnRegisterPlayState(this);
            Destroy(gameObject);
        }
    }

    public void Play_Enter()
    {
    }

    public void Play_Update()
    {
        //transform.position += 0.02f * Vector3.down;
    }

    public void Play_Exit()
    {
    }
}
