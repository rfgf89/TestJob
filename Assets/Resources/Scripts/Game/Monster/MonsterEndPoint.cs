using UnityEngine;

namespace Game.Monsters
{
    [RequireComponent(typeof(Collider))]
    public class MonsterEndPoint : MonoBehaviour
    {
        private GameLoop _gameLoop;
        public void Init(GameLoop gameLoop)
        {
            _gameLoop = gameLoop;
        }
        
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
            {
                Destroy(other.gameObject);
                _gameLoop.Remove(other.gameObject.GetComponent<IGameUpdate>());
            }
        }
    }
}