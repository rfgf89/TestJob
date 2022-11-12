using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameLoop : MonoBehaviour
    {
        private readonly List<IGameUpdate> _behaviours = new List<IGameUpdate>();
        private readonly List<IGameUpdate> _behavioursDelete = new List<IGameUpdate>();
        private readonly List<IGameUpdate> _behavioursAdd = new List<IGameUpdate>();

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
                    gameBeh.GameUpdate(Time.deltaTime, Time.time);
            }
        }

        public void Add(IGameUpdate gameBeh)
        {
            if(gameBeh!=null)
                _behavioursAdd.Add(gameBeh);
        }

        public void Remove(IGameUpdate gameBeh)
        {
            if(gameBeh!=null)
                _behavioursDelete.Add(gameBeh);
        }

    }
}