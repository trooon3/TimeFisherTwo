using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FieldOfView))]
public class FishCatcher : MonoBehaviour
{
    [SerializeField] private PlayerAnimationController _player;

    [Range(0, 360)]
    [SerializeField] private float _minAngle;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _radius;

    [SerializeField] public bool IsCanCatchFish;

    private float _angle = 100;
    private float _elapsedTime = 0;
    private Bag _bag;
    private Fish _fishToCatch;

    private FieldOfView _fieldOfView;
    
    private int FishNumber = 9;
    private int Fish;
    public float ElapsedTime => _elapsedTime;
    public UnityAction ElapsedTimeChanged;

    private void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        Mathf.Clamp(_angle, _minAngle, _maxAngle);
        _bag = GetComponent<Bag>();
        Fish = 1 << FishNumber;
    }

    private void Update()
    {
        //FieldOfViewCheck();
        _fishToCatch = _fieldOfView.FishToCatch;
        TryFindFish();
    }

    private void TryAddFish(Fish fish)
    {
        _bag.TryAddFish(fish);
        fish.SetOffFish();
    }

    private void TryFindFish()
    {
        if (_fishToCatch != null)
        {
            TryCatchFish();
        }
    }

    private void TryCatchFish()
    {
        if (CheckElapsedTime(_fishToCatch))
        {
            TryAddFish(_fishToCatch);
            IsCanCatchFish = false;
            _elapsedTime = 0;
            ElapsedTimeChanged?.Invoke();

            _fishToCatch = null;
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
