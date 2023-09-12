using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LooneyDog
{
    public class LevelManager : MonoBehaviour
    {
        public int LevelNumber { get { return _levelNumber; }set { _levelNumber = value; } }
        public GameDifficulty Difficulty { get { return _difficulty; }set { _difficulty = value; } }
        public int Reward { get { return _reward; } private set {} }
        public int StartsObtained { get { return _starsObtained; }private set { } }

        public LevelData[] Leveldatas { get { return levelDatas; }set { levelDatas = value; } }

        public bool GameCompletedBool { get { return _gameCompletedBool; }set { _gameCompletedBool = value; } }

        public Car_Controller CurrentActiveCar { get { return _currentActiveCar; }set { _currentActiveCar = value; } }

        [SerializeField] private int _levelNumber;
        [SerializeField] private GameDifficulty _difficulty;
        [SerializeField] private int _reward;
        [SerializeField] private int _starsObtained;
        [SerializeField] private bool _gameCompletedBool;
        private LevelDataStruct Currentleveldata;

        [SerializeField] private LevelData[] levelDatas;
        [SerializeField] Car_Controller _currentActiveCar;

        public void GetLevelData(int levelNumber, GameDifficulty gamedifficulty) {
            if (levelNumber == levelDatas[levelNumber-1].LevelNumber)//-1 coz sciptable object array starts from 0
            {
                Currentleveldata = levelDatas[levelNumber-1].GetLevelData(gamedifficulty);//-1 coz sciptable object array starts from 0
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

        public void SaveLevelDataToSO(bool levelCompleted,int starsObtained) {
            if (levelDatas[_levelNumber - 1].LevelCompleted)
            {
                if (starsObtained > levelDatas[_levelNumber - 1].StarsObtained)
                {
                    levelDatas[_levelNumber - 1].SetLevelData(levelCompleted, starsObtained);
                    GameManager.Game.Data.player.SetDataToJson(levelCompleted, starsObtained, _levelNumber - 1);
                }
            }
            else
            {
                levelDatas[_levelNumber - 1].SetLevelData(levelCompleted, starsObtained);
                GameManager.Game.Data.player.SetDataToJson(levelCompleted, starsObtained, _levelNumber - 1);
            }
        }

        public int GetStarForLevel(int levelno) {
            return levelDatas[levelno].StarsObtained;
        }

        public void GameCompleted(bool GameStatus) {
            if (!_gameCompletedBool)
            {
                _gameCompletedBool = true;
                int starsObtained = CalculateStars();
                if (GameStatus)
                {
                    GameManager.Game.Screen.GameScreen.GameWin(starsObtained, _reward);
                    SaveLevelDataToSO(GameStatus, starsObtained);
                    AddCoinReward(_reward);
                }
                else
                {
                    GameManager.Game.Screen.GameScreen.GameLose();
                }
            }
        }

        public int CalculateStars() {
            int starsCount = 0;
            int _timeTakenForCompletion = GameManager.Game.Screen.GameScreen.StopWatchTimer.GetElaspedTime();
            if (_timeTakenForCompletion < Currentleveldata.timeRequriedFor_3Star)
            {
                starsCount = 3;
            }
            else if (_timeTakenForCompletion < Currentleveldata.timeRequriedFor_2Star)
            {
                starsCount = 2;
            }
            else 
            {
                starsCount = 1;
            }

            CalculateReward(starsCount);
            return starsCount;
        }

        public void LoadLevel(GameObject CallingScreen, int levelnumber)
        {
            GameManager.Game.Screen.LoadFadeScreen(CallingScreen, GameManager.Game.Screen.Load.gameObject);
            GameManager.Game.Screen.Load.SetSceneIndexAndDifficulty(levelnumber, _difficulty);
        }

        public void SetCurrentLevelDetails(int levelnumber, GameDifficulty difficulty) //Called From LoadingScreen 
        {
            _levelNumber = levelnumber;
            _difficulty = difficulty;
        }

        public void AddCoinReward(int reward) {
            GameManager.Game.Data.player.AddRewardCoins(reward);
            GameManager.Game.Screen.Top.UpdateCoinData();
        }

        public void SetCurrentActiveCar(Car_Controller _currentCar) {
            _currentActiveCar = _currentCar;
            //GameManager.Game.Screen.GameScreen.SetCarControllerToUi(transform, _nextObjective);
        }

    }
}
