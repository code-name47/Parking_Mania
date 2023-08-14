using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace LooneyDog
{
    [Serializable]
    public class PlayerData 
    {
        public int HighScore { get { return HIGHSCORE; } set { HIGHSCORE = value; } }
        public float CameraSensitivity { get { return CAMERASENSITIVITY; } set { CAMERASENSITIVITY = value; } }
        public bool ShowTutorial { get { return SHOWTUTORIAL; } set { SHOWTUTORIAL = value; } }

        public LevelDataJson[] Leveldatajson { get { return _leveldatajson; } set { _leveldatajson = value; } }

        [SerializeField] private int HIGHSCORE;
        [SerializeField] private float CAMERASENSITIVITY = 0.5f;
        [SerializeField] private bool SHOWTUTORIAL = true;
        [SerializeField] private LevelDataJson[] _leveldatajson;

        public void Check() {
            if (_leveldatajson == null)
            {
                _leveldatajson = new LevelDataJson[GameManager.Game.Level.Leveldatas.Length];
                GetLevelDataToScriptableObjects();
            }
            else {
                SetLevelDataToScriptableObjects();
            }
        }
        public void SetNormalizedCameraSensitivity(float value)
        {
            CAMERASENSITIVITY = value / 100;
        }

        public void SetDataToJson(bool levelstatus,int starsobtained, int levelnumber) {
            _leveldatajson[levelnumber].LevelCompleted = levelstatus;
            _leveldatajson[levelnumber].StarsObtained = starsobtained;
        }

        public void SetLevelDataToScriptableObjects() {
            for (int i = 0; i < GameManager.Game.Level.Leveldatas.Length; i++) {
                GameManager.Game.Level.Leveldatas[i].LevelCompleted = _leveldatajson[i].LevelCompleted;
                GameManager.Game.Level.Leveldatas[i].StarsObtained = _leveldatajson[i].StarsObtained;
            }
        }

        public void GetLevelDataToScriptableObjects()
        {
            for (int i = 0; i < GameManager.Game.Level.Leveldatas.Length; i++)
            {
                _leveldatajson[i].LevelCompleted = GameManager.Game.Level.Leveldatas[i].LevelCompleted;
                _leveldatajson[i].StarsObtained = GameManager.Game.Level.Leveldatas[i].StarsObtained;
                Debug.Log(" json: Stars Obtained" + GameManager.Game.Level.Leveldatas[i].StarsObtained);
            }
        }

    }

    [Serializable]
    public struct LevelDataJson {
        public bool LevelCompleted;
        public int StarsObtained;
    }

}
