using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Fishes
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class FishMover : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private readonly float _maxValue = 90;
        private readonly float _minValue = 200;
        private readonly float _height = 1.48f;
        private readonly int _boatPositionLift = 0;
        private readonly int _targetLift = 1;
       [SerializeField] private Vector3 _targetPosition;
       [SerializeField] private Vector3 _boatUpRightPosition = new Vector3(162, 1, 140);
       [SerializeField] private Vector3 _boatDownLeftPosition = new Vector3(130, 1, 168);

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            SetNewRandomDestination();
        }

        private void OnEnable()
        {
            SetNewRandomDestination();
        }

        private void Update()
        {
            if (HasReachedDestination() || IsInsideBoatArea())
            {
                SetNewRandomDestination();
            }
        }

        private void SetNewRandomDestination()
        {
            _targetPosition = new Vector3(Random.Range(_minValue, _maxValue), _height, Random.Range(_minValue, _maxValue));
            _agent.SetDestination(_targetPosition);
        }

        private bool HasReachedDestination()
        {
            float distanceX = Mathf.Abs(transform.position.x - _targetPosition.x);
            float distanceZ = Mathf.Abs(transform.position.z - _targetPosition.z);
            return distanceX < _targetLift && distanceZ < _targetLift;
        }

        private bool IsInsideBoatArea()
        {
            return _targetPosition.x - _boatDownLeftPosition.x > _boatPositionLift
                && _targetPosition.x - _boatUpRightPosition.x < _boatPositionLift
                && _targetPosition.z - _boatDownLeftPosition.z < _boatPositionLift
                && _targetPosition.z - _boatUpRightPosition.z > _boatPositionLift;
        }
    }
}