using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _transforms;
    [SerializeField] private List<Fish> _prefabs;
    [SerializeField] private int _maxFishCount;
    [SerializeField] private Closet _closet;
    private ObjectPool<Fish> _pool;

    private void Start()
    {
        _pool = new ObjectPool<Fish>(() =>
        { return Instantiate(_prefabs[Random.Range(0, _prefabs.Count)]); },
        fish => { fish.gameObject.SetActive(true); },
        fish => { fish.gameObject.SetActive(false); },
        fish => { Destroy(fish.gameObject); },
        false, 30, 50);

        for (int i = 0; i < _maxFishCount; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        var fish = _pool.Get();
        fish.transform.position = _transforms[Random.Range(0, _transforms.Count)].position;
    }

    public void SetOffFish(Fish fish)
    {
        _pool.Release(fish);
        Spawn();
    }
}
