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
    [SerializeField] private FishSpawner _spawner;
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
    public UnityAction Catched;

    private void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        Mathf.Clamp(_angle, _minAngle, _maxAngle);
        _bag = GetComponent<Bag>();
        Fish = 1 << FishNumber;
    }

    private void Update()
    {
        _fishToCatch = _fieldOfView.FishToCatch;
        TryFindFish();
    }

    private void TryAddFish(Fish fish)
    {
        if (_bag.TryAddFish(fish))
        {
            Catched?.Invoke();
            _spawner.SetOffFish(fish);
        }
    }

    private void TryFindFish()
    {
        if (_fishToCatch != null)
        {
            TryCatchFish();
        }
        else
        {
            _elapsedTime = 0;
            ElapsedTimeChanged?.Invoke();
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
        fish.SetCatcher(this);

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

}
