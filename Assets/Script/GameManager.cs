
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _win;
    [SerializeField] private GameObject _fail;
    [SerializeField] private GameObject _gameUi;
    [SerializeField] private GameObject _setting;
    [SerializeField] private Canvas _mainUi;
    [SerializeField] private TextMeshProUGUI _textCoutWave;
    [SerializeField] private CameraFollow _cameraFollow;
    [SerializeField] private PrefabsZombieUpdate _prefabsZombieUpdate;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private SaveLocal _savelocal;
    [SerializeField] private string[] _selectiontext;
    [SerializeField] private TextMeshProUGUI _reloadtext;
    [Header("AD")]
    [SerializeField] YandexGame _sdk;
    [SerializeField] GameObject _fulladScreen;
    private float _timer = 10f;
    [SerializeField] bool _IsAdplay;
    [SerializeField] private TextMeshProUGUI _textad;

    public bool IsReload;
    public bool IsStartReload;
    public bool IsStopReload;
    
    public int Wavecout;
    public EnemyAi[] _enemyAi;
    [SerializeField] GameObject LoadingScreen;
    public Slider scale;
    [SerializeField] private Task _task;
    private void OnApplicationQuit()
    {
        _savelocal.Save();
        Debug.Log("Выход");
    }
    public void ResetProgress() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void FullcreenShow() => _sdk._FullscreenShow();
    private void Update()
    {
        //AddSdk();   
    }
    private void Awake()
    {
        if(Application.isMobilePlatform)
        {
            Loading();
        }
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(AdFullScreen());
        _cameraFollow = FindObjectOfType<CameraFollow>();
    }
    public void WaveUpdate()
    {
        _textCoutWave.text = Wavecout.ToString();
    }
    
    public void GameOver()
    {
        PauseGames();
        int random  = Random.Range(0, _selectiontext.Length);
        _reloadtext.text = _selectiontext[random];
        _fail.SetActive(true);
        _gameUi.SetActive(false);

    }
    public void Setting()
    {
        PauseGames();
        if (_setting.activeSelf != true) _setting.SetActive(true);
        else _setting.SetActive(false);
    }    
    public void ReloadSceneFail()
    {
       
        SceneManager.LoadScene("Main_scene");
        Wavecout--;
        _savelocal.Save();
        IsStopReload = true;
        _textCoutWave.text = Wavecout.ToString();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("Main_scene");
        IsStopReload = true;
    }
    public void StartGames()
    {
        if(_prefabsZombieUpdate != null)
        _prefabsZombieUpdate.ReplaceZombies();
        SearchTarget();
        IsStartReload = true;
        _mainUi.enabled = false;
    }
    private void SearchTarget()
    {
        List<Transform> availableZombies = new List<Transform>();
        _enemyAi = FindObjectsOfType<EnemyAi>();
        
        foreach (EnemyAi enemy in _enemyAi)
        {
            availableZombies.Add(enemy.transform);
        }
        if (availableZombies.Count > 0)
        {
            Transform randomZombieTransform = availableZombies[Random.Range(0, availableZombies.Count)];
            if (_cameraFollow != null) _cameraFollow.target = randomZombieTransform;
            else Debug.LogWarning("Компонент CameraFollow не найден.");
        }
        else Debug.LogWarning("Не найдено объектов с компонентом EnemyAi.");

    }
    public void PassedLvl()
    {
        PauseGames();
        _audioManager.PlaySound(2);
        _win.SetActive(true);
        _gameUi.SetActive(false);
        IsStopReload = true;
    }
    public void NextLvl()
    {
        ReloadScene();
        YandexGame.NewLeaderboardScores("WaveCout", Wavecout++);
        _task.UpdateCompleteLevels(1);
        _savelocal.Save();
        _textCoutWave.text = Wavecout.ToString();

    }
    public void AddSdk()
    {

        if (_IsAdplay == true)
        {
            int roundedTimer = Mathf.FloorToInt(_timer);
            if (roundedTimer >= 0)
            {
                _timer -= Time.deltaTime;
                _textad.text = roundedTimer.ToString();
            }
            if(_timer < 0)
            {
                _IsAdplay = false;
                _fulladScreen.SetActive(false);
                FullcreenShow();
            }  
        }
    }
    private IEnumerator AdFullScreen()
    {
        yield return new WaitForSeconds(60f);
        _fulladScreen.SetActive(true);
        _IsAdplay = true;
    }
    private bool _isPause = true;
    private void PauseGames()
    {
        if(_isPause)
        {
            Time.timeScale = 0;
            _isPause = false;
        }
        else
        {
            Time.timeScale = 1;
            _isPause = true;
        }
    }
    public void Loading()
    {
        LoadingScreen.SetActive(true);
        StartCoroutine(PseudoLoading());
    }
    private IEnumerator PseudoLoading()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.2f);
            scale.value += 0.2f;
        }
        LoadingScreen.SetActive(false);
    }
} 

