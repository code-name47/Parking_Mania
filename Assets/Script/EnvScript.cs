using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;

public class EnvScript : MonoBehaviour
{
    int timeLimit = 0;
    TimerAndScoreHandler.DIFFICULTY diff = TimerAndScoreHandler.DIFFICULTY.EASY;
    TimerAndScoreHandler tsc = null;

    EnvScript()
    {
        diff = (TimerAndScoreHandler.DIFFICULTY) Main_Menu.Lvldifficulty;
        timeLimit = getTimeBasedOnDifficulty(); 
        tsc = new TimerAndScoreHandler(timeLimit, diff);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        tsc.Level = SceneManager.GetActiveScene().buildIndex;
        tsc.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        tsc.UpdateData();
    }

    int getTimeBasedOnDifficulty()
    {
        int time = 0;
        switch(diff)
        {
            case TimerAndScoreHandler.DIFFICULTY.EASY:
                time = 90;
            break;
            case TimerAndScoreHandler.DIFFICULTY.MEDIUM:
                time = 60;
            break;
            case TimerAndScoreHandler.DIFFICULTY.HARD:
                time = 30;
            break;
            default:
                // set to easy value if something goes wrong then as this is a fail safe and should not happen.
                time = 90;
                Debug.LogError("Invalid difficulty received");
            break;
        }

        return time;
    }

    void OnApplicationQuit()
    {
        //stopping timer as game ended.
        Debug.Log("Stopping timer as game is stopped");
        tsc.GlobalScoreTimer.Stop();
        UserDataHandler.Instance().CleanUp();
    }
}
