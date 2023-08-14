using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class LevelButton : MonoBehaviour
    {
        public int ButtonId { get { return _buttonId; }set { _buttonId = value; } }
        public GameObject[] Stars { get { return _stars; }set { _stars = value; } }

        [SerializeField] private GameObject[] _stars = new GameObject[3];
        [SerializeField] private int _buttonId;

        public void SetActiveStars(int noOfStars) {
            for (int i = 0; i < noOfStars; i++) {
                _stars[i].SetActive(true);
                GameManager.Game.Anime.SmashFromScreen(_stars[i].transform, 1f);
            }
        }

        public void DisableActiveStars()
        {
            for (int i = 0; i < 3; i++)// Total number of stars
            {
                _stars[i].SetActive(false);
                GameManager.Game.Anime.SmashFromScreen(_stars[i].transform, 1f);
            }
        }

    }
}