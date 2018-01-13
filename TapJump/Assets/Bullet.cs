using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    readonly Vector3 SPEED = new Vector3(0,0.2f,0);
	
	

	// Update is called once per frame
	void Update () {
        transform.position += SPEED;
        if (transform.position.y > 6f)
        {
            Rocket.current.RetriveBullet(this);
        }
    }
}
