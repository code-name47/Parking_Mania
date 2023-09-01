using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class CameraController : MonoBehaviour
    {
        //public MiniMapController MiniMapContr { get { return _miniMapContr; }set { _miniMapContr = value; } }

        [SerializeField] Transform _cameraObject;
        [SerializeField] Transform _followObject;
        [SerializeField] float _distancefromObject;
        [SerializeField] float _cameraFollowSpeed;
        [SerializeField] bool _move=false;
        //[SerializeField] MiniMapController _miniMapContr;
        

        public void SetCameraFollowObject(Transform car) {
            _followObject = car;
            _move = true;
        }


        private void Update()
        {
            Vector3 cameraPosition = new Vector3(_cameraObject.localPosition.x,
                                                          _cameraObject.localPosition.y,
                                                          _cameraObject.localPosition.z);
            Vector3 followPosition = new Vector3(_followObject.localPosition.x,
                                                  _followObject.localPosition.y,
                                                  _cameraObject.localPosition.z);
            if (_move) {
                if (Vector3.Distance(cameraPosition, followPosition) > _distancefromObject) {
                    _cameraObject.localPosition = Vector3.Lerp(cameraPosition, followPosition,_cameraFollowSpeed*Time.deltaTime);  
                }
            }
        }
    }
}
