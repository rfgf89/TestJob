using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameLoop : MonoBehaviour
    {
        private readonly Dictionary<int, GameBehaviour> _behaviours = new Dictionary<int, GameBehaviour>();
        private readonly HashSet<int> _behavioursDelete = new HashSet<int>();
        private readonly HashSet<GameBehaviour> _behavioursAdd = new HashSet<GameBehaviour>();
        private float _time;
        private int _indexer;

        private void Update()
        {
            foreach (var index in _behavioursDelete)
            {
                if (_behaviours.ContainsKey(index))
                    _behaviours.Remove(index);
            }

            foreach (var gameBeh in _behavioursAdd)
                _behaviours.Add(gameBeh.LoopIndex, gameBeh);

            _behavioursDelete.Clear();
            _behavioursAdd.Clear();

            foreach (var gameBeh in _behaviours.Values)
            {
                if (gameBeh != null)
                    gameBeh.GameUpdate(Time.deltaTime, _time);
            }

            _time += Time.deltaTime;
        }

        public int Add(GameBehaviour gameBehaviour)
        {
            _behavioursAdd.Add(gameBehaviour);
            return ++_indexer;
        }

        public void Remove(int index)
        {
            _behavioursDelete.Add(index);
        }
    }
}