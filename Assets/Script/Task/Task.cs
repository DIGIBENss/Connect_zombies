using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using YG;

public class Task : MonoBehaviour
{
    [Header("Ìèññèè")]
    [SerializeField] private TextMeshProUGUI[] _textmission;
    [SerializeField] private TextMeshProUGUI[] _textmissionñout;
    [SerializeField] private TextMeshProUGUI[] _textmissionñoutmax;
    public int[] _missioncount = new int[4];
    public int[] _missioncountmax = new int [4];
    [Header("Íàãðàäà")]
    [SerializeField] private TextMeshProUGUI[] _textrewardcout;
    [SerializeField] private Button[] _buttonreward;
    [SerializeField] private Image[] _imagesreward;
    [SerializeField] private BlinkingImages _images;
    public int[] _reawrdmoney = new int[4];

    [Header("Links")]
    [SerializeField] private Wallet _wallet;
    private void StartBuyZombies(int i)
    {
        if (_missioncount[i] == _missioncountmax[i]) SelectReward(i);
    }
    private void StartCompleteLevels(int i)
    {
        if (_missioncount[i] == _missioncountmax[i]) SelectReward(i);
    }
    private void StartConnectZombies(int i)
    {
        if (_missioncount[i] == _missioncountmax[i]) SelectReward(i);
    }
    private void StartKillSoldier(int i)
    {
        if (_missioncount[i] == _missioncountmax[i]) SelectReward(i);
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.15f);
        StartBuyZombies(0);
        StartCompleteLevels(1);
        StartConnectZombies(2);
        StartKillSoldier(3);
    }
    public void UpdateDataHint()
    {
        for (int i = 0; i < _missioncount.Length; i++)
        {
            _textmissionñout[i].text = _missioncount[i].ToString();
        }
        for (int i = 0; i < _missioncountmax.Length; i++)
        {
            _textmissionñoutmax[i].text = _missioncountmax[i].ToString();
        }
        for (int i = 0; i < _reawrdmoney.Length; i++)
        {
            _textrewardcout[i].text = _reawrdmoney[i].ToString();
        }
    }
    private void SelectReward(int value)
    {
        if(value == 0)
        {
            _imagesreward[0].enabled = true;
            StartCoroutine(_images.BlinkImage(_imagesreward[0]));
        }
        if (value == 1)
        {
            _imagesreward[1].enabled = true;
            StartCoroutine(_images.BlinkImage(_imagesreward[1]));
        }
        if (value == 2)
        {
            _imagesreward[2].enabled = true;
            StartCoroutine(_images.BlinkImage(_imagesreward[2]));
        }
        if (value == 3)
        {
            _imagesreward[3].enabled = true;
            StartCoroutine(_images.BlinkImage(_imagesreward[3]));
        }
    }
    private void Offimage(int value)
    {
        switch(value)
        {
            case 0:
                _imagesreward[0].enabled = false;
                break;
            case 1:
                _imagesreward[1].enabled = false;
                break;
            case 2:
                _imagesreward[2].enabled = false;
                break;
            case 3:
                _imagesreward[3].enabled = false;
                break;
        }
    }
    private int IncreaseMoney(int money)
    {
        float a = 0;
        a = money * 0.5f;
        money += (int)a;
        return money;
    }
    private int IncreaseValue(int value)
    {
        float a = 0;
        if (value < 5)
        {
            a = value + 2;
            value = (int)a;
        }
        else
        {
            a = value + 5;
            value = (int)a;
        }
        return value;
    }
    public void OnBuyZombies(int i)
    {
        // i = 0
        if(_missioncount[i] == _missioncountmax[i])
        {
            _reawrdmoney[i] = IncreaseMoney(_reawrdmoney[i]);
            _wallet.AddMoney(_reawrdmoney[i]);
            _missioncountmax[i] = IncreaseValue(_missioncountmax[i]);
            _missioncount[i] = 0;
            _textmissionñout[i].text = _missioncount[i].ToString();
            _textrewardcout[i].text = _reawrdmoney[i].ToString();
            _textmissionñoutmax[i].text = _missioncountmax[i].ToString();
            Offimage(i);
        }
       
    }
    public void UpdateBuyZombies(int i)
    {
        // i = 0
        _missioncount[i] = Mathf.Min(_missioncount[i] + 1, _missioncountmax[i]);
        if (_missioncount[i] == _missioncountmax[i])
        {
            SelectReward(i);
        }
        _textmissionñout[i].text = _missioncount[i].ToString();

    }
    public void OnCompleteLevels(int i)
    {
        // i = 1
        if (_missioncount[i] == _missioncountmax[i])
        {
            _reawrdmoney[i] = IncreaseMoney(_reawrdmoney[i]);
            _wallet.AddMoney(_reawrdmoney[i]);
            _missioncountmax[i] = IncreaseValue(_missioncountmax[i]);
            _missioncount[i] = 0;
            _textmissionñout[i].text = _missioncount[i].ToString();
            _textrewardcout[i].text = _reawrdmoney[i].ToString();
            _textmissionñoutmax[i].text = _missioncountmax[i].ToString();
            Offimage(i);
        }
    }
    public void UpdateCompleteLevels(int i)
    {
        // i = 1
        _missioncount[i] = Mathf.Min(_missioncount[i] + 1, _missioncountmax[i]);
        if (_missioncount[i] == _missioncountmax[i])
        {
            SelectReward(i);
        }
        _textmissionñout[i].text = _missioncount[i].ToString();
    }
    public void OnConnectZombies(int i)
    {
        // i = 2
        if (_missioncount[i] == _missioncountmax[i])
        {
            _reawrdmoney[i] = IncreaseMoney(_reawrdmoney[i]);
            _wallet.AddMoney(_reawrdmoney[i]);
            _missioncountmax[i] = IncreaseValue(_missioncountmax[i]);
            _missioncount[i] = 0;
            _textmissionñout[i].text = _missioncount[i].ToString();
            _textrewardcout[i].text = _reawrdmoney[i].ToString();
            _textmissionñoutmax[i].text = _missioncountmax[i].ToString();
            Offimage(i);
        }
    }
    public void UpdateConnectZombies(int i)
    {
        // i = 2
        _missioncount[i] = Mathf.Min(_missioncount[i] + 1, _missioncountmax[i]);
        if (_missioncount[i] == _missioncountmax[i])
        {
            SelectReward(i);
        }
        _textmissionñout[i].text = _missioncount[i].ToString();

    }
    public void OnKillSoldier(int i)
    {
        // i = 3
        if (_missioncount[i] == _missioncountmax[i])
        {
            _reawrdmoney[i] = IncreaseMoney(_reawrdmoney[i]);
            _wallet.AddMoney(_reawrdmoney[i]);
            _missioncountmax[i] = IncreaseValue(_missioncountmax[i]);
            _missioncount[i] = 0;
            _textmissionñout[i].text = _missioncount[i].ToString();
            _textrewardcout[i].text = _reawrdmoney[i].ToString();
            _textmissionñoutmax[i].text = _missioncountmax[i].ToString();
            Offimage(i);
        }
    }
    public void UpdateKillSoldier(int i)
    {
        // i = 3
        _missioncount[i] = Mathf.Min(_missioncount[i] + 1, _missioncountmax[i]);
        if (_missioncount[i] == _missioncountmax[i])
        {
            SelectReward(i);
        }
        _textmissionñout[i].text = _missioncount[i].ToString();

    }
}
