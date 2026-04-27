using Assets.Scripts.BagScripts;
using Assets.Scripts.Fishes;
using Assets.Scripts.Saves;
using UnityEngine;
using YG;

namespace Assets.Scripts.SkinScripts
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private ChestInteractionController _interaction;
        [SerializeField] private ChestAdManager _adManager;
        [SerializeField] private ChestTutorialManager _tutorialManager;
        [SerializeField] private FishProcessor _fishProcessor;
        [SerializeField] private Bag _bag;

        private void Start()
        {
            YandexGame.savesData.LoadTutorial(TutorialsKeys.IsShowTutorialRod);

            _interaction.CloseChest();
        }

        public void OnChestButtonClick()
        {
            if (!_interaction.IsPlayerNearby) return;

            _adManager.ShowAd();
            _interaction.OpenChest();
            _tutorialManager.HandleChestInteraction();

            var fishes = _bag.GetFish();
            if (fishes.Count > 0)
            {
                _fishProcessor.ProcessFishes(fishes);
            }
        }
    }
}