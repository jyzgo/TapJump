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

[DefaultExecutionOrder(-1)]
public class LevelMgr : MonoBehaviour {
    public enum LevelState
    {
        Menu,
        Playing,

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

    public static LevelMgr current;

    public ComponentPool<ParticleSystem> _parPool;


    StateMachine<LevelState> fsm;
    public void Awake()
    {
        current = this;
        AdMgr.RegisterAllAd();
        AdMgr.ShowDownAdmobBanner();
        Application.targetFrameRate = 60; 
        Vector3 max = new Vector3(Screen.width, Screen.height, 10f);
        Vector3 min = new Vector3(0, 0, 10f);

        max = Camera.main.ScreenToWorldPoint(max);
        min = Camera.main.ScreenToWorldPoint(min);

       AdMgr.ShowDownAdmobBanner();

    }



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

        StartGame();

        UpdateScore();


    }

    public void PlayBtn_Click()
    {

        fsm.ChangeState(LevelState.Playing);
    }

      
        

   void StartGame()
    {
    }

    void Playing_Enter()
    {

        if (CtrlListeners != null)
        {
            CtrlListeners(true);
        }

    }


    void Playing_Exit()
    {
        if (CtrlListeners != null)
        {
            CtrlListeners(false);
        }
    }
	
    


    public Transform DeadZone;

    public void ToMenu()
    {
        fsm.ChangeState(LevelState.Menu);
    }
    #region Lose
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
}
