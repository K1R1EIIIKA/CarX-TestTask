using Infrastructure.Configs;
using MonstersLogic;
using ProjectileLogic;
using UnityEngine;

namespace Infrastructure.Asset
{
    public class AssetDatabase : IAssetDatabase
    {
        public Monster MonsterPrefab => GetAsset<Monster>("Monster").GetComponent<Monster>();
        public SimpleProjectileConfig SimpleProjectileConfig => GetAsset<SimpleProjectileConfig>("Projectiles/SimpleProjectileConfig");
        public CannonProjectileConfig CannonProjectileConfig => GetAsset<CannonProjectileConfig>("Projectiles/CannonProjectileConfig");
        public MortarProjectileConfig MortarProjectileConfig => GetAsset<MortarProjectileConfig>("Projectiles/MortarProjectileConfig");

        private T GetAsset<T>(string assetName) where T : Object
        {
            var asset = Resources.Load<T>(assetName);
            if (asset == null)
                Debug.LogError($"Asset with name {assetName} not found in Resources folder.");

            return asset;
        }
    }
}