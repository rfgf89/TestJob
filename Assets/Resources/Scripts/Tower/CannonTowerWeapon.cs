using System;
using System.Collections.Generic;
using Game;
using Infrastructure;
using TMPro;
using Towers.Enums;
using Towers.Projectiles;
using Unity.Mathematics;
using UnityEngine;

namespace Towers
{
	public class CannonTowerWeapon : TowerWeapon, IGameUpdate
	{
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private Transform _hub;
		[SerializeField] private Transform _barrel;

		[SerializeField] private ProjectileType _projectileType;
		[SerializeField] private CannonMode _cannonMode;
		[SerializeField] private float _shootInterval = 0.5f;
		[SerializeField, Range(0.1f, 9.8f)] private float _gravity = 9.80f;
		[SerializeField] private float _speed;
		[SerializeField] private float _angleSpeed;
		[SerializeField] private float _angleRange;
		[SerializeField] private float _attackRadius;
		[SerializeField] private LayerMask _layerMask;

		private Vector3 angleVelocity;
		private ITowerTarget _curTarget;
		private IProjectileFactory _projectileFactory;
		private float _lastShotTime = -0.5f;
		private Quaternion _angleToMonster;
		private enum CannonMode
		{
			Standart,
			Ballistics 
		}
		//+ target.Velocity.normalized / (_speed / 2) * target.Speed
		public void Init(IProjectileFactory projectileFactory)
		{
			_projectileFactory = projectileFactory;
		}

		public void GameUpdate(float deltaTime, float time)
		{
			if (TryGetTarget(out _curTarget, _attackRadius, _layerMask) && TryShot(_hub, _curTarget, time))
				ShootBallistics(_curTarget, time);
			
		}

		private Vector3 dd;
		private void ShootBallistics(ITowerTarget target, float time)
		{
			var targetPos = (target.transform.position - _shootPoint.position) + target.Velocity.normalized / (_speed / 2) * target.Speed;
			var bas = LaunchBallistics(_barrel, Vector3.zero, targetPos, 2, false);
			
			dd =_shootPoint.position + bas;
			LaunchBallistics(_barrel, _shootPoint.position, dd, 0f, true);
			
			_lastShotTime = time;
		}
		
		private void Shoot(ITowerTarget target, float time)
		{
			var targetPos = (target.transform.position - _shootPoint.position) + target.Velocity.normalized / (_speed / 2) * target.Speed;
			var bas = LaunchBallistics(_barrel, Vector3.zero, targetPos, 2, false);
			
			dd =_shootPoint.position + bas;
			LaunchBallistics(_barrel, _shootPoint.position, dd, 0f, true);
			
			_lastShotTime = time;
		}
		
		private bool TryShot(Transform hub, ITowerTarget target, float time)
		{
			Vector3 angle = hub.eulerAngles;
			_angleToMonster = Quaternion.LookRotation(
				(target.transform.position)-_shootPoint.position + target.Velocity.normalized / (_speed / 2) * target.Speed);
			angle.y = Mathf.SmoothDampAngle(hub.eulerAngles.y, _angleToMonster.eulerAngles.y, ref angleVelocity.y, _angleSpeed);
			hub.eulerAngles = angle; 
			
			return _lastShotTime + _shootInterval < time
			       && _angleToMonster.eulerAngles.y > angle.y - _angleRange 
			       && _angleToMonster.eulerAngles.y < angle.y + _angleRange 
			       && math.distance(transform.position, target.transform.position) < _attackRadius - target.MaxBoundSize;
		}
		
		
		private Vector3 Launch(Transform barrel, Vector3 pos, Vector3 endPos, float time)
		{
			var newPos = pos + endPos * time;
			
			var projectile = _projectileFactory.Create(_projectileType, pos);
			projectile.Launch(_curTarget.transform.gameObject,barrel.forward, 0f, pos);
			
			Vector3 an = Quaternion.LookRotation(endPos) * Vector3.up;
			barrel.eulerAngles = new Vector3(an.x, barrel.eulerAngles.y, barrel.eulerAngles.z);

			return newPos;
		}
		
		private Vector3 LaunchBallistics(Transform barrel, Vector3 pos, Vector3 endPos, float time, bool launch)
		{
			float3 dir = float3.zero;
			dir.x = endPos.z - pos.z;
			dir.y = endPos.y - pos.y;
			dir.z = endPos.x - pos.x;
		
			var x = math.length(dir.xyz);
			var y = 0;
			dir /= x;
		
			var g = _gravity;
			float x1 = x + 0.25f;
			var s = Mathf.Sqrt((y + Mathf.Sqrt(x1 * x1 + y * y)) * 9.81f);
			var s2 = s * s;
		
			var r = s2 * s2 - g * (g * x * x + 2f * y * s2);
			if (r <= 0f)
				return pos;
			
			var tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
			var cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
			var sinTheta = cosTheta * tanTheta;
			
			if (launch)
			{
				var projectile = _projectileFactory.Create(_projectileType, pos);
				projectile.Launch(_curTarget.transform.gameObject,
					new Vector3(s * cosTheta * dir.z, s * sinTheta, s * cosTheta * dir.x), g, pos);
			}
			
			pos.x = dir.z * cosTheta * s * time;
			pos.z = dir.x * cosTheta * s * time;
			pos.y = -dir.y * (sinTheta * s * time - 0.5f * g * time * time);
			
			Quaternion quaternion = Quaternion.LookRotation(new Vector3(dir.z, tanTheta * 0.5f, dir.x));
			barrel.eulerAngles = new Vector3(quaternion.eulerAngles.x, barrel.eulerAngles.y, barrel.eulerAngles.z);
			
			return pos;
		}
		

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(dd, 0.5f);
		}
	}
}