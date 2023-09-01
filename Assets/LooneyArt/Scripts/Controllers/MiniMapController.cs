using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class MiniMapController : MonoBehaviour
    {
        private Transform _followObject;
        private Transform _goalObject;
         [SerializeField] private Transform _goalPointer;
         [SerializeField] private float turn_speed;
        [SerializeField] private float _pointerDisapperDistance;


        public void SetMiniMapDetails(Transform Car,Transform Goal)
        {
            _followObject = Car;
            _goalObject = Goal;

        }

        private void Update()
        {
            if (_followObject!=null && _goalObject!=null)
            {
                var dir = _goalObject.position - _followObject.position;
                var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
                _goalPointer.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
                if (Vector2.Distance(_goalObject.position, _followObject.position) < _pointerDisapperDistance)
                {
                    if (_goalPointer.gameObject.activeSelf)
                    {

                        _goalPointer.gameObject.SetActive(false);
                    }
                }
                else {
                    if (!_goalPointer.gameObject.activeSelf)
                    {

                        _goalPointer.gameObject.SetActive(true);
                    }
                };
            }
        }


    }

}
