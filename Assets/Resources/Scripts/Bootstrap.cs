using Game;
using Infrastructure;
using Towers;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    //Game
    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private TowerMarker[] _towerMarkers;
    [SerializeField] private Spawner _spawner;

    //Infrastructures 
    [SerializeField] private GeneralMonsterFactory _monsterFactoryPrefab;
    [SerializeField] private GeneralProjectileFactory _projectileFactoryPrefab;
    [SerializeField] private GeneralTowerPartFactory _towerPartFactoryPrefab;
    [SerializeField] private GeneralCannonPartFactory _cannonPartFactoryPrefab;

    private GeneralMonsterFactory _monsterFactory;
    private GeneralProjectileFactory _projectileFactory;
    private GeneralTowerPartFactory _towerPartFactory;
    private GeneralCannonPartFactory _cannonPartFactory;
    private void Start()
    {
        //Infrastructures
        _monsterFactory = Instantiate(_monsterFactoryPrefab, Vector3.zero, Quaternion.identity, null);
        _projectileFactory = Instantiate(_projectileFactoryPrefab, Vector3.zero, Quaternion.identity, null);
        _towerPartFactory = Instantiate(_towerPartFactoryPrefab, Vector3.zero, Quaternion.identity, null);
        _cannonPartFactory = Instantiate(_cannonPartFactoryPrefab, Vector3.zero, Quaternion.identity, null);
        
        ///Dependencies
        _projectileFactory.Init(_gameLoop);
        _monsterFactory.Init(_gameLoop);
        _spawner.Init(_monsterFactory).InitLoop(_gameLoop);
        _towerPartFactory.Init(_gameLoop);
        _cannonPartFactory.Init(_gameLoop, _projectileFactory);
        
        //Initialization 
        foreach (var marker in _towerMarkers)
        {
            var tower = _towerPartFactory.CreatePart(marker, marker.transform.position);
            _cannonPartFactory.CreatePart(marker, marker.transform.position, tower.transform);
        }

    }
}