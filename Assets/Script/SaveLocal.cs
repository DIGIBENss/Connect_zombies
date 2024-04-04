using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class SaveLocal : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PrefabsZombieUpdate _prefabsZombieUpdate;
    [SerializeField] private Level_Generation _generation;
    [SerializeField] private Task _task;
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;
    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.05f);
    }
    public void Save()
    {
        YandexGame.savesData.ZombieSaveYG = _prefabsZombieUpdate.ZombieeV.ToList();
        YandexGame.savesData.Money = _wallet.WalletCout;
        YandexGame.savesData.WaveZombie = _gameManager.Wavecout;
        YandexGame.savesData.MissionCount = _task._missioncount;
        YandexGame.savesData.MissioncountMax = _task._missioncountmax;
        YandexGame.savesData.ReawrdMoney = _task._reawrdmoney;
        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad()
    {
        _prefabsZombieUpdate.ZombieeV = YandexGame.savesData.ZombieSaveYG;
        _gameManager.Wavecout = YandexGame.savesData.WaveZombie;
        _wallet.WalletCout = YandexGame.savesData.Money;
            _task._reawrdmoney = YandexGame.savesData.ReawrdMoney;
            _task._missioncountmax = YandexGame.savesData.MissioncountMax;
            _task._missioncount = YandexGame.savesData.MissionCount;
        _generation.LoadRoad();
        _prefabsZombieUpdate.SpawnSavedZombiesSO();
        _wallet.WalledUpdate();
        _gameManager.WaveUpdate();
        _task.UpdateDataHint();
    }
        
}
