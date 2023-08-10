using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class LevelButton : MonoBehaviour
    {
        public GameObject[] Stars { get { return _stars; }set { _stars = value; } }

        [SerializeField] private GameObject[] _stars = new GameObject[3];

        public void SetActiveStars(int noOfStars) {
            for (int i = 0; i < noOfStars; i++) {
                _stars[i].SetActive(true);
            }
        }

    }
}