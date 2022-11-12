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
		
		public override Tower Init(IProjectileFactory projectileFactory)
		{
			_projectileFactory = projectileFactory;
			return this;
		}

		private void Update () 
		{
			if (TryGetTarget(out _curTarget, _attackRadius, _layerMask) && TryShot(_hub, _curTarget))
					Shoot(_curTarget);
			
			CalculateAllProjectile(_projectiles, _barrel, _shootPoint);
			
		}

		private void Shoot(ITowerTarget target)
		{
			var targetPos = target.gameObject.transform.position + target.Velocity.normalized / (_speed / 2) * target.Speed;
					
			Projectile projectile = _projectileFactory.Create(_projectileType, _shootPoint.position);
			projectile.SetTarget(target.gameObject);
			_projectiles.Add(new ProjectileControlData(projectile, -1.0f, targetPos));
			
			_lastShotTime = Time.time;
		}
		
		private bool TryShot(Transform hub, ITowerTarget target)
		{
			Vector3 angle = hub.eulerAngles;
			Quaternion quaternion = Quaternion.LookRotation(target.gameObject.transform.position - transform.position);
			angle.y = Mathf.SmoothDampAngle(hub.eulerAngles.y, quaternion.eulerAngles.y, ref angleVelocity.y, _angleSpeed);
			hub.eulerAngles = angle; 
			
			return _lastShotTime + _shootInterval < Time.time 
			       && quaternion.eulerAngles.y > angle.y - _angleRange 
			       && quaternion.eulerAngles.y < angle.y + _angleRange 
			       && math.distance(transform.position, target.gameObject.transform.position) < _attackRadius - target.MaxBoundSize;
		}
	
		private void CalculateAllProjectile(IList<ProjectileControlData> projectiles, Transform barrel, Transform shotPos)
		{
			for (int i = 0; i < projectiles.Count; i++)
			{
				if (projectiles[i].projectile != null)
				{
					projectiles[i].time += Time.deltaTime * _speed;

					if (projectiles[i].projectile.transform.position.y < projectiles[i].target.y )
					{
						Destroy(projectiles[i].projectile.gameObject);
						projectiles.RemoveAt(i);
						i--;
						continue;
					}

					projectiles[i].projectile.transform.position = 
						shotPos.position + GetBallistics(barrel, Vector3.zero, 
							projectiles[i].target - shotPos.position, projectiles[i].time + 1.0f);;
				}
			}
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