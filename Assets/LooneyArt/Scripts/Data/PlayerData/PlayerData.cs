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

        [SerializeField] private int HIGHSCORE;
        [SerializeField] private float CAMERASENSITIVITY = 0.5f;
        [SerializeField] private bool SHOWTUTORIAL = true;

        public void SetNormalizedCameraSensitivity(float value)
        {
            CAMERASENSITIVITY = value / 100;
        }

    }

}
