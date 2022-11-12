using Game;
using UnityEngine;

namespace Infrastructure
{
    public class GameLoopFactory : MonoBehaviour
    {
        private GameLoop _gameLoop;

        public void Init(GameLoop gameLoop)
        {
            _gameLoop = gameLoop;
        }

        protected T Instance<T>(T tower, Vector3 at, Transform parent = null) where T : GameBehaviour
        {
            if (tower != null)
            {
                var towerInst = Instantiate(tower, at + tower.transform.localPosition, tower.transform.rotation,
                    parent);
                towerInst.InitLoop(_gameLoop);
                return towerInst;
            }

            return null;
        }
    }
}