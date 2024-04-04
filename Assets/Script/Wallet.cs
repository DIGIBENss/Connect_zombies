using System.Collections;
using TMPro;
using UnityEngine;
using YG;

public class Wallet : MonoBehaviour
{
    [SerializeField] PrefabsZombieUpdate _prefabsZombieUpdate;
    [SerializeField] TextMeshProUGUI _textWallet;
    [SerializeField] AudioManager _audioManager;
    [SerializeField] SaveLocal _savelocal;
    [SerializeField] private Task _task;
    [SerializeField] private AudioSource _audioSource;
    public int WalletCout;
    public int CoutMoneylvl;
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
    public void ExampleOpenRewardAd(int id) => YandexGame.RewVideoShow(id);
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.15f);
    }
    public void WalledUpdate()
    {
        _textWallet.text = WalletCout.ToString();
    }
    public void AddMoney(int money)
    {
        WalletCout += money;
        _textWallet.text = WalletCout.ToString();
        _audioSource.Play();
        _savelocal.Save();
    }
    private void Rewarded(int id)
    {
        if (id == 1)
            AddMoney(100);
        else if (id == 2)
            AddMoney(50);
        else if (id == 3)
            AddMoney(0);
    }
    public void BuyZombie()
    {
        if(WalletCout >= 50)
        {
            bool isSpawned = _prefabsZombieUpdate.TrySpawnZombie();
            if(isSpawned)
            {
                _audioManager.PlaySound(0);
                WalletCout -= 50;
                _task.UpdateBuyZombies(0);
                _textWallet.text = WalletCout.ToString();
                _savelocal.Save();
            }
        }
    }

}
