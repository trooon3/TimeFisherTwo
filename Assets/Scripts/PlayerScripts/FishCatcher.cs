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
    [SerializeField] private bool _isCanCatchFish;

    private float _angle = 100;
    private float _elapsedTime = 0;
    private Bag _bag;
    private Fish _fishToCatch;
    private Coroutine _coroutine;

    private FieldOfView _fieldOfView;
    public FieldOfView FieldOfView => _fieldOfView;
    public Fish FishToCatch => _fishToCatch;
    
    private int FishNumber = 9;
    private int Fish;
    public float ElapsedTime => _elapsedTime;
    
    public UnityAction Catched;

    private void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        Mathf.Clamp(_angle, _minAngle, _maxAngle);
        _bag = GetComponent<Bag>();
        Fish = 1 << FishNumber;
    }

    public void SetCatchFish(Fish fish)
    {
        Debug.Log("устанавливаем рыбу");
        _isCanCatchFish = true;
        _fishToCatch = fish;
    }

    public void ResetSettings()
    {
        _isCanCatchFish = false;
        _fishToCatch = null;
        _elapsedTime = 0;
    }

    public void TryFindFish()
    {
        Debug.Log("защи в поиск рыбы");
        if (_fishToCatch != null)
        {
        Debug.Log("рыба не нулл");
            _fishToCatch.SetCatcher(this);
            TryCatchFish();
        }
        else
        {
            if (_elapsedTime != 0)
            {
                _elapsedTime = 0;
            }
        }
    }

    private void TryCatchFish()
    {
        Debug.Log("зашли в трай кетч фиш");
        StartElapseTime();
    }

    private void TryAddFish(Fish fish)
    {
        if (_bag.TryAddFish(fish))
        {
            Catched?.Invoke();
            _spawner.SetOffFish(fish);
        }
    }

    //private bool CheckElapsedTime(Fish fish)
    //{
    //    if (fish == null)
    //    {
    //        
    //        return false;
    //    }

    //    if (_elapsedTime >= fish.Catchtime)
    //    {
    //        return true;
    //    }

    //    _elapsedTime += Time.deltaTime;
    //    return false;
    //}

    private void StartElapseTime()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        if (_fishToCatch != null)
        {
            Debug.Log("рыба не нулл");
            _coroutine = StartCoroutine(ElapseTime());
        }
    }

    private IEnumerator ElapseTime()
    {
        Debug.Log($"зашли в корутину проверки времени");

        while (_isCanCatchFish)
        {
            if (_elapsedTime >= _fishToCatch.Catchtime)
            {
                _isCanCatchFish = false;
                _elapsedTime = 0;
                TryAddFish(_fishToCatch);
                _fishToCatch = null;
            }

            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        
    }

}
