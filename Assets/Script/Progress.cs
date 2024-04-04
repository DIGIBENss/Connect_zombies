
using UnityEngine;
using UnityEngine.SceneManagement;

public class Progress : MonoBehaviour
{
    public static Progress Instance { get; private set; }
    [SerializeField] private PrefabsZombieUpdate _prefabsZombieUpdate;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _prefabsZombieUpdate = FindObjectOfType<PrefabsZombieUpdate>();
        //SpawnZombie();
        //StartCoroutine(SpawnZombie());
    }
   // private IEnumerator Start()
    //{
      //  yield return new WaitForSeconds(0.15f);
     //   SpawnZombie();
    //}
    private void SpawnZombie()
    {
        //yield return new WaitForSeconds(0.15f);
        _prefabsZombieUpdate.SpawnSavedZombiesSO();


    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.K))
        {
            SpawnZombie();
        }
           
        //StartCoroutine(SpawnZombie());
    }
    private void Awake()
    {
     
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
