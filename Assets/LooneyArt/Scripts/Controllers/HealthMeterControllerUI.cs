using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LooneyDog
{

    public class HealthMeterControllerUI : MonoBehaviour
    {
        public Image HealthMeter { get { return _healthMeter; } set { _healthMeter = value; } }
        public Image ArmorMeter { get { return _armorMeter; } set { _armorMeter = value; } }

        [SerializeField] private Image _healthMeter, _armorMeter;
        [SerializeField] private float _padding, _speed,_health,_armor,_flashTime;

        public void Update()
        {

            LerpHealthMeter(_health, _armor);
        }

        public void UpdateHealthMeter(float health, float armor) {
            _healthMeter.fillAmount = health / 100;
            _armorMeter.fillAmount = armor / 100;
        }


        public void SetMeters(float health, float armor)
        {
            flashMeters();
            _health = health / 100;
            _armor = armor / 100;
        }
        public void LerpHealthMeter(float health, float armor) {
            _healthMeter.fillAmount = Mathf.Lerp(_healthMeter.fillAmount, health, _speed * Time.deltaTime);
            _armorMeter.fillAmount = Mathf.Lerp(_armorMeter.fillAmount, armor, _speed * Time.deltaTime);
        }

        public void flashMeters() {
            GameManager.Game.Anime.Flash(_healthMeter, _flashTime);
            GameManager.Game.Anime.Flash(_armorMeter, _flashTime);
            GameManager.Game.Anime.WobbleAnimation(transform, _flashTime, 40, 5);
        }
    }
}
