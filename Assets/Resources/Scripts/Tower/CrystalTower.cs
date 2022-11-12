using Infrastructure;
using Towers.Enums;
using UnityEngine;

namespace Towers
{
	public class CrystalTower : Tower
	{
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private ProjectileType _projectileType;
		[SerializeField] private Transform _shotPos;

		[SerializeField] private float _radius = 4f;
		[SerializeField] private float _shotInterval = 0.5f;

		private float _lastShotTime = -0.5f;
		private IProjectileFactory _projectileFactory;

		public override Tower Init(IProjectileFactory projectileFactory)
		{
			_projectileFactory = projectileFactory;

			return this;
		}

		public override void GameUpdate(float deltaTime, float time)
		{
			if (_lastShotTime + _shotInterval < time && TryGetTarget(out var target, _radius, _layerMask))
			{
				_projectileFactory
					.Create(_projectileType, _shotPos.position)
					.SetTarget(target.gameObject);

				_lastShotTime = time;
			}
		}
	}
}