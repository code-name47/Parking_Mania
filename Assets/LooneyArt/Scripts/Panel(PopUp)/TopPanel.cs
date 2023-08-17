using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LooneyDog
{

    public class TopPanel : MonoBehaviour
    {
        public TextMeshProUGUI CoinText { get { return _coinText; }set { _coinText = value; } }

        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private float _transitionSpeed;

        private void Start()
        {
            //UpdateCoinData();
        }

        private void OnEnable()
        {
            UpdateCoinData();
        }
        public int GetCoinData() {
            return GameManager.Game.Data.player.GetCoinData();
        }

        public void SetCoinData() { 
        
        }

        public void UpdateCoinData() {
            GameManager.Game.Anime.IntTextAnimation(_coinText, GetCoinData(), _transitionSpeed);
        }
    }
}
