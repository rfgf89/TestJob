using System;
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
		
		private Vector3 _start = Vector3.zero;
		private Vector3 _launchVelocity = Vector3.zero;
		private float _gravity;
		private bool isDestroy;
		private float destroyTime;
		private float timeFly;
		private bool isLaunch;
		private GameLoop _gameLoop;
		
		public override float Speed => _speed;
		
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
			if (isLaunch)
			{
				Vector3 p = _start + _launchVelocity * timeFly;
				p.y -= 0.5f * _gravity * timeFly * timeFly;
				transform.localPosition = p;
				
				if (destroyTime <= 0f)
				{
					_gameLoop.Remove(this);
					Destroy(gameObject);
				}
				else
					destroyTime -= deltaTime;

				timeFly += deltaTime * _speed;
			}

		}
		public override void Launch(GameObject target, Vector3 velocity, float gravity, Vector3 start)
		{
			_launchVelocity = velocity;
			_gravity = gravity;
			_start = start;
			isLaunch = true;
		}
		

		private void OnTriggerEnter(Collider other)
		{
			if (!isDestroy)
			{
				var dam = other.gameObject.GetComponent<IDamagable>();
				dam?.Damage(_damage);
				
				destroyTime = _timeLifeInMonster;
				isDestroy = true;
			}
		}


	}
	
}