using UnityEngine;

namespace Game.Monsters
{
    [RequireComponent(typeof(Collider))]
    public class MonsterEndPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, transform.localScale);
            Gizmos.color = Color.white;
        }

        private void OnTriggerEnter(Collider other)
        {
            var monster = other.gameObject.GetComponent<IDestroyableInEndPoint>();
            if (monster != null)
                Destroy(other.gameObject);
        }
    }
}