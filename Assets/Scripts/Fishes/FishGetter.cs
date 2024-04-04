using System.Collections.Generic;
using UnityEngine;

public class FishGetter : MonoBehaviour
{
    private List<SeaCreature> _seaCreatures;

    private void Awake()
    {
        _seaCreatures = new List<SeaCreature> { };
    }

    public Fish CreateFish(SeaCreature seaCreature)
    {
        Fish fish = new Fish();
        fish.Init(seaCreature);
        return fish;
    }

    public Fish GetFishOnHook(SeaCreature seaCreature)
    {
        Fish fish = new Fish();
        fish.Init(seaCreature.FoodFor);
        return fish;
    }

    public List<Fish> GetAllLegendaryFishes()
    {
        List<Fish> fishes = new List<Fish>();

        for (int i = 0; i < 3; i++)
        {
            Fish legendaryFish = new Fish();
            //legendaryFish.Init(_legendaryFishes[i], 5);
            fishes.Add(legendaryFish);
        }
        
        return fishes;
    }

}
