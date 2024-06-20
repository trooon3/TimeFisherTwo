using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private Rod _rod;
    [SerializeField] private Bag _bag;
    [SerializeField] private Closet _closet;

    public void IncreasePlayerSpeed()
    {
        _mover.SetActiveIncrease();
    }

    public void DecreaseCatchTime()
    {
        _rod.SetActiveIncrease();
    }

    public void IncreaceCountCatchedFish()
    {
        _bag.SetActiveIncrease();
    }

    public void IncreaceCountResources()
    {
        _closet.SetActiveIncrease();
    }
}
