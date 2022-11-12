using Game;
using UnityEngine;

namespace Towers.Projectiles
{
	public class CannonProjectile : Projectile
	{
		[SerializeField] private int _damage;

		private void OnTriggerEnter(Collider other)
		{
			var dam = other.gameObject.GetComponent<IDamagable>();
			if (dam == null)
				return;

			dam.Damage(_damage);
			Destroy(gameObject);
		}
	}
}