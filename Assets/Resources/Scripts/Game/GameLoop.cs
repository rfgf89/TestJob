using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameLoop : MonoBehaviour
    {
        private readonly HashSet<GameBehaviour> _behaviours = new HashSet<GameBehaviour>();
        private readonly HashSet<GameBehaviour> _behavioursDelete = new HashSet<GameBehaviour>();
        private readonly HashSet<GameBehaviour> _behavioursAdd = new HashSet<GameBehaviour>();
        private float _time;
        private int _indexer;
        private void Update()
        {
            foreach (var gameBeh in _behavioursDelete)
                _behaviours.Remove(gameBeh);

            foreach (var gameBeh in _behavioursAdd)
                _behaviours.Add(gameBeh);

            _behavioursDelete.Clear();
            _behavioursAdd.Clear();

            foreach (var gameBeh in _behaviours)
            {
                if (gameBeh != null)
                    gameBeh.GameUpdate(Time.deltaTime, _time);
            }

            _time += Time.deltaTime;
        }

        public void Add(GameBehaviour gameBehaviour) => _behavioursAdd.Add(gameBehaviour);
        public void Remove(GameBehaviour gameBeh) => _behavioursDelete.Add(gameBeh);
        
    }
}