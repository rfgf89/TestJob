using Game;
using Game.Config;
using Game.Monsters;
using Infrastructure;
using Towers;
using UnityEngine;
using UnityEngine.Serialization;

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
    [FormerlySerializedAs("_configCannon")] [SerializeField] private GeneralTowerWeaponScriptableObject configTowerWeapon;
    
    private GeneralMonsterFactory _monsterFactory;
    private GeneralProjectileFactory _projectileFactory;
    private GeneralTowerFactory _towerFactory;
    private GeneralTowerWeaponFactory _towerWeaponFactory;

    private void Start()
    {
        //Infrastructures
        _monsterFactory = new GeneralMonsterFactory(_configMonster, _gameLoop);
        _projectileFactory = new GeneralProjectileFactory(_configProjectile, _gameLoop);
        _towerFactory = new GeneralTowerFactory(_configTower);
        _towerWeaponFactory = new GeneralTowerWeaponFactory(configTowerWeapon, _gameLoop, _projectileFactory);
        
        ///Dependencies
        _monsterEndPoint.Init(_gameLoop);
        _spawner.Init(_monsterFactory);
        
        //Initialization 
        _gameLoop.Add(_spawner);

        CreateAllTower(_towerMarkers, _towerFactory, _towerWeaponFactory);
    }

    private void CreateAllTower(TowerMarker[] towerMarkers, GeneralTowerFactory towerFactory, GeneralTowerWeaponFactory towerWeaponFactory)
    {
        foreach (var marker in towerMarkers)
        {
            var tower = towerFactory.Create(marker, marker.transform.position);;
            towerWeaponFactory.Create(marker, marker.transform.position, tower.transform);
        }
    }
}