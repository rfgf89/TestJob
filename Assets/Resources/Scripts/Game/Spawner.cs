using Game.Monsters;
using Infrastructure;
using UnityEngine;

namespace Game
{
	public class Spawner : GameBehaviour
	{
		[SerializeField] private float _interval = 3;
		[SerializeField] private GameObject _moveTarget;
		[SerializeField] private MonsterType _monsterType;

		private GeneralMonsterFactory _monsterFactory;
		private float _lastSpawn = -1;

		public Spawner Init(GeneralMonsterFactory monsterFactory)
		{
			_monsterFactory = monsterFactory;
			return this;
		}

		public override void GameUpdate(float deltaTime, float time)
		{
			if (time > _lastSpawn + _interval)
			{

				_monsterFactory.Create(_monsterType, _moveTarget.transform, transform.position);

				_lastSpawn = time;
			}
		}
	}
}