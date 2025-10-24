using Infrastructure.DIContainer;
using MonstersLogic;
using ProjectileLogic;
using UnityEngine;

namespace Infrastructure.Asset
{
    public interface IAssetDatabase : IService
    {
        Monster MonsterPrefab { get; }
        SimpleProjectile SimpleProjectilePrefab { get; }
        CannonProjectile CannonProjectilePrefab { get; }
        MortarProjectile MortarProjectilePrefab { get; }
    }
}