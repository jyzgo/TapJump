using UnityEngine;
using System.Collections;
using System;

public class BGResize : MonoBehaviour,ICtrlAble {

    SpriteRenderer sr;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	// Use this for initialization
	void Start () {
        Resize();
        LevelMgr.current.CtrlListeners += this.SetCtrlAble;
    }
    void Resize()
    {


        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Camera.main.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width;
        transform.localScale = xWidth;
        //transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height;
        transform.localScale = yHeight;
        //transform.localScale.y = worldScreenHeight / height;

    }

    public void SetBg(Sprite sp)
    {
        sr.sprite = sp;
        Resize();
    }

    Vector3 touchPos = Vector3.zero;
    Vector3 dragPos = Vector3.zero;
    private void OnMouseDown()
    {

        if (_ctrlAble)
        {
            var v3 = Input.mousePosition;
            v3.z = 10f;
            touchPos = Camera.main.ScreenToWorldPoint(v3);
           LevelMgr.current.BegainTouch(touchPos);
       }

    }

    const float DIS = 0.5f;
    const float MIN_DIS = 0.4f;
    private void OnMouseUp()
    {


        if(_ctrlAble)
        {
            if (dragPos.y < touchPos.y)
            {
                if (Vector3.Distance(dragPos, touchPos) > MIN_DIS)
                {

                }
            }
        }
    }

    private void OnMouseDrag()
    {
        if(_ctrlAble)
        {
            var v3 = Input.mousePosition;
            v3.z = 10f;
            touchPos = Camera.main.ScreenToWorldPoint(v3);

            LevelMgr.current.TouchMoving(touchPos);
        }
    }

    bool _ctrlAble = false;
    public void SetCtrlAble(bool b)
    {
        _ctrlAble = b;
    }


}
