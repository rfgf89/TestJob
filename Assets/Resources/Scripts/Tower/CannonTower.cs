using System.Collections.Generic;
using Infrastructure;
using Towers.Enums;
using Towers.Projectiles;
using Unity.Mathematics;
using UnityEngine;

namespace Towers
{


	public class CannonTower : Tower 
	{
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private Transform _hub;
		[SerializeField] private Transform _barrel;

		[SerializeField] private ProjectileType _projectileType;
		[SerializeField] private CannonMode _cannonMode;
		[SerializeField] private float _shootInterval = 0.5f;
		[SerializeField] private float _speed;
		[SerializeField] private float _angleSpeed;
		[SerializeField] private float _angleRange;
		[SerializeField] private float _attackRadius;
		[SerializeField] private LayerMask _layerMask;
		
		private readonly List<ProjectileControlData> _projectiles = new List<ProjectileControlData>();
		private Vector3 angleVelocity;
		private ITowerTarget _curTarget;
		private IProjectileFactory _projectileFactory;
		private float _lastShotTime = -0.5f;

		private enum CannonMode
		{
			Standart,
			Ballistics 
		}
		
		public override Tower Init(IProjectileFactory projectileFactory)
		{
			_projectileFactory = projectileFactory;
			return this;
		}

		public override void GameUpdate(float deltaTime, float time)
		{
			if (TryGetTarget(out _curTarget, _attackRadius, _layerMask) && TryShot(_hub, _curTarget, time))
					Shoot(_curTarget, time);
			
			CalculateAllProjectile(_projectiles, _barrel, _shootPoint, deltaTime);
		}
		
		private void Shoot(ITowerTarget target, float time)
		{
			var targetPos = target.gameObject.transform.position + target.Velocity.normalized / (_speed / 2) * target.Speed;
					
			Projectile projectile = _projectileFactory.Create(_projectileType, _shootPoint.position);
			projectile.SetTarget(target.gameObject);
			_projectiles.Add(new ProjectileControlData(projectile, -1.0f, targetPos));
			
			_lastShotTime = time;
		}
		
		private bool TryShot(Transform hub, ITowerTarget target, float time)
		{
			Vector3 angle = hub.eulerAngles;
			Quaternion quaternion = Quaternion.LookRotation(target.gameObject.transform.position - transform.position);
			angle.y = Mathf.SmoothDampAngle(hub.eulerAngles.y, quaternion.eulerAngles.y, ref angleVelocity.y, _angleSpeed);
			hub.eulerAngles = angle; 
			
			return _lastShotTime + _shootInterval < time
			       && quaternion.eulerAngles.y > angle.y - _angleRange 
			       && quaternion.eulerAngles.y < angle.y + _angleRange 
			       && math.distance(transform.position, target.gameObject.transform.position) < _attackRadius - target.MaxBoundSize;
		}
	
		private void CalculateAllProjectile(IList<ProjectileControlData> projectiles, Transform barrel, Transform shotPos, float deltaTime)
		{
			for (int i = 0; i < projectiles.Count; i++)
			{
				if (projectiles[i].projectile != null)
				{
					projectiles[i].time += deltaTime * _speed;

					if (projectiles[i].projectile.transform.position.y < projectiles[i].target.y )
					{
						Destroy(projectiles[i].projectile.gameObject);
						projectiles.RemoveAt(i);
						i--;
						continue;
					}

					projectiles[i].projectile.transform.position = 
						shotPos.position + GetProjectilePos(_cannonMode, barrel, Vector3.zero, 
							projectiles[i].target - shotPos.position, projectiles[i].time + 1.0f);;
				}
			}
		}

		private Vector3 GetProjectilePos(CannonMode cannonMode, Transform barrel, Vector3 pos, Vector3 endPos, float time)
		{
			switch (cannonMode)
			{
				case CannonMode.Standart : return GetStandart(barrel, pos, endPos, time);
				case CannonMode.Ballistics : return GetBallistics(barrel, pos, endPos, time);
			}
			
			return Vector3.zero;
		}

		private Vector3 GetStandart(Transform barrel, Vector3 pos, Vector3 endPos, float time)
		{
			var newPos = Vector3.Lerp(pos, endPos, time);
			
			Quaternion quaternion = Quaternion.LookRotation(endPos);
			barrel.eulerAngles = new Vector3(quaternion.eulerAngles.x, barrel.eulerAngles.y, barrel.eulerAngles.z);
			
			return newPos;
		}
		private Vector3 GetBallistics(Transform barrel, Vector3 pos, Vector3 endPos, float time)
		{
			float3 dir = float3.zero;
			dir.x = endPos.z - pos.z;
			dir.y = endPos.y - pos.y;
			dir.z = endPos.x - pos.x;
		
			var x = math.length(dir.xyz);
			var y = - pos.x;
			dir /= x;
		
			var g = 9.81f;
			float x1 = x + 1f;
			var s = Mathf.Sqrt((y + Mathf.Sqrt(x1 * x1 + y * y)) * g);
			var s2 = s * s;
		
			var r = s2 * s2 - g * (g * x * x + 2f * y * s2);
			if (r <= 0f)
				return pos;
			
			var tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);

			var cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
			var sinTheta = cosTheta * tanTheta;
   
			pos.x = dir.z * cosTheta * s * time;
			pos.z = dir.x * cosTheta * s * time;
			pos.y = -dir.y * (sinTheta * s * time -0.8f * g * time * time);
		
			Quaternion quaternion = Quaternion.LookRotation(new Vector3(dir.z, tanTheta*0.5f, dir.x));
			barrel.eulerAngles = new Vector3(quaternion.eulerAngles.x, barrel.eulerAngles.y, barrel.eulerAngles.z);
			
			return pos;
		}
		
	}
}