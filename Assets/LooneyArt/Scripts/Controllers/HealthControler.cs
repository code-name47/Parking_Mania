using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class HealthControler : MonoBehaviour
    {
        public float Health { get { return _health; }set { _health = value; } }
        public float Armor { get { return _armor; }set { _armor = value; } }

        [SerializeField] private float _health, _armor;
        [SerializeField] private float _damageReductionByArmor;

        public void Start()
        {
            GameManager.Game.Skin.ApplyHealthAndArmor(this);
            GameManager.Game.Level.GameCompletedBool = false;
        }
        public void DamageHealth(float _damage) {

            if (_armor > 0)
            {
                _health -= _damage * (_damageReductionByArmor / 100); // Normalizing
                _armor -= _damage;  // Normalizing
            }
            else
            {
                _health -= _damage;
            }
            GameManager.Game.Screen.GameScreen.HealthMeter.SetMeters(_health, _armor);
            if (_health < 0)
            {
                GameManager.Game.Level.GameCompleted(false);
            }
        }
        
       
    }
}
