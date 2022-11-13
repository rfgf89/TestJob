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
        _monsterFactory = new GeneralMonsterFactory(_configMonster, _gameLoop);
        _projectileFactory = new GeneralProjectileFactory(_configProjectile, _gameLoop);
        _towerPartFactory = new GeneralTowerPartFactory(_configTower);
        _cannonPartFactory = new GeneralCannonPartFactory(_configCannon, _gameLoop, _projectileFactory);
        
        ///Dependencies
        _monsterEndPoint.Init(_gameLoop);
        _spawner.Init(_monsterFactory);
        
        //Initialization 
        _gameLoop.Add(_spawner);

        CreateAllTower(_towerMarkers, _towerPartFactory, _cannonPartFactory);
    }

    private void CreateAllTower(TowerMarker[] towerMarkers, GeneralTowerPartFactory towerPartFactory, GeneralCannonPartFactory cannonPartFactory)
    {
        foreach (var marker in towerMarkers)
        {
            var tower = towerPartFactory.Create(marker, marker.transform.position);;
            cannonPartFactory.Create(marker, marker.transform.position, tower.transform);
        }
    }
}