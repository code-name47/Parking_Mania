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

        public bool[] VehicleUnlocked { get { return _vehicleUnclocked; }set { _vehicleUnclocked = value; } }
        public VehicleID CurrentActiveSkin { get { return _currentActiveSkin; }set { _currentActiveSkin = value; } }
        public int Coins { get { return COINS; }set { COINS = value; } }

        [SerializeField] private int HIGHSCORE;
        [SerializeField] private float CAMERASENSITIVITY = 0.5f;
        [SerializeField] private bool SHOWTUTORIAL = true;
        [SerializeField] private int COINS;
        [SerializeField] private LevelDataJson[] _leveldatajson;
        [SerializeField] private bool[] _vehicleUnclocked;
        [SerializeField] private VehicleID _currentActiveSkin;

        public void Check() {
            if (_leveldatajson == null)
            {
                _leveldatajson = new LevelDataJson[GameManager.Game.Level.Leveldatas.Length];
                _vehicleUnclocked = new bool[GameManager.Game.Skin.Vehicles.Length];
                GetLevelDataToScriptableObjects();
                GetVehicleDataFromScriptableObjects();
            }
            else {
                SetLevelDataToScriptableObjects();
                SetVehicleDataToScriptableObjects();
            }
            //GameManager.Game.Screen.Top.UpdateCoinData();
        }

        public void AddRewardCoins(int coins) {
            COINS = COINS + coins;
        }

        public void SubstractRewardCoins(int coins)
        {
            if ((COINS - coins) > 0)
            {
                COINS = COINS - coins;
            }
            else
            {
                COINS = 0;
            }
        }

        public bool CheckIfEnoughCoins(int value) {
            if (value > COINS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetCoinData() {
            return (Int32)COINS;
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

        public void SetVehicleDataToScriptableObjects() {
            for (int i = 0; i < GameManager.Game.Skin.Vehicles.Length; i++)
            {
                GameManager.Game.Skin.Vehicles[i].Unlocked = _vehicleUnclocked[i];
            }
        }

        public void SetCurrentSkin(VehicleID vehicleId) {
            _currentActiveSkin = vehicleId;
        }

        public VehicleID GetCurrentSkin() {
            return _currentActiveSkin;
        }
        public void GetVehicleDataFromScriptableObjects() {
            for (int i = 0; i < GameManager.Game.Skin.Vehicles.Length; i++)
            {
                _vehicleUnclocked[i] = GameManager.Game.Skin.Vehicles[i];
            }
        }
        public void GetLevelDataToScriptableObjects()
        {
            for (int i = 0; i < GameManager.Game.Level.Leveldatas.Length; i++)
            {
                _leveldatajson[i].LevelCompleted = GameManager.Game.Level.Leveldatas[i].LevelCompleted;
                _leveldatajson[i].StarsObtained = GameManager.Game.Level.Leveldatas[i].StarsObtained;
            }
        }

    }

    [Serializable]
    public struct LevelDataJson {
        public bool LevelCompleted;
        public int StarsObtained;
    }

    public struct VehicleDataJson
    {
        public bool Unlocked;
        public int currentSkin;
    }

}
