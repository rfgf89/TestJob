using Game;
using Game.Config;
using Game.Monsters;
using Unity.Mathematics;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralMonsterFactory
    {
        private readonly GeneralMonsterScriptableObject _config;
        private readonly GameLoop _gameLoop;
        
        public GeneralMonsterFactory(GeneralMonsterScriptableObject config, GameLoop gameLoop)
        {
            _config = config;
            _gameLoop = gameLoop;
        }
        
        public void Create(MonsterType monsterType, Transform target, Vector3 at)
        {
            switch (monsterType)
            {
                case MonsterType.Monster: InstanceMonster(_config.monster, at).SetTarget(target); return;
            }

            Debug.LogError($"Not implementation for {monsterType} from General Monster Factory");
        }

        private Monster InstanceMonster(Monster monsterPrefab, Vector3 at)
        {
            var monster = GameObject.Instantiate(monsterPrefab, _config.monster.transform.localPosition + at, Quaternion.identity, null);
            _gameLoop.Add(monster);
            monster.Init(_gameLoop);
            return monster;
        }
    }
}