using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _transforms;
    [SerializeField] private List<Fish> _prefabs;
    [SerializeField] private int _maxFishCount;
    [SerializeField] private Closet _closet;

    private void Start()
    {
        for (int i = 0; i < _maxFishCount; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Fish fish = Instantiate(_prefabs[Random.Range(0, _prefabs.Count)], _transforms[Random.Range(0, _transforms.Count)]);
    }

    private void OnFishTransferred(Fish fish)
    {
        fish.transform.position = _transforms[Random.Range(0, _transforms.Count)].position;
        fish.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _closet.FishTransferred += OnFishTransferred;
    }

    private void OnDisable()
    {
       _closet.FishTransferred -= OnFishTransferred;
    }
}
