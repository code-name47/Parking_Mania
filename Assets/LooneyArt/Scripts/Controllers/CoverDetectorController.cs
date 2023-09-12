using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LooneyDog
{
    public class CoverDetectorController : MonoBehaviour
    {
        [SerializeField] float _lineOfSightRadius,_detectionSpeed, _pathradius=5f,_raycastColliderSize =0.01f, enemyLineOfSight=500;
        CircleCollider2D _lineOfSightCollider;
        [SerializeField] List<Transform> _covers;
        [SerializeField] List<Vector2> _movablePoints;
        [SerializeField] List<Vector2> _hideablePoints;
        [SerializeField] Transform _enemy;
        [SerializeField] LayerMask layerMask;
        [SerializeField] private AIController _aicontroller;
        private bool _detectingCovers = false;
        private NavMeshAgent _agent;
        NavMeshPath path;

        Vector2 center;
        Vector2[] circumferencePoints = new Vector2[6];
        public void DetectCovers() {
            
            if (_lineOfSightCollider == null)
            {
                _lineOfSightCollider = gameObject.AddComponent<CircleCollider2D>();
            }
            _lineOfSightCollider.enabled = true;
            _lineOfSightCollider.isTrigger = true;
            _lineOfSightCollider.radius = 0;
            _detectingCovers = true;
            _covers.Clear();
            _movablePoints.Clear();

            center = new Vector2(transform.position.x, transform.position.y);
            path = new NavMeshPath();
        }

        public void Update()
        {
            if (_detectingCovers) {
                if (_lineOfSightCollider.radius < _lineOfSightRadius)
                {
                    _lineOfSightCollider.radius = Mathf.Lerp(_lineOfSightCollider.radius, (_lineOfSightRadius + 1), _detectionSpeed * Time.deltaTime);
                }
                else {
                    _lineOfSightCollider.enabled = false;
                    //coverDetction Scanned
                    _detectingCovers = false;
                    if (_covers.Count > 0) {
                        CreateMovablePoints();
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        { 
            if (collision.gameObject.CompareTag("ObstacleAndCover")) {
                if (!_covers.Contains(collision.transform))
                {
                    _covers.Add(collision.transform);
                }
            }
        }

        private void CreateMovablePoints() {
            try
            {
                _agent = this.GetComponent<NavMeshAgent>();
            }
            catch
            {
                Debug.Log("no agent on aicontroller please add agent");
            }

            if (_agent != null)
            {
                if (_covers.Count > 0)
                {
                    for (int j = 0; j < _covers.Count; j++)
                    {
                        for (int i = 0; i < circumferencePoints.Length; i++)
                        {

                            circumferencePoints[i] = new Vector2(_pathradius * Mathf.Cos(i) + _covers[j].position.x, _pathradius * Mathf.Sin(i) + _covers[j].position.y);
                            if (_agent.CalculatePath(circumferencePoints[i], path))
                            {
                                _movablePoints.Add(circumferencePoints[i]);
                            }
                        }
                    }
                    CreateColliderForRayCastDetection();
                };

            }
        }

        /*private void CreateColliderForRayCastDetection() {
            
            if (_movablePoints.Count > 0)
            {
                CircleCollider2D[] _raycastDetectionColliders = new CircleCollider2D[_movablePoints.Count];
                GameObject[] _raycastDetectionGameObjects = new GameObject[_movablePoints.Count];
                for (int i = 0; i < _movablePoints.Count; i++)
                {
                    _raycastDetectionGameObjects[i] = new GameObject();
                    _raycastDetectionGameObjects[i].layer= 6;
                    _raycastDetectionGameObjects[i].name = "gameobj +" + i;
                    _raycastDetectionGameObjects[i].transform.position = new Vector3(_movablePoints[i].x, _movablePoints[i].y, 0);
                    _raycastDetectionColliders[i] = _raycastDetectionGameObjects[i].AddComponent<CircleCollider2D>();
                    _raycastDetectionColliders[i].radius = _raycastColliderSize;
                }
                CreateRaycast(_raycastDetectionGameObjects);
            }
        }*/

        private void CreateColliderForRayCastDetection()
        {

            if (_movablePoints.Count > 0)
            {
                GameObject[] _raycastDetectionGameObjects = new GameObject[_movablePoints.Count];
                for (int i = 0; i < _movablePoints.Count; i++)
                {
                    _raycastDetectionGameObjects[i] = GameManager.Game.Pool.SpwanRayCastColliders(_movablePoints[i]);
                }
                CreateRaycast(_raycastDetectionGameObjects);
            }
        }

        private void CreateRaycast(GameObject[] _raycastDetectionGameObjects)
        {
            RaycastHit2D hit2D;
            Ray[] ray = new Ray[_movablePoints.Count];
            for (int i = 0; i < _movablePoints.Count; i++)
            {
                hit2D = Physics2D.Raycast(new Vector2(_enemy.position.x, _enemy.position.y)
                    , _movablePoints[i] - new Vector2(_enemy.position.x, _enemy.position.y), enemyLineOfSight, layerMask);

                if (hit2D.collider.gameObject == _raycastDetectionGameObjects[i])
                {

                    Debug.DrawRay(_enemy.position
                   , _movablePoints[i] - new Vector2(_enemy.position.x, _enemy.position.y), Color.green, enemyLineOfSight);
                    
                }
                else {
                    _hideablePoints.Add(_movablePoints[i]);

                }
            }

            _aicontroller.ActionHide(_hideablePoints);
            ClearData(_raycastDetectionGameObjects);
            //_movablePoints.RemoveAll(_NonHideablePoints);
        }

        private void ClearData(GameObject[] _raycastDetectionGameObjects) { // To Be Object Pooled

            _movablePoints.Clear();
            _hideablePoints.Clear();
            for (int i = 0; i < _raycastDetectionGameObjects.Length; i++)
            {
                // Destroy(_raycastDetectionGameObjects[i].gameObject);
                GameManager.Game.Pool.KillRaycastColliders(_raycastDetectionGameObjects[i].gameObject);
                //_raycastDetectionGameObjects = null;
            }
            _raycastDetectionGameObjects = null;
        }

        public List<Vector2> GetHidablelocations() {
            return _hideablePoints;
        }

        private void OnDrawGizmos()
        {
             
            if (_covers.Count > 0)
            {
                for (int j = 0; j < _covers.Count; j++)
                {
                    for (int i = 0; i < circumferencePoints.Length; i++)
                    {

                        circumferencePoints[i] = new Vector2(_pathradius * Mathf.Cos(i) + _covers[j].position.x, _pathradius * Mathf.Sin(i) + _covers[j].position.y);
                        if (_agent.CalculatePath(circumferencePoints[i], path))
                        {
                            //Gizmos.DrawWireSphere(circumferencePoints[i], 0.1f);
                            //Gizmos.DrawWireSphere(_hideablePoints[i], 0.1f);
                            //Gizmos.DrawLine(_enemy.position, circumferencePoints[i]);
                        }
                    }
                }
            }
            if (_movablePoints.Count > 0)
            {
                for (int i = 0; i < _movablePoints.Count; i++)
                {
                    Gizmos.DrawWireSphere(_movablePoints[i], 0.1f);
                }
                for (int i = 0; i < _hideablePoints.Count; i++)
                {
                    Gizmos.DrawWireCube(_hideablePoints[i], Vector3.one * 0.1f);
                }
            }
            
        }
    }
}
