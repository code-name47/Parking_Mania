using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class ResultPopUp : MonoBehaviour
    {
        public WinpopUp WinPanel { get { return _winPanel; }set { _winPanel = value; } }
        public LosePopUP LosePanel { get { return _losePanel; } set { _losePanel = value; } }

        [SerializeField] private WinpopUp _winPanel;
        [SerializeField] private LosePopUP _losePanel;
        [SerializeField] private int _starsObtained;

        public void SetGameCompleteStatus(bool GameStatus,int starsObtained) {
            _winPanel.gameObject.SetActive(false);
            _losePanel.gameObject.SetActive(false);
            _starsObtained = starsObtained;
            if (GameStatus)
            {
                _winPanel.gameObject.SetActive(true);
                _losePanel.gameObject.SetActive(false);
            }
            else
            {
                _winPanel.gameObject.SetActive(false);
                _losePanel.gameObject.SetActive(true);
            }
        }

        private void OnEnable()
        {
            StartCoroutine(WaitForScreenPopUp(_starsObtained));
        }
        public IEnumerator WaitForScreenPopUp(int starsObtained) {
            
            yield return new WaitForSeconds(GameManager.Game.Screen.GameScreen.TransitionSpeed);
            _winPanel.ActivateStars(starsObtained);
        }
    }
}
