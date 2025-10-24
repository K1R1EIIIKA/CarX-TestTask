using Infrastructure.DIContainer;
using MonstersLogic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IMonsterFactory : IService
    {
        Monster CreateMonster(Vector3 position, Quaternion rotation);
    }
}