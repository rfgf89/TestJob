using UnityEngine;

namespace Game
{
    public class GameBehaviour : MonoBehaviour
    {
        private GameLoop _gameLoop;

        public void InitLoop(GameLoop gameLoop)
        {
            _gameLoop = gameLoop;
            _gameLoop.Add(this);
        }

        private void OnDestroy()
        {
            _gameLoop.Remove(this);
            GameOnDestroy();
        }

        protected virtual void GameOnDestroy()
        { }

        public virtual void GameUpdate(float deltaTime, float time)
        { }

    }
}