
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{

    private PrefabsZombieUpdate _update;
    private float _trigercooldown = 0.5f;
    private bool _ischeck;

    private Dictionary<string, System.Action<GameObject>> zombieDeathHandlers = new Dictionary<string, System.Action<GameObject>>();
    [SerializeField] private AudioManager _audioManager;
    private void Start()
    {       
        _audioManager = FindObjectOfType<AudioManager>();
        _update = FindObjectOfType<PrefabsZombieUpdate>();
        for (int i = 1; i <= 10; i++)
        {
            int level = i; // Создаем локальную переменную, чтобы захватить текущее значение i
            zombieDeathHandlers.Add("Zombie" + level, obj => _update.DieZombie(level, obj));
        }
    }
    private void Update()
    {
        if (_ischeck)
        {
            _trigercooldown -= Time.deltaTime;
            if (_trigercooldown < 0)
            {
                _ischeck = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
            string myTag = gameObject.tag;
            string otherTag = other.gameObject.tag;
            if (zombieDeathHandlers.ContainsKey(myTag) && myTag == otherTag)
            {
                _update.RemoveZombiePositionSO(this.GetComponent<Zombiee>());
                Destroy(other.gameObject);
                System.Action<GameObject> deathHandler = zombieDeathHandlers[myTag];
                deathHandler(gameObject);
                _audioManager.PlaySound(4);
            }
    }
}
