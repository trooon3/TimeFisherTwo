using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationController))]
public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _angle;
    [SerializeField] private LayerMask _targetMask;

    private PlayerAnimationController _playerRef;

    private Fish _fish;
    private bool _canSeePlayer;

    public float Radius => _radius;

    public Fish FishToCatch => _fish;
    public float Angle => _angle;
    public bool CanSeePlayer => _canSeePlayer;
    public PlayerAnimationController PlayerRef => _playerRef;

    private void Start()
    {
        _playerRef = GetComponent<PlayerAnimationController>();
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            if (rangeChecks[0].TryGetComponent(out Fish fish))
            {
                Vector3 directionToTarget = (fish.transform.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) <= _angle)
                {
                    _fish = fish;
                }
                else
                {
                    _fish = null;
                    _canSeePlayer = false;
                }
            }
        }
        else
        {
            _fish = null;
        } 
    }
}
