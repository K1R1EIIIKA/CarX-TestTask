using MonstersLogic;
using ProjectileLogic;
using UnityEngine;

namespace Infrastructure.Asset
{
    public class AssetDatabase : IAssetDatabase
    {
        public Monster MonsterPrefab => GetAsset<Monster>("Monster").GetComponent<Monster>();
        public SimpleProjectile SimpleProjectilePrefab => GetAsset<SimpleProjectile>("Projectiles/SimpleProjectile");
        public CannonProjectile CannonProjectilePrefab => GetAsset<CannonProjectile>("Projectiles/CannonProjectile");
        public MortarProjectile MortarProjectilePrefab => GetAsset<MortarProjectile>("Projectiles/GuidedProjectile");

        private T GetAsset<T>(string assetName) where T : Object
        {
            var asset = Resources.Load<T>(assetName);
            if (asset == null)
                Debug.LogError($"Asset with name {assetName} not found in Resources folder.");

            return asset;
        }
    }
}