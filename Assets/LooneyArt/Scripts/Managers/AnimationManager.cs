using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace LooneyDog
{
    public class AnimationManager : MonoBehaviour
    {
        public void SmashFromScreen(Transform Object, float TransitionSpeed) {
            
            Vector3 initialScale = Object.localScale;
            Vector3 initialPosition = Object.position;
            Quaternion initialRotation = Object.rotation;

            Object.localScale = new Vector3(10, 10, 10);
            Object.DOScale(1, TransitionSpeed).SetEase(Ease.OutElastic,1f);
        }

        public void WobbleAnimation(Transform Object, float Duration, int shakeStrength,int shakes) 
        {
            Vector3 initialScale = Object.localScale;
            Vector3 initialPosition = Object.position;
            Quaternion initialRotation = Object.rotation;

            Object.DOShakeRotation(Duration, shakeStrength,shakes,0,true);
        }

    }
}
