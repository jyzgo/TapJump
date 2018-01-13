﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MTUnity.Actions;

public class Obstacle : MonoBehaviour {


    readonly Color[] colors = {
        new Color(0.96f, 0.71f, 0.18f),
        new Color(0.52f,0.77f,0.31f),
        new Color(0.64f,0.56f,0.32f),
        new Color(0.89f,0.07f,0.37f),
        new Color(0.76f,0.13f,0.53f),
        new Color(0.09f,0.45f,0.74f),
        new Color(0f,0.6f,0.55f),
        new Color(0.88f,0.32f,0.12f),
        new Color(0.24f,0.15f,0.14f),
        new Color(0.1f,0.14f,0.49f),
        new Color(0.72f,0.11f,0.11f) };

    int[] LV_ARR = new int[]{
        0,
        1,
         5,
        10,
        15,
        30,
        50,
        100,
        150,
        200,
        300,
        400
        };

    Color GetCurrentColor()
    {

        return colors[GetCurrentColorIndex()];
    }

    int GetCurrentColorIndex()
    {
        for (int i = 0; i < LV_ARR.Length; i++)
        {
            if (_life < LV_ARR[i])
            {
                return i;
            }
        }
        return colors.Length - 1;
    }

    Color GetPreColor()
    {
        int index = GetCurrentColorIndex() - 1;
        if (index > 0)
        {
            return colors[index];
        }
        return colors[0];

    }
    void InitColor()
    {
        sp.color = GetCurrentColor();
    }
    Vector3 _offsetColor;
    void UpdateColor()
    {
        if (_life> LV_ARR[0])
        {

            var currentColor = GetCurrentColor();
            var preColor = GetPreColor();
            int currentIndex = GetCurrentColorIndex();
            int indexHp = LV_ARR[currentIndex];
            int preHp = LV_ARR[currentIndex - 1];
            float per = (float)(_life- preHp) / (float)(indexHp - preHp);

            sp.color = Color.Lerp(currentColor, preColor, per);
        }


    }

    public SpriteRenderer sp;
    public Text LifeNum;

    float _dir = 1f;
    float _horizontalSpeed =0f;
    int _life = 1;
    public void Init(float x,int life =1,float inSpeed = 1f)
    {
        int ran = MTRandom.Next()%2;
        
        float speedScale = MTRandom.GetRandomFloat(0.8f,1.5f);
        _dir = ((float)ran - 0.5f) * 2f;
        if (x > 0)
        {
            _horizontalSpeed = -0.03f * speedScale - 0.01f;
        } else
        {
            _horizontalSpeed = 0.003f * speedScale + 0.01f;
        }
        _horizontalSpeed *= inSpeed;

        _life = life;
        UpdateLife();
    }
	
	// Update is called once per frame
	void Update () {
        
        sp.transform.Rotate(new Vector3(0, 0, _dir));
        transform.position += new Vector3(_horizontalSpeed, 0, 0);
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet b = collision.GetComponent<Bullet>();
        if(b != null)
        {
            _life--;
            UpdateLife();
        }
    }

    void UpdateLife()
    {
        LifeNum.text = _life.ToString();
        if (_life <= 0)
        {
            SpikeManager.current.RetriveObstacle(this);
        }
        else
        {
            UpdateColor();
        }
    }
}