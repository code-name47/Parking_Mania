using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace LooneyDog
{

    public class PoolManager : MonoBehaviour
    {
        [Header("ObjectPool")]
        private ObjectPool<GameObject> _aiHideColliderMovingSpotPool;
        [SerializeField] private bool _UseAiHideColliderMovingSpotPool;

        [Header("Prefabs")]
        [SerializeField] GameObject _raycastCollider;


        


        private void Start()
        {
           
        }
        public GameObject SpwanRayCastColliders(Vector2 spwanableArea)
        {
            Debug.Log("Pool Manager Accesible");
            GameObject RayCastCollider;
            if (_aiHideColliderMovingSpotPool != null)
            {
                
                RayCastCollider = _UseAiHideColliderMovingSpotPool ? _aiHideColliderMovingSpotPool.Get() : Instantiate(_raycastCollider,spwanableArea, Quaternion.identity);
                Debug.Log("Raycast collider " + RayCastCollider.name);
            }
            else
            {
                _aiHideColliderMovingSpotPool = new ObjectPool<GameObject>(() =>
                {
                    return Instantiate(_raycastCollider, spwanableArea, Quaternion.identity);
                }, _collider =>
                {
                    _collider.gameObject.SetActive(true);
                }, _collider =>
                {
                    _collider.gameObject.SetActive(false);
                }, _collider =>
                {
                    Destroy(_collider.gameObject);
                }, false, 100, 1000);

                RayCastCollider = _UseAiHideColliderMovingSpotPool ? _aiHideColliderMovingSpotPool.Get() : Instantiate(_raycastCollider, spwanableArea,Quaternion.identity);
                Debug.Log("Raycast collider after Pool" + RayCastCollider.name);
            }
            return RayCastCollider;
        }

        public void KillRaycastColliders(GameObject _collider)
        {
            if (_UseAiHideColliderMovingSpotPool)
            {
                _aiHideColliderMovingSpotPool.Release(_collider);
            }
            else
            {
                Destroy(_collider);
            }
        }
    }

}