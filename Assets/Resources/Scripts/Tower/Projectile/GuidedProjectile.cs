using Game;
using UnityEngine;

namespace Towers.Projectiles
{
	public class GuidedProjectile : Projectile, IGameUpdate
	{
		[SerializeField] private float _speed = 0.2f;
		[SerializeField] private int _damage = 10;
		[SerializeField] private float _timeLifeInMonster;
		[SerializeField] private float _timeLife;

		private GameObject _target;
		private bool isDestroy;
		private float destroyTime;
		private GameLoop _gameLoop;
		
		public GuidedProjectile Init(GameLoop gameLoop)
		{
			_gameLoop = gameLoop;
			return this;
		}
		
		private void Start()
		{
			destroyTime = _timeLife;
		}

		public void GameUpdate(float deltaTime, float time)
		{
			if (_target != null)
			{
				transform.position =
					Vector3.MoveTowards(transform.position, _target.transform.position, _speed * deltaTime);

				if (destroyTime <= 0f)
				{
					_gameLoop.Remove(this);
					Destroy(gameObject);
				}
				else
					destroyTime -= deltaTime;
			}
			else
			{
				_gameLoop.Remove(this);
				Destroy(gameObject);
			}
		}

		public override void Launch(GameObject target, Vector3 velocity, float gravity, Vector3 start)
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