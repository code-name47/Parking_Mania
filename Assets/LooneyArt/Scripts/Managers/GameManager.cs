using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LooneyDog
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Game;

        public DataManager Data;
        public LevelManager Level;
        public ScreenManager Screen;
        public AnimationManager Anime;
        /*public SoundManager Sound;
        public AdManager Admob;*/
        /* public SkinManager Skin;
         public SoundManager Sound;*/


        private void Awake()
        {
            if (Game == null)
            {
                DontDestroyOnLoad(gameObject);
                Game = this;
                Initialize();
            }
            else if (Game != this)
            {
                Destroy(gameObject);
            }

        }

        private void Start()
        {
            /*if (SystemInfo.deviceType != DeviceType.Desktop)
            {
                Application.targetFrameRate = 45;
            }*/
        }

        private void Initialize()
        {
            if (Data == null) { Data = gameObject.GetComponent<DataManager>(); }
            if (Level == null) { Level = gameObject.GetComponent<LevelManager>(); }
            if (Anime == null) { Anime = gameObject.GetComponent<AnimationManager>(); }

            /*if (Sound == null) { Sound = gameObject.GetComponent<SoundManager>(); }
            if (Admob == null) { Admob = gameObject.GetComponent<AdManager>(); }*/
            /*if (Skin == null) { Skin = gameObject.GetComponent<SkinManager>(); }*/
            if (Screen == null) { Screen = gameObject.GetComponent<ScreenManager>(); }

            
        }
    }
}
