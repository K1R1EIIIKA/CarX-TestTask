using CanonLogic;
using Infrastructure.Configs;
using UnityEngine;

namespace Infrastructure.Asset
{
    public class AssetDatabase : IAssetDatabase
    {
        public MonsterConfig MonsterConfig => GetAsset<MonsterConfig>("MonsterConfig");
        private SimpleProjectileConfig SimpleProjectileConfig => GetAsset<SimpleProjectileConfig>("Projectiles/SimpleProjectileConfig");
        private CannonProjectileConfig CannonProjectileConfig => GetAsset<CannonProjectileConfig>("Projectiles/CannonProjectileConfig");
        private MortarProjectileConfig MortarProjectileConfig => GetAsset<MortarProjectileConfig>("Projectiles/MortarProjectileConfig");

        private T GetAsset<T>(string assetName) where T : Object
        {
            var asset = Resources.Load<T>(assetName);
            if (asset == null)
                Debug.LogError($"Asset with name {assetName} not found in Resources folder.");

            return asset;
        }
        
        public ProjectileConfig ProjectileConfig(ProjectileType type)
        {
            return type switch
            {
                ProjectileType.Simple => SimpleProjectileConfig,
                ProjectileType.Cannon => CannonProjectileConfig,
                ProjectileType.Mortar => MortarProjectileConfig,
                _ => null
            };
        }
    }
}