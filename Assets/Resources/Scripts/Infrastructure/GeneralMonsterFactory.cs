using Game;
using Game.Monsters;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralMonsterFactory : GameLoopFactory, IMonsterFactory
    {
        [SerializeField] private GameBehaviour _default;

        public void Init(GameLoop gameLoop)
        {
            base.Init(gameLoop);
        }

        public Monster Create(MonsterType monsterType, Transform target, Vector3 at)
        {
            switch (monsterType)
            {
                case MonsterType.Default:
                    return Instance(_default, at).GetComponentInChildren<Monster>().SetTarget(target);
            }

            Debug.LogError($"Not implementation for {monsterType} from GeneralMonsterFactory");
            return null;
        }
    }
}