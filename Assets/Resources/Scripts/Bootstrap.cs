using Game;
using Game.Config;
using Game.Monsters;
using Infrastructure;
using Towers;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    //Game
    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private TowerMarker[] _towerMarkers;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private MonsterEndPoint _monsterEndPoint;
    
    //Infrastructure
    [SerializeField] private GeneralMonsterScriptableObject _configMonster;
    [SerializeField] private GeneralProjectileScriptableObject _configProjectile;
    [SerializeField] private GeneralTowerScriptableObject _configTower;
    [SerializeField] private GeneralCannonScriptableObject _configCannon;
    
    private GeneralMonsterFactory _monsterFactory;
    private GeneralProjectileFactory _projectileFactory;
    private GeneralTowerPartFactory _towerPartFactory;
    private GeneralCannonPartFactory _cannonPartFactory;

    private void Start()
    {
        //Infrastructures
        _monsterFactory = new GeneralMonsterFactory();
        _projectileFactory = new GeneralProjectileFactory();
        _towerPartFactory = new GeneralTowerPartFactory();
        _cannonPartFactory = new GeneralCannonPartFactory();
        
        ///Dependencies
        _projectileFactory.Init(_configProjectile, _gameLoop);
        _monsterFactory.Init(_configMonster, _gameLoop);
        _towerPartFactory.Init(_configTower);
        _cannonPartFactory.Init(_configCannon, _gameLoop, _projectileFactory);
        
        _monsterEndPoint.Init(_gameLoop);
        _spawner.Init(_monsterFactory);
        
        //Initialization 
        _gameLoop.Add(_spawner);
        
        foreach (var marker in _towerMarkers)
        {
            var tower = _towerPartFactory.Create(marker, marker.transform.position);;
            _cannonPartFactory.Create(marker, marker.transform.position, tower.transform);
        }

    }
}