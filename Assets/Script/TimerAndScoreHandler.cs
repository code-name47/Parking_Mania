using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TimerAndScoreHandler
{
    // Start is called before the first frame update
    Timer m_globalScoreTimer;
    bool isGameOver = false;
    public static int GameStars => m_gameStars;
    const int m_gameStars = 3;
    const int HIGHDIFF = 30; //30 secs
    const int MIDDIFF = 60; // 60 secs 
    const int LOWDIFF = 90; // 90 secs
    int m_score = 0;
    int m_timeLimit = 0;
    int m_level = 0;
    UserDataHandler userDataHandler;

    DIFFICULTY m_hardness;
    Car_Controller cc = null;

    public int TimeLimit { get => m_timeLimit; set => m_timeLimit = value; }
    public Timer GlobalScoreTimer { get => m_globalScoreTimer; set => m_globalScoreTimer = value; }
    public DIFFICULTY Difficulty { get => m_hardness; set => m_hardness = value; }
    public int Score { get => m_score; set => m_score = value; }
    public int Level { get => m_level; set => m_level = value; }

    public TimerAndScoreHandler(int limit, DIFFICULTY diff)
    {
        m_timeLimit = limit;
        m_hardness = diff;
        m_globalScoreTimer = new Timer(1000);
    }

    public enum DIFFICULTY
    {
        EASY,
        MEDIUM,
        HARD
    };

    public void StartTimer()
    {
        try
        {
            userDataHandler = UserDataHandler.Instance();
            cc = GameObject.FindObjectOfType(typeof(Car_Controller)) as Car_Controller;
            if(cc == null)
            {
                Debug.LogError("Failed to find car controller object.");
                return;
            }

            Debug.Log("Starting Counter\n");
            m_globalScoreTimer.Elapsed += (sender, e) => RunCounter(sender, e, this);
            m_globalScoreTimer.Start();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        
    }

    public void UpdateData()
    {
        if(cc.Game_Win.activeSelf && !isGameOver)
        {
            /* Generate score only if the game is won */
            _generateScore();
            m_globalScoreTimer.Stop();
            Debug.Log("Counter stopped after winning.");
            Debug.Log("You Won, got " + Score + " stars.");
            userDataHandler.PushLevelData(Score, m_timeLimit, m_hardness, m_level);
            isGameOver = true;
        }
        else if((cc.Game_Over.activeSelf && !isGameOver) || m_timeLimit <= 0)
        {
            /* This stop is a fail safe. */
            m_globalScoreTimer.Stop();
            cc.Game_Over.SetActive(true);
            Debug.Log("You loose, got " + Score + " stars.");
            isGameOver = true;
            m_timeLimit = 1; //so that it doesn't come back here.
        }
    }

    private static void RunCounter(object sender, ElapsedEventArgs e, TimerAndScoreHandler _tsh)
    {
        try
        {
            Debug.Log("Time elapsed: " + e.SignalTime + "\n");
            if(_tsh.m_timeLimit <= 0)
            {
                _tsh.m_globalScoreTimer.Stop();
                _tsh.isGameOver = true;
                Debug.Log("Counter stopped after loosing");
            }
            --_tsh.m_timeLimit;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }    
    }

    private void _generateScore()
    {
        switch(m_hardness)
        {
            case DIFFICULTY.EASY:
            {
                m_score = _getScoreValues(LOWDIFF);
            }
            break;
            case DIFFICULTY.MEDIUM:
            {
               m_score = _getScoreValues(MIDDIFF);
            }
            break;
            case DIFFICULTY.HARD:
            {
                m_score = _getScoreValues(HIGHDIFF);
            }
            break;
            default:
                //this should not happen.
                Debug.LogError("Default difficulty hit, score not generated");
            break;   
        }
    }

    private int _getScoreValues(int difficultyTime)
    {
        double scoreLowLevel = difficultyTime / GameStars;
        double scoreMidLevel = scoreLowLevel * 2;
        double scoreHighLevel = scoreLowLevel * 3;

        int value = 0;

        if(m_timeLimit < scoreHighLevel && m_timeLimit >= scoreMidLevel)
        {
            value = 3;
        }
        else if(m_timeLimit < scoreMidLevel && m_timeLimit >= scoreLowLevel)
        {
            value = 2;
        }
        else
        {
            value = 1;
        }

        Debug.Log("Generating score, shl: " + scoreHighLevel + " sml: " + scoreMidLevel + " sll: " + scoreLowLevel + " counter: " + m_timeLimit 
        + " actual score: " + value);

        return value;
    }
}
