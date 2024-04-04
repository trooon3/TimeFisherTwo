using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FishMover : MonoBehaviour
{
    private NavMeshAgent _agent;
    private float _maxValue = 250;
    private float _minValue = 50;
    private Vector3 _targetPosition;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _targetPosition = new Vector3(Random.Range(_minValue, _maxValue), 1.48f, Random.Range(_minValue, _maxValue));
        _agent.SetDestination(_targetPosition);
    }

    private void Update()
    {
        if (transform.position.x == _targetPosition.x && transform.position.z == _targetPosition.z)
        {
            _targetPosition = new Vector3(Random.Range(_minValue, _maxValue), 1.48f, Random.Range(_minValue, _maxValue));
            _agent.SetDestination(_targetPosition);
        }
    }
}
