using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
namespace LooneyDog
{
    public class Main_Menu : MonoBehaviour
    {
        public GameObject levelSelector;
        public GameObject areYouSurePanel;
        public GameObject levelLockedPanel;
        public GameObject startsUi;
        public GameObject[] LevelArray = new GameObject[10];
        public int Stars;
        public static int Coins;
        private GameObject Clicked_Level;
        public GameObject Difficulty_DropDown;
        public static int Lvldifficulty = (int)TimerAndScoreHandler.DIFFICULTY.EASY;
        internal Level[] allLevels;

        public void Start()
        {
            UserDataHandler.Instance().PullLevelData();
            allLevels = UserDataHandler.Instance().GetAllLevel();
            // if(allLevels.Length == 0)
            // {
            /*This should only happend during first time.*/
            //     Stars = 2;
            // }
            foreach (var level in allLevels)
            {
                Stars += level.GetStars(Lvldifficulty);
            }

            populateStars();


        }
        public void populateStars()
        {
            for (int i = 0; i < allLevels.Length; ++i)
            {
                LevelArray[i].GetComponent<EnableStars>().starcount = allLevels[i].GetStars(Lvldifficulty);
            }
        }
        public void onClickStart()
        {
            levelSelector.SetActive(true);
        }

        public void onclickLevel()
        {
            Clicked_Level = EventSystem.current.currentSelectedGameObject;
            if (Clicked_Level.transform.Find("Level_Unlocked").gameObject.activeSelf == true)
            {
                areYouSurePanel.SetActive(true);
            }
            else
            {
                levelLockedPanel.SetActive(true);
            }
        }

        private void unlockLevelsByStars()
        {
            //int i = Stars / 2;
            for (int i = 0; i < LevelArray.Length; i++)
            {
                if (i < (Stars / 2) + 1)
                {
                    LevelArray[i].transform.Find("Level_Unlocked").gameObject.SetActive(true);
                    LevelArray[i].transform.Find("Stars").gameObject.SetActive(true);
                }
                else
                {
                    LevelArray[i].transform.Find("Level_Unlocked").gameObject.SetActive(false);
                    LevelArray[i].transform.Find("Stars").gameObject.SetActive(false);
                }

            }
        }

        public void OnDropDownDifficulty()
        {
            Lvldifficulty = Difficulty_DropDown.GetComponent<TMPro.TMP_Dropdown>().value;
            int temp = 0;
            //--------------------- get data from json
            UserDataHandler.Instance().PullLevelData();
            allLevels = UserDataHandler.Instance().GetAllLevel();

            foreach (var level in allLevels)
            {
                temp += level.GetStars(Lvldifficulty);

            }
            Stars = temp;
            populateStars();



            //---------------------------------------
        }

        public void onClickYesOnSure()
        {
            int i;
            int scenenumber = 0;
            for (i = 0; i < LevelArray.Length; i++)
            {
                if (Clicked_Level.name == LevelArray[i].name)
                {
                    scenenumber = i + 1;
                }
            }
            SceneManager.LoadScene(scenenumber);

        }
        public void onClickNoOnSure()
        {
            areYouSurePanel.SetActive(false);

        }

        public void onClickBackOnSure()
        {
            levelLockedPanel.SetActive(false);
        }
        public void onClickExit()
        {
            Application.Quit();
        }

        public void Update()
        {
            unlockLevelsByStars();
            startsUi.GetComponent<TMPro.TMP_Text>().text = "" + Stars;

        }
    }
}
