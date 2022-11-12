using Game;
using UnityEngine;

namespace Towers.Projectiles
{
	public class GuidedProjectile : Projectile
	{
		[SerializeField] private float _speed = 0.2f;
		[SerializeField] private int _damage = 10;
		[SerializeField] private float _timeLifeInMonster;
		[SerializeField] private float _timeLife;

		private GameObject _target;
		private bool isDestroy;
		private float destroyTime;

		private void Start()
		{
			destroyTime = _timeLife;
		}

		public override void GameUpdate(float deltaTime, float time)
		{
			if (_target != null)
			{
				transform.position =
					Vector3.MoveTowards(transform.position, _target.transform.position, _speed * deltaTime);

				if (destroyTime <= 0f)
					Destroy(gameObject);
				else
					destroyTime -= deltaTime;
			}
			else
				Destroy(gameObject);
		}

		public override void SetTarget(GameObject target)
		{
			_target = target;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!isDestroy)
			{
				var dam = other.gameObject.GetComponent<IDamagable>();

				if (dam == null)
					return;

				dam.Damage(_damage);
				destroyTime = _timeLifeInMonster;
				isDestroy = true;
			}
		}
	}
}