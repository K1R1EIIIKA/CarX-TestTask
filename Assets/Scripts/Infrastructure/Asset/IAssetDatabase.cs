using Infrastructure.Configs;
using Infrastructure.DIContainer;
using MonstersLogic;
using ProjectileLogic;
using UnityEngine;

namespace Infrastructure.Asset
{
    public interface IAssetDatabase : IService
    {
        Monster MonsterPrefab { get; }
        SimpleProjectileConfig SimpleProjectileConfig { get; }
        CannonProjectileConfig CannonProjectileConfig { get; }
        MortarProjectileConfig MortarProjectileConfig { get; }
    }
}