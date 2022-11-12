using Game.Monsters;
using UnityEngine;

namespace Infrastructure
{
    public interface IMonsterFactory
    {
        Monster Create(MonsterType monsterType, Transform target, Vector3 at);
    }
}