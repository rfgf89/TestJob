using Game;
using UnityEngine;

namespace Towers.Projectiles
{
	public class CannonProjectile : Projectile, IGameUpdate
	{
		[SerializeField] private float _speed = 0.2f;
		[SerializeField] private int _damage = 10;
		[SerializeField] private float _timeLifeInMonster;
		[SerializeField] private float _timeLife;
		
		private Vector3 _target;
		private Vector3 _start;
		private Vector3 _launchVelocity;
		private float _gravity;
		private bool isDestroy;
		private float destroyTime;
		private float timeFly;
		private GameLoop _gameLoop;
		
		public CannonProjectile Init(GameLoop gameLoop)
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
			//transform.position = Vector3.MoveTowards(transform.position, _target, _speed * deltaTime);

			timeFly += Time.deltaTime * _speed;
			Vector3 p = _start + _launchVelocity * timeFly;
			p.y -= 0.5f * 6.81f * timeFly * timeFly;
			transform.localPosition = p;
			
			if (destroyTime <= 0f)
			{
				_gameLoop.Remove(this);
				Destroy(gameObject);
			}
			else
			{
				destroyTime -= deltaTime;
			}
			
		}
		public override void Launch(GameObject target, Vector3 velocity, float gravity, Vector3 start)
		{
			_launchVelocity = velocity;
			_gravity = gravity;
			_start = start;
			_target = target.transform.position;
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