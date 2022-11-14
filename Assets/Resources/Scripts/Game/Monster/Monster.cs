using Towers;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Monsters
{
	[RequireComponent(typeof(Collider))]
	public class Monster : MonoBehaviour, IGameUpdate, ITowerTarget, IDamagable, IDestroyableInEndPoint
	{
		public float MaxBoundSize { get; private set; }
		public Vector3 Velocity { get; private set; }
		
		[SerializeField] private float _maxHp;
		[SerializeField] private float _speed;
		private float _hp;
		private Transform _target;
		private GameLoop _gameLoop;
		
		public void Init(GameLoop gameLoop)
		{
			_gameLoop = gameLoop;
		}
		
		void Start()
		{
			var coll = GetComponent<Collider>();
			MaxBoundSize = math.max(coll.bounds.size.x, coll.bounds.size.z);

			_hp = _maxHp;
		}

		public void GameUpdate(float deltaTime, float time)
		{
			if (_target == null)
				return;

			Velocity = (_target.transform.position - transform.position).normalized * _speed;
			transform.Translate(Velocity * deltaTime);
		}

		public void Damage(float damage)
		{
			_hp -= damage;
			if (_hp <= 0f)
			{
				_gameLoop.Remove(this);
				Destroy(gameObject);
			}
		}

		public void SetTarget(Transform target)
		{
			_target = target;
		}
	}
}