using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

namespace LooneyDog
{
    public class AnimationManager : MonoBehaviour
    {
        private bool _spriteTweenisPlaying=false, _imageTweenisPlaying = false,_wobbleisPlaying=false;
        [SerializeField] private Color _flashColor;
        public void SmashFromScreen(Transform Object, float TransitionSpeed) {
            
            Vector3 initialScale = Object.localScale;
            Vector3 initialPosition = Object.position;
            Quaternion initialRotation = Object.rotation;

            Object.localScale = new Vector3(10, 10, 10);
            Object.DOScale(1, TransitionSpeed).SetEase(Ease.OutElastic,1f).SetUpdate(true);
        }

        public void WobbleAnimation(Transform Object, float Duration, int shakeStrength,int shakes) 
        {
            Vector3 initialScale = Object.localScale;
            Vector3 initialPosition = Object.position;
            Quaternion initialRotation = Object.rotation;

            if (!_wobbleisPlaying)
            {
                _wobbleisPlaying = true;
                Object.DOShakeRotation(Duration, shakeStrength, shakes, 0, true).OnComplete(()=> {
                    _wobbleisPlaying = false;
                }).SetUpdate(true);
            }
        }

        public void IntTextAnimation(TextMeshProUGUI textBox, int NewValue, float transitionSpeed) {
            string textboxtext = textBox.text;
            int prev = Convert.ToInt32(textboxtext);
            textBox.DOCounter(prev, (Int32)NewValue, transitionSpeed,false).SetUpdate(true);       
        }
        /// <summary>
        /// Flashes sprite to color set in _flashColor variable in Anime
        /// Workes For Single Sprite
        /// flash from time/2
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="time"></param>
        public void Flash(SpriteRenderer sprite, float time) {
            Color initialSpriteColor = sprite.color;
            if (!_spriteTweenisPlaying) {
                sprite.DOColor(_flashColor, time / 2).SetEase(Ease.InFlash).OnStart(()=> {
                    _spriteTweenisPlaying = true;
                }).OnComplete(() =>
                {
                    sprite.DOColor(initialSpriteColor, time / 2).SetEase(Ease.InFlash).OnComplete(()=> {
                        _spriteTweenisPlaying = false;
                    });
                }).SetUpdate(true);
            }
        }
        /// <summary>
        /// Flashes image to color set in _flashColor variable in Anime
        /// Workes For Single image
        /// flash from time/2
        /// </summary>
        /// <param name="image"></param>
        /// <param name="time"></param>
        public void Flash(Image image, float time)
        {
            Color initialImageColor = image.color;
            if (!_imageTweenisPlaying)
            {
                image.DOColor(_flashColor, time / 2).SetEase(Ease.InFlash).OnStart(() => {
                    _imageTweenisPlaying = true;
                }).OnComplete(() =>
                {
                    image.DOColor(initialImageColor, time / 2).SetEase(Ease.OutFlash).OnComplete(() => {
                        _imageTweenisPlaying = false;
                    });
                }).SetUpdate(true);
            }
        }

    }
}
