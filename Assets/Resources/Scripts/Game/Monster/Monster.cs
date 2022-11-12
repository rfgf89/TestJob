using Towers;
using Unity.Mathematics;
using UnityEngine;

namespace Game.Monsters
{
	[RequireComponent(typeof(Collider))]
	public class Monster : GameBehaviour, ITowerTarget, IDamagable, IDestroyableInEndPoint
	{
		public float MaxBoundSize { get; private set; }
		public Vector3 Velocity { get; private set; }
		public float Speed => _speed;

		[SerializeField] private float _maxHp;
		[SerializeField] private float _speed;
		private float _hp;
		private Transform _target;

		void Start()
		{
			var coll = GetComponent<Collider>();
			MaxBoundSize = math.max(coll.bounds.size.x, coll.bounds.size.z);

			_hp = _maxHp;
		}

		public override void GameUpdate(float deltaTime, float time)
		{
			if (_target == null)
				return;

			Velocity = (_target.transform.position - transform.position).normalized * _speed * deltaTime;
			transform.Translate(Velocity);
		}

		public void Damage(float damage)
		{
			_hp -= damage;
			if (_hp <= 0f)
				Destroy(gameObject);
		}

		public Monster SetTarget(Transform target)
		{
			_target = target;
			return this;
		}
	}
}