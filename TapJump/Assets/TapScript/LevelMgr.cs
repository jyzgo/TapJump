using MTUnity.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using System;
using UnityEngine.UI;

public interface ICtrlAble
{
    void SetCtrlAble(bool b);
}

public interface IPlayState
{
    void Play_Enter();
    void Play_Update();
    void Play_Exit();
}

[DefaultExecutionOrder(-1)]
public class LevelMgr : MonoBehaviour {
    public enum LevelState
    {
        Menu,
        Playing,
        Lose

    }

    public Text Score;

    int currentScore = 0;

    public void AddCurrentScore()
    {
        currentScore++;
        UpdateScore();
    }

    public void UpdateScore()
    {
        //Score.text = moveTimes.ToString();// currentScore + "/" + maxScore; 
    }
    public Action<bool> CtrlListeners;
    public HashSet<IPlayState> _playUpdateSet = new HashSet<IPlayState>();
    public void RegisterPlayState(IPlayState item)
    {
        _playUpdateSet.Add(item);
    }

    public void UnRegisterPlayState(IPlayState item)
    {
        _playUpdateSet.Remove(item);
    }

    public static LevelMgr current;

    public ComponentPool<ParticleSystem> _parPool;


    StateMachine<LevelState> fsm;
    public void Awake()
    {
        current = this;
        AdMgr.RegisterAllAd();
        AdMgr.ShowDownAdmobBanner();
        AdMgr.ShowDownAdmobBanner();
        Application.targetFrameRate = 60;
        Vector3 max = new Vector3(Screen.width, Screen.height, 10f);
        Vector3 min = new Vector3(0, 0, 10f);

        max = Camera.main.ScreenToWorldPoint(max);
        min = Camera.main.ScreenToWorldPoint(min);

        _camera = Camera.main.transform;
        _camOriginalPos = _camera.position;
    }
    Vector3 _camOriginalPos;


    const float r = 0.2f;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    // Use this for initialization

   void Start () {
        fsm = StateMachine<LevelState>.Initialize(this, LevelState.Menu);
        
	}

   // public Transform startPos;
    private void ResetGame()
    {
   }
    public GameObject PlayMenu;
    void Menu_Enter()
    {
        PlayMenu.SetActive(true);
        ResetGame();
        if (AdMgr.IsAdmobInterstitialReady())
        {
            AdMgr.ShowAdmobInterstitial();
        }

    }



    void Menu_Exit()
    {
        AdMgr.PreloadAdmobInterstitial();
        PlayMenu.SetActive(false);


        UpdateScore();


    }

    public void PlayBtn_Click()
    {

        fsm.ChangeState(LevelState.Playing);
    }



    void Playing_Enter()
    {
        _camera.transform.position = _camOriginalPos;

        if (CtrlListeners != null)
        {
            CtrlListeners(true);
        }

        foreach(var b in _playUpdateSet)
        {
            b.Play_Enter();
        }

    }

    Transform _camera;
    void Playing_Update()
    {
       if(_ball != null)
        {
            _ball.Playing_Update();
        }
       foreach(var b in _playUpdateSet)
        {
            b.Play_Update();
        }

       // _camera.position += CAMERA_SPEED; 

       
    }

    public void Tap(float x)
    {
        if(_ball != null)
        {

            _ball.Taping(x);
        }

    }
    void Playing_Exit()
    {
        if (CtrlListeners != null)
        {
            CtrlListeners(false);
        }

        foreach(var b in _playUpdateSet)
        {
            b.Play_Exit();
        }
    }
	
    


    public Transform DeadZone;

    public void ToMenu()
    {
        fsm.ChangeState(LevelState.Menu);
    }
    #region Lose

    public void ToLose()
    {
        fsm.ChangeState(LevelState.Lose);
    }

    public GameObject LoseMenu;
    void Lose_Enter()
    {
        LoseMenu.SetActive(true);
    }

    void Lose_Exit()
    {
        LoseMenu.SetActive(false);
    }
    #endregion Lose


    #region Ball
    Ball _ball;
    public void RegisterBall(Ball bal)
    {
        _ball = bal;

    }
    #endregion Ball
}
