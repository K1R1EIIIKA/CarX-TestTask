using Infrastructure.Configs;
using UnityEngine;

namespace Infrastructure.Asset
{
    public class AssetDatabase : IAssetDatabase
    {
        public MonsterConfig MonsterConfig => GetAsset<MonsterConfig>("MonsterConfig");
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