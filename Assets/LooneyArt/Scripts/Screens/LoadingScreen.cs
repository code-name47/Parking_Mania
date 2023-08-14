using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LooneyDog
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private int _sceneIndexToBeLoaded;
        [SerializeField] GameDifficulty _difficultySet;
        [SerializeField] private float _waitTimeBeforeLoad;

        private void OnEnable()
        {
            StartCoroutine(WaitTimeBeforeLoad());
        }

        IEnumerator WaitTimeBeforeLoad() {
            yield return new WaitForSeconds(_waitTimeBeforeLoad);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneIndexToBeLoaded, LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            //yield return new WaitForSeconds(2f);
            LoadLevel();
        }

        private void LoadLevel() {
            if (_sceneIndexToBeLoaded > 0)
            {
                GameManager.Game.Screen.LoadFadeScreen(GameManager.Game.Screen.Load.gameObject, GameManager.Game.Screen.GameScreen.gameObject);
                GameManager.Game.Level.SetCurrentLevelDetails(_sceneIndexToBeLoaded, _difficultySet);
                GameManager.Game.Level.GetLevelData(_sceneIndexToBeLoaded, _difficultySet);
            }
            else
            {
                GameManager.Game.Screen.LoadFadeScreen(GameManager.Game.Screen.Load.gameObject, GameManager.Game.Screen.Home.gameObject);
            }
        }
        public void SetSceneIndexAndDifficulty(int sceneindex, GameDifficulty gameDifficulty) {
            _sceneIndexToBeLoaded = sceneindex;
            _difficultySet = gameDifficulty;
        }
    }
}
