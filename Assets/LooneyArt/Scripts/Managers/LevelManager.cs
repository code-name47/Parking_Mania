using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class LevelManager : MonoBehaviour
    {
        public int LevelNumber { get { return _levelNumber; }set { _levelNumber = value; } }
        public GameDifficulty Difficulty { get { return _difficulty; }set { _difficulty = value; } }
        public int Reward { get { return _reward; } private set {} }

        [SerializeField] private int _levelNumber;
        [SerializeField] private GameDifficulty _difficulty;
        [SerializeField] private int _reward;
        private LevelDataStruct Currentleveldata;

        [SerializeField] private LevelData[] levelDatas; 

        public void GetLevelData(int levelNumber, GameDifficulty gamedifficulty) {
            if (levelNumber == levelDatas[levelNumber].LevelNumber)
            {
                Currentleveldata = levelDatas[levelNumber].GetLevelData(gamedifficulty);
            }
            else
            {
                Debug.Log("LevelNumber Mismatch");
            }
        }

        public void CalculateReward(int starsobtained) {
            switch (starsobtained) {
                case 1:
                    _reward = Currentleveldata.rewardfor1Star;
                    break;
                case 2:
                    _reward = Currentleveldata.rewardfor2Star;
                    break;
                case 3:
                    _reward = Currentleveldata.rewardfor3Star;
                    break;
            }
        }

        public void LevelCompleted(bool levelCompleted,int starsObtained) {
            levelDatas[_levelNumber].SetLevelData(levelCompleted, starsObtained);
        }

        public int GetStarForLevel(int levelno) {
            return levelDatas[levelno].StarsObtained;
        }
    }
}
