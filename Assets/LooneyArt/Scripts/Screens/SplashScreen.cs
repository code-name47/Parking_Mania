using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace LooneyDog
{
    public class SplashScreen : MonoBehaviour
    {
        public Image LooneyDogLogo {get{ return _looneyDogLogo; }set { _looneyDogLogo = value; } }
        [SerializeField] private Image _looneyDogLogo;
        [SerializeField] private float _transitionSpeed;
        [SerializeField] private float _logoAppearDelay;
        [SerializeField] private float _logoScreenTime;

        private void OnEnable()
        {
            StartCoroutine(LogoAppear());
        }


        private IEnumerator LogoAppear() {
            yield return new WaitForSeconds(_logoAppearDelay);
            _looneyDogLogo.DOFade(1, _transitionSpeed).OnComplete(()=> {
                StartCoroutine(LogoDisappear());
            });
        }

        private IEnumerator LogoDisappear() {
            yield return new WaitForSeconds(_logoScreenTime);
            GameManager.Game.Screen.LoadFadeScreen(GameManager.Game.Screen.Splsh.gameObject, GameManager.Game.Screen.Home.gameObject);
        }
    }
}