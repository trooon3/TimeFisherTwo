using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Fishes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FishMover : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private float _maxValue = 90;
        private float _minValue = 200;
        private Vector3 _targetPosition;
        private Vector3 _boatUpRightPosition = new Vector3(162, 1, 140);
        private Vector3 _boatDownLeftPosition = new Vector3(130, 1, 168);

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _targetPosition = new Vector3(Random.Range(_minValue, _maxValue), 1.55f, Random.Range(_minValue, _maxValue));
            _agent.SetDestination(_targetPosition);
        }

        private void OnEnable()
        {
            _targetPosition = new Vector3(Random.Range(_minValue, _maxValue), 1.55f, Random.Range(_minValue, _maxValue));
            _agent.SetDestination(_targetPosition);
        }

        private void Update()
        {
            if (transform.position.x == _targetPosition.x && transform.position.z == _targetPosition.z ||
                _targetPosition.x >= _boatDownLeftPosition.x && _targetPosition.x <= _boatUpRightPosition.x && _targetPosition.z <= _boatDownLeftPosition.z && _targetPosition.z >= _boatUpRightPosition.z)
            {
                _targetPosition = new Vector3(Random.Range(_minValue, _maxValue), 1.48f, Random.Range(_minValue, _maxValue));
                _agent.SetDestination(_targetPosition);
            }
        }
    }
}

