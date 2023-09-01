using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LooneyDog
{

    public class LevelButton : MonoBehaviour
    {
        public int ButtonId { get { return _buttonId; }set { _buttonId = value; } }
        public GameObject[] Stars { get { return _stars; }set { _stars = value; } }

        public TextMeshProUGUI LevelText { get { return _levelText; }set { _levelText = value; } } 

        [SerializeField] private GameObject[] _stars = new GameObject[3];
        [SerializeField] private int _buttonId;
        [SerializeField] TextMeshProUGUI _levelText;

        public void SetActiveStars(int noOfStars) {
            setLevelText();
            for (int i = 0; i < noOfStars; i++) {
                _stars[i].SetActive(true);
                GameManager.Game.Anime.SmashFromScreen(_stars[i].transform, 1f);
            }
        }

        private void setLevelText() {
            _levelText.text = "" + _buttonId;
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