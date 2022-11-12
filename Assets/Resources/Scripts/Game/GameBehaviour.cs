using UnityEngine;

namespace Game
{
    public class GameBehaviour : MonoBehaviour
    {
        public int LoopIndex { get; private set; }
        private GameLoop _gameLoop;

        public void InitLoop(GameLoop gameLoop)
        {
            _gameLoop = gameLoop;
            LoopIndex = _gameLoop.Add(this);
        }

        private void OnDestroy()
        {
            if (_gameLoop != null)
                _gameLoop.Remove(LoopIndex);
            GameOnDestroy();
        }

        protected virtual void GameOnDestroy()
        {
        }

        public virtual void GameUpdate(float deltaTime, float time)
        {
        }

    }
}