using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

namespace LooneyDog
{
    [Serializable]
    public class Level
    {
        internal struct LevelData
        {
            internal bool m_status;
            internal int m_stars;
            internal int m_timeTakenToComplete;
            internal int m_difficulty;
        }

        public Level()
        {
            LvlDatas = new LevelData[3];
        }

        public int GetStars(int difficulty)
        {
            return LvlDatas[difficulty].m_stars;
        }

        public int GetTimeTakenToComplete(int difficulty)
        {
            return LvlDatas[difficulty].m_timeTakenToComplete;
        }

        public bool GetLevelStatus(int difficulty)
        {
            return LvlDatas[difficulty].m_status;
        }

        public int GetLevelNumber()
        {
            return m_levelNumber;
        }

        public void SetLevelNumber(int value)
        {
            m_levelNumber = value;
        }

        internal void AddData(int stars, int timeTakenToComplete, TimerAndScoreHandler.DIFFICULTY diff, int levelNumber)
        {
            m_levelNumber = levelNumber;
            switch (diff)
            {
                case TimerAndScoreHandler.DIFFICULTY.EASY:
                    LvlDatas[0].m_stars = stars;
                    LvlDatas[0].m_timeTakenToComplete = timeTakenToComplete;
                    LvlDatas[0].m_difficulty = (int)diff;
                    LvlDatas[0].m_status = true;
                    break;
                case TimerAndScoreHandler.DIFFICULTY.MEDIUM:
                    LvlDatas[1].m_stars = stars;
                    LvlDatas[1].m_timeTakenToComplete = timeTakenToComplete;
                    LvlDatas[1].m_difficulty = (int)diff;
                    LvlDatas[1].m_status = true;
                    break;
                case TimerAndScoreHandler.DIFFICULTY.HARD:
                    LvlDatas[2].m_stars = stars;
                    LvlDatas[2].m_timeTakenToComplete = timeTakenToComplete;
                    LvlDatas[2].m_difficulty = (int)diff;
                    LvlDatas[2].m_status = true;
                    break;
                default:
                    break;
            }

        }

        internal void UpdateData(int stars, int timeTakenToComplete, TimerAndScoreHandler.DIFFICULTY diff)
        {
            switch (diff)
            {
                case TimerAndScoreHandler.DIFFICULTY.EASY:
                    LvlDatas[0].m_stars = stars;
                    LvlDatas[0].m_timeTakenToComplete = timeTakenToComplete;
                    LvlDatas[0].m_difficulty = (int)diff;
                    LvlDatas[0].m_status = true;
                    break;
                case TimerAndScoreHandler.DIFFICULTY.MEDIUM:
                    LvlDatas[1].m_stars = stars;
                    LvlDatas[1].m_timeTakenToComplete = timeTakenToComplete;
                    LvlDatas[1].m_difficulty = (int)diff;
                    LvlDatas[1].m_status = true;
                    break;
                case TimerAndScoreHandler.DIFFICULTY.HARD:
                    LvlDatas[2].m_stars = stars;
                    LvlDatas[2].m_timeTakenToComplete = timeTakenToComplete;
                    LvlDatas[2].m_difficulty = (int)diff;
                    LvlDatas[2].m_status = true;
                    break;
                default:
                    break;
            }
        }

        private int m_levelNumber = 0;
        internal LevelData[] LvlDatas;


    }

    /*  
    *    This class is a singleton class and cannot be called with new UserDataHandler()
    *    instead call UserDatahandler.Instance() 
    */

    public class UserDataHandler
    {
        string m_dataDir = "";
        string m_fileName = "UserData.json";
        string m_filePath = "";
        static UserDataHandler instance; //singleton instance
        Dictionary<string, Level> levelDict = new Dictionary<string, Level>();

        public string FilePath { get => m_filePath; set => m_filePath = value; }

        protected UserDataHandler()
        {
            // Application directory where the file will be stored
            m_dataDir = Application.persistentDataPath;
            m_filePath = m_dataDir + "/" + m_fileName;
        }

        /*Singleton class*/
        public static UserDataHandler Instance()
        {
            if (instance == null)
            {
                instance = new UserDataHandler();
            }
            return instance;
        }

        public void PushLevelData(int stars, int timeTakenToComplete, TimerAndScoreHandler.DIFFICULTY diff, int levelNumber)
        {
            Level levelData = null;
            string key = Convert.ToString("Level" + levelNumber);
            bool hasWritten = false;

            try
            {
                if (levelDict.ContainsKey(key))
                {
                    levelData = levelDict[key];
                    if (stars > levelData.LvlDatas[(int)diff].m_stars)
                    {
                        levelData.UpdateData(stars, timeTakenToComplete, diff);
                        Debug.Log("Updating " + key + " to map");
                    }
                    else
                    {
                        //got less stars than previous high score 
                        Debug.Log("Updating " + key + " to map");
                    }

                    levelDict[key] = levelData;
                }
                else
                {
                    levelData = new Level();
                    levelData.AddData(stars, timeTakenToComplete, diff, levelNumber);
                    Debug.Log("Adding KV to map: " + key + ":" + levelData.ToString());
                    levelDict.Add(key, levelData);
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }

            //do this async
            hasWritten = WriteLevelData();

        }

        private bool WriteLevelData()
        {
            // { /*main obj*/
            //     "1": { /*Level obj*/
            //         "easy": {  /*Difficulty obj1*/
            //             "stars": 3,
            //             "timetaken": ""
            //         },
            //         "medium": { /*Difficulty obj2*/
            //             "stars": 3,
            //             "timetaken": ""
            //         },
            //         "hard": { /*Difficulty obj3*/
            //             "stars": 3,
            //             "timetaken": ""
            //         }
            //     }
            // }
            bool retval = false;
            try
            {
                JObject mainObj = new JObject();
                JProperty[] levelProp = new JProperty[levelDict.Count + 1];
                string json = "";
                for (int i = 1; i <= levelDict.Count; ++i)
                {
                    Level lvl = levelDict["Level" + i.ToString()];

                    JObject easyLvlData = new JObject(
                        new JProperty("stars", lvl.LvlDatas[0].m_stars.ToString()),
                        new JProperty("timetaken", lvl.LvlDatas[0].m_timeTakenToComplete.ToString()),
                        new JProperty("status", lvl.LvlDatas[0].m_status.ToString()));
                    JObject medLvlData = new JObject(
                        new JProperty("stars", lvl.LvlDatas[1].m_stars.ToString()),
                        new JProperty("timetaken", lvl.LvlDatas[1].m_timeTakenToComplete.ToString()),
                        new JProperty("status", lvl.LvlDatas[1].m_status.ToString()));
                    JObject highLvlData = new JObject(
                        new JProperty("stars", lvl.LvlDatas[2].m_stars.ToString()),
                        new JProperty("timetaken", lvl.LvlDatas[2].m_timeTakenToComplete.ToString()),
                        new JProperty("status", lvl.LvlDatas[2].m_status.ToString()));

                    JProperty diffProp1 = diffProp1 = new JProperty("easy", easyLvlData);
                    JProperty diffProp2 = diffProp2 = new JProperty("medium", medLvlData);
                    JProperty diffProp3 = diffProp3 = new JProperty("hard", highLvlData);

                    levelProp[i] = new JProperty(lvl.GetLevelNumber().ToString(), new JObject(diffProp1), new JObject(diffProp2), new JObject(diffProp3));
                    mainObj.Add(levelProp[i]);
                }

                json = mainObj.ToString();
                Debug.Log("Data written to file: " + json);
                File.WriteAllText(m_filePath, json);

                retval = true;
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to write user data: " + ex.Message + "stacktrace: " + ex.StackTrace.ToString());
                retval = false;
            }

            return retval;
        }

        public void PullLevelData()
        {
            int levelCount = ReadLevelData();
        }

        private int ReadLevelData()
        {
            int size = 0;
            try
            {
                string fileData = File.ReadAllText(m_filePath);
                if (string.IsNullOrEmpty(fileData))
                {
                    return -1;
                }

                JObject o1 = JObject.Parse(fileData);
                for (int i = 1; i <= o1.Count; ++i)
                {
                    JArray levelArray = JArray.Parse(o1[i.ToString()].ToString());
                    Level level = generateLevelFromJson(levelArray);
                    level.SetLevelNumber(i);
                    string key = "Level" + i.ToString();
                    levelDict.Add(key, level);
                }
                size = levelDict.Count;
            }
            catch (Exception ex)
            {
                size = -1;
            }

            return size;
        }

        private Level generateLevelFromJson(JArray levelArray)
        {
            Level level = new Level();

            JObject easyObj = JObject.Parse(levelArray[0]["easy"].ToString());
            JObject medObj = JObject.Parse(levelArray[1]["medium"].ToString());
            JObject hardObj = JObject.Parse(levelArray[2]["hard"].ToString());

            /*Easy level*/
            level.LvlDatas[0].m_difficulty = (int)TimerAndScoreHandler.DIFFICULTY.EASY;
            if (!int.TryParse(easyObj["stars"].ToString(), out level.LvlDatas[0].m_stars))
            {
                level.LvlDatas[0].m_stars = 0;
            }
            if (!int.TryParse(easyObj["timetaken"].ToString(), out level.LvlDatas[0].m_timeTakenToComplete))
            {
                level.LvlDatas[0].m_timeTakenToComplete = 0;
            }
            if (!bool.TryParse(easyObj["status"].ToString(), out level.LvlDatas[0].m_status))
            {
                level.LvlDatas[0].m_status = false;
            }

            /*Medium level*/
            level.LvlDatas[1].m_difficulty = (int)TimerAndScoreHandler.DIFFICULTY.EASY;
            if (!int.TryParse(medObj["stars"].ToString(), out level.LvlDatas[1].m_stars))
            {
                level.LvlDatas[1].m_stars = 0;
            }
            if (!int.TryParse(medObj["timetaken"].ToString(), out level.LvlDatas[1].m_timeTakenToComplete))
            {
                level.LvlDatas[1].m_timeTakenToComplete = 0;
            }
            if (!bool.TryParse(medObj["status"].ToString(), out level.LvlDatas[1].m_status))
            {
                level.LvlDatas[1].m_status = false;
            }

            /*Hard Level*/
            level.LvlDatas[2].m_difficulty = (int)TimerAndScoreHandler.DIFFICULTY.EASY;
            if (!int.TryParse(hardObj["stars"].ToString(), out level.LvlDatas[2].m_stars))
            {
                level.LvlDatas[2].m_stars = 0;
            }
            if (!int.TryParse(hardObj["timetaken"].ToString(), out level.LvlDatas[2].m_timeTakenToComplete))
            {
                level.LvlDatas[2].m_timeTakenToComplete = 0;
            }
            if (!bool.TryParse(hardObj["status"].ToString(), out level.LvlDatas[2].m_status))
            {
                level.LvlDatas[2].m_status = false;
            }

            return level;
        }

        public Level[] GetAllLevel()
        {
            List<Level> levelList = new List<Level>();
            if (levelDict.Count <= 0)
            {
                // returns empty list
                return levelList.ToArray();
            }

            foreach (var keyValuePair in levelDict)
            {
                levelList.Add(keyValuePair.Value);
            }
            return levelList.ToArray();
        }

        public Level GetLevel(int levelNumber)
        {
            Level lvl = null;
            string key = "Level" + levelNumber.ToString();
            if (!levelDict.TryGetValue(key, out lvl))
            {
                Debug.LogError("Failed to get level object for level: " + levelNumber);
            }

            return lvl;
        }

        /*Call this method only when the game is exiting.*/
        public void CleanUp()
        {
            Debug.Log("Cleaning up data before shutdown");
            levelDict.Clear();
        }
    }
}
