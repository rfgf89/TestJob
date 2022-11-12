using Game.Monsters;
using Infrastructure;
using UnityEngine;

namespace Game
{
	public class Spawner : MonoBehaviour, IGameUpdate
	{
		[SerializeField] private float _interval = 3;
		[SerializeField] private GameObject _moveTarget;
		[SerializeField] private MonsterType _monsterType;

		private GeneralMonsterFactory _monsterFactory;
		private float _lastSpawn = -1;

		public void Init(GeneralMonsterFactory monsterFactory)
		{
			_monsterFactory = monsterFactory;
		}

		public  void GameUpdate(float deltaTime, float time)
		{
			if (time > _lastSpawn + _interval)
			{

				_monsterFactory.Create(_monsterType, _moveTarget.transform, transform.position);

				_lastSpawn = time;
			}
		}
	}
}