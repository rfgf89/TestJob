using Game;
using Infrastructure;
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

		[SerializeField, Min(1)] private int _cannonLevel; 
		
		[SerializeField] private ProjectileType _projectileType;
		[SerializeField] private bool _ballistics;
		[SerializeField] private float _shootInterval = 0.5f;
		[SerializeField, Range(0.1f, 9.8f)] private float _gravity = 9.80f;
		[SerializeField] private float _angleSpeed;
		[SerializeField] private float _angleRange;
		[SerializeField] private float _attackRadius;
		[SerializeField] private LayerMask _layerMask;

		private Vector3 angleVelocity;
		private ITowerTarget _curTarget;
		private IProjectileFactory _projectileFactory;
		private Projectile _curProjectile;
		private float _lastShotTime = -0.5f;
		public override int Level => _cannonLevel;

		public void Init(IProjectileFactory projectileFactory)
		{
			_projectileFactory = projectileFactory;
		}

		public void GameUpdate(float deltaTime, float time)
		{
			if (_curProjectile == null)
				_curProjectile = _projectileFactory.Create(_projectileType, transform.position + Vector3.up * 1000f);
			
			if (TryGetTarget(out _curTarget, _attackRadius, _layerMask))
			{
				if (_ballistics)
				{
					var posPredict = PredictBallistics(_curTarget);
					DampAngle(_hub, posPredict - _shootPoint.position, out var angleFrom, out var angleTo);
					if (TryShot(_curTarget, angleFrom, angleTo, time))
					{
						LaunchBallistics(_barrel, _shootPoint.position, posPredict, 0f, true);
						_lastShotTime = time;
					}
				}
				else
				{
					var posPredict = Predict(_curTarget);
					DampAngle(_hub, posPredict - _shootPoint.position, out var angleFrom, out var angleTo);
					if (TryShot(_curTarget, angleFrom, angleTo, time))
					{
						Launch(_curTarget, posPredict);
						_lastShotTime = time;
					}
				}
			}
		}

		private void Launch(ITowerTarget target, Vector3 targetPos)
		{
			var an = Quaternion.LookRotation(targetPos - _shootPoint.position);
			_barrel.eulerAngles = new Vector3(an.eulerAngles.x, _barrel.eulerAngles.y, _barrel.eulerAngles.z);

			if (_curProjectile != null)
			{
				_curProjectile.Launch(_curTarget.transform.gameObject, _barrel.forward * 10f, 0f, _shootPoint.position);
				_curProjectile = null;
			}
		}

		private Vector3 Predict(ITowerTarget target)
		{
			return target.transform.position + target.Velocity.normalized
				* (target.transform.position + target.Velocity
				   - _shootPoint.position).magnitude / (_curProjectile.Speed * 10f) * target.Velocity.magnitude;
		}

		private Vector3 PredictBallistics(ITowerTarget target)
		{
			return target.transform.position + target.Velocity.normalized
				* (target.transform.position + target.Velocity
				   - _shootPoint.position).magnitude / (
					LaunchBallistics(_barrel, _shootPoint.position, target.transform.position, 2f, false)
					* _curProjectile.Speed * (_gravity / 9.8f * 0.7f)) * target.Velocity.magnitude;
		}
		
		private bool TryShot(ITowerTarget target, Vector3 angleFrom, Vector3 angleTo, float time)
		{
			return _lastShotTime + _shootInterval < time
			       && angleTo.y > angleFrom.y - _angleRange 
			       && angleTo.y < angleFrom.y + _angleRange 
			       && math.distance(transform.position, target.transform.position) < _attackRadius - target.MaxBoundSize;
		}

		private void DampAngle(Transform hub, Vector3 targetPos, out Vector3 angleFrom, out Vector3 angleTo)
		{
			Vector3 angle = hub.eulerAngles;
			var angleQ = Quaternion.LookRotation(targetPos);
			angle.y = Mathf.SmoothDampAngle(hub.eulerAngles.y, angleQ.eulerAngles.y, ref angleVelocity.y, _angleSpeed);
			hub.eulerAngles = angle;
			
			angleFrom = angle;
			angleTo = angleQ.eulerAngles;
		}
		private float LaunchBallistics(Transform barrel, Vector3 pos, Vector3 endPos, float time, bool launch)
		{
			
			float3 dir = float3.zero;

			dir.x = endPos.z - pos.z;
			dir.y = endPos.y - pos.y;
			dir.z = endPos.x - pos.x;
		
			var x = math.length(dir.xyz);
			var y = - pos.y;
			dir /= x;
		
			var g = _gravity;
			float x1 = x + 0.01f;
			var s = Mathf.Sqrt((y + Mathf.Sqrt(x1 * x1 + y * y)) * 9.81f);
			var s2 = s * s;
		
			var r = s2 * s2 - g * (g * x * x + 2f * y * s2);
			if (r <= 0f)
				return s;
			
			var tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
			var cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
			var sinTheta = cosTheta * tanTheta;
			
			if (launch && _curProjectile != null)
			{
				_curProjectile.Launch(_curTarget.transform.gameObject,
					new Vector3(s * cosTheta * dir.z, s * sinTheta, s * cosTheta * dir.x), g, pos);
				_curProjectile = null;
			}
			
			Quaternion quaternion = Quaternion.LookRotation(new Vector3(dir.z, tanTheta, dir.x));
			barrel.eulerAngles = new Vector3(quaternion.eulerAngles.x, barrel.eulerAngles.y, barrel.eulerAngles.z);
			
			return s;
		}
		
	}
}