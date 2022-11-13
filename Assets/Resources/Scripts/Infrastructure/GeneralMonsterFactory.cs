using Game;
using Game.Config;
using Game.Monsters;
using Unity.Mathematics;
using UnityEngine;

namespace Infrastructure
{
    public class GeneralMonsterFactory
    {
        private GeneralMonsterScriptableObject _config;
        private GameLoop _gameLoop;
        
        public GeneralMonsterFactory(GeneralMonsterScriptableObject config, GameLoop gameLoop)
        {
            _config = config;
            _gameLoop = gameLoop;
        }
        
        public Monster Create(MonsterType monsterType, Transform target, Vector3 at)
        {
            switch (monsterType)
            {
                case MonsterType.Monster: return InstanceMonster(_config.monster, at).SetTarget(target);
            }

            Debug.LogError($"Not implementation for {monsterType} from General Monster Factory");
            return null;
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