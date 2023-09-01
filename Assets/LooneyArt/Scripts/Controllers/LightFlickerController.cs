using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering.Universal;
//using UnityEngine

namespace LooneyDog
{

    public class LightFlickerController : MonoBehaviour
    {
        [SerializeField] private  Light2D _2dFlickerLightObject;
        [SerializeField] private float _lowIntensity, _highIntensity, _speed,_intensityBoundarypadding = 0.1f;
        [SerializeField] private bool _waitingPeriodOver,_speedRandomGeneration,_flicker;
        private int rnd;

        private void Update()
        {
            if (_flicker)
            {
                if (_waitingPeriodOver)
                {
                    _2dFlickerLightObject.intensity = Mathf.Lerp(_2dFlickerLightObject.intensity, _highIntensity, _speed * Time.deltaTime);
                    if (_2dFlickerLightObject.intensity >= _highIntensity - _intensityBoundarypadding)
                    {
                        _waitingPeriodOver = false;
                        if (_speedRandomGeneration)
                        {
                            _speed = TrueRandomWrtTime();
                        }
                    }
                }
                else
                {
                    _2dFlickerLightObject.intensity = Mathf.Lerp(_2dFlickerLightObject.intensity, _lowIntensity, _speed * Time.deltaTime);
                    if (_2dFlickerLightObject.intensity <= _lowIntensity + _intensityBoundarypadding)
                    {
                        _waitingPeriodOver = true;
                    }

                }
            }
        }

        private int TrueRandomWrtTime() {
            return rnd = Random.Range(1, 10);
        }
    }
}
