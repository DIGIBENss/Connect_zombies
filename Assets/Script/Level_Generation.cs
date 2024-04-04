
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Generation : MonoBehaviour
{
    [Header("UI")]
 

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Transform[] _spawnerposition = new Transform[5];
    [SerializeField] private Transform[] _spawnerspare;
    [SerializeField] private List<GameObject>  _levelobject1, _levelobject2, _levelobject3, _levelobject4;
    [SerializeField] private Vector3 spawnSize = new Vector3(1, 1, 1);
    [SerializeField] private LayerMask obstacleLayer; 
    private int _tier;
    private int _spawnCount = 0;
    int[] waveThresholds = new int[] {0, 2, 5, 10, 15, 25 };
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.15f);  
    }
    public void LoadRoad()
    {
        NextlvlApp();
        Increasingroads();
    }
    private void Increasingroads()
    {
        for (int i = 0; i < waveThresholds.Length; i++)
        {
            if (_gameManager.Wavecout >= waveThresholds[i])
            {
                _spawnerposition[_spawnCount] = _spawnerspare[_spawnCount];
                Instantiate(Proverka(), _spawnerposition[_spawnCount].position, Quaternion.identity, _spawnerposition[_spawnCount]);
                _spawnCount++;
            }
        }
    }
    public void NextlvlApp()
    {
       if(_gameManager.Wavecout >= 14 && _gameManager.Wavecout < 15)
       {
            _tier = 1;
            print(_tier);
       }
       else if(_gameManager.Wavecout >= 29 && _gameManager.Wavecout < 30)
       {
            _tier = 2;
            print(_tier);
        }
       else if (_gameManager.Wavecout >= 44 )
       {
            print(_tier);
            _tier = 3;
       }
    }
    private GameObject Proverka()
    {
        switch(_tier)
        {
            case 0:
                return _levelobject1[UnityEngine.Random.Range(0, _levelobject1.Count)];
            case 1:
                return _levelobject2[UnityEngine.Random.Range(0, _levelobject2.Count)];
            case 2:
                return _levelobject3[UnityEngine.Random.Range(0, _levelobject3.Count)];
            case 3:
                return _levelobject4[UnityEngine.Random.Range(0, _levelobject4.Count)];
            default:
                return null;
        }
    }
    private bool IsPositionFree(Vector3 position)
    {
        // Проверяем, есть ли в указанной позиции какие-либо объекты, используя bounding box проверку
        bool isOccupied = Physics.CheckBox(position, spawnSize / 2, Quaternion.identity, obstacleLayer);
        return !isOccupied; // Если область занята - возвращаем false, если свободна - true
    }
}
