using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishCatcher : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController _player;
    [SerializeField] private float _radius;
    [SerializeField] private float _maxDistance;
    [Range(0, 360)]
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;

    [SerializeField] public bool IsCanCatchFish;
    private float _angle = 100;
    private float _elapsedTime = 0;
    private Bag _bag;
    private Fish _fishToCatch;

    private int FishNumber = 9;
    private int Fish;
    public float ElapsedTime => _elapsedTime;
    public UnityAction ElapsedTimeChanged;

    private void Start()
    {
        Mathf.Clamp(_angle, _minAngle, _maxAngle);
        _bag = GetComponent<Bag>();
        Fish = 1 << FishNumber;
    }

    private void Update()
    {
        //FieldOfViewCheck();
        IsCanCatchFish = Physics.CheckSphere(transform.position, _radius, Fish);
        TryFindFish();
        //_fishes.AddRange(Physics.SphereCastAll(transform.position, _radius, Vector3.forward, _maxDistance, Fish));
    }

    private void TryAddFish(Fish fish)
    {
        _bag.TryAddFish(fish);
        fish.SetOffFish();
    }

    private void TryFindFish()
    {
        if (IsCanCatchFish)
        {
            TryCatchFish();
        }
    }

    private void TryCatchFish()
    {
        if (Physics.SphereCast(transform.position, _radius, Vector3.forward, out RaycastHit hit))
        {
            Debug.Log("Reayycast попал в рыбу");
            if (Vector3.Angle(transform.forward, Vector3.forward) < _angle)
            {
                Debug.Log("Рыба встала в поле");
                _fishToCatch = hit.collider.gameObject.GetComponent<Fish>();

                if (CheckElapsedTime(_fishToCatch))
                {
                    TryAddFish(_fishToCatch);
                    IsCanCatchFish = false;
                    _elapsedTime = 0;
                    ElapsedTimeChanged?.Invoke();

                    _fishToCatch = null;
                }
            }
        }
    }

    private bool CheckElapsedTime(Fish fish)
    {
        if (fish == null)
        {
            return false;
        }

        if (_elapsedTime >= fish.Catchtime)
        {
            return true;
        }

        _elapsedTime += Time.deltaTime;
        ElapsedTimeChanged?.Invoke();
        Debug.Log("прошло времени " + _elapsedTime);
        return false;
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, Fish);

        if (rangeChecks.Length != 0)
        {

            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle)
            {
                IsCanCatchFish = true;
                Debug.Log("Вижу рыбу");

                _elapsedTime += Time.deltaTime;
                ElapsedTimeChanged?.Invoke();
                Debug.Log("прошло времени " + _elapsedTime);

                if (IsCanCatchFish)
                {
                    if (_elapsedTime >= target.gameObject.GetComponent<Fish>().Catchtime)
                    {
                        TryAddFish(target.gameObject.GetComponent<Fish>());
                        IsCanCatchFish = false;
                        _elapsedTime = 0;
                        ElapsedTimeChanged?.Invoke();
                    }
                }
            }
            else
            {
                IsCanCatchFish = false;
                _elapsedTime = 0;
                ElapsedTimeChanged?.Invoke();
            }
        }
        else if (IsCanCatchFish)
        {
            IsCanCatchFish = false;
            _elapsedTime = 0;
            ElapsedTimeChanged?.Invoke();
            Debug.Log("Потерял рыбу");
        }
    }
}
