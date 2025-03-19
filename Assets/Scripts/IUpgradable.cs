using Assets.Scripts.FishResources;

namespace Assets.Scripts
{
    public interface IUpgradable
    {
        public void Upgrade();
        public Resource GetResourceToUpgrade();
        public int GetResourceCountToUpgrade();
    }
}

