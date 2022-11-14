using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Game
{
    public class GameLoop : MonoBehaviour
    {

        [SerializeField] private float _timeSpeed = 1f;
        [SerializeField] private float _time;
        [SerializeField] private float _curTimeSpeed;
        
        private readonly List<IGameUpdate> _behaviours = new List<IGameUpdate>();
        private readonly List<IGameUpdate> _behavioursDelete = new List<IGameUpdate>();
        private readonly List<IGameUpdate> _behavioursAdd = new List<IGameUpdate>();
        
        private void Update()
        {
            _curTimeSpeed = _timeSpeed * Time.deltaTime;
            
            foreach (var gameBeh in _behavioursDelete)
                _behaviours.Remove(gameBeh);

            foreach (var gameBeh in _behavioursAdd)
                _behaviours.Add(gameBeh);

            _behavioursDelete.Clear();
            _behavioursAdd.Clear();

            foreach (var gameBeh in _behaviours)
            {
                if (gameBeh != null)
                    gameBeh.GameUpdate(_curTimeSpeed, _time);
            }

            _time += _curTimeSpeed;
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