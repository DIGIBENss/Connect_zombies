
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using Color = UnityEngine.Color;

public class PrefabsZombieUpdate : MonoBehaviour
{
    [Header("Mesh_Zombie")]
    public Zombiee [] Mesh_Zombie;
    [Header("Zombie")]
    public GameObject[] Zombie;
    [Header("Spanwer")]
    [SerializeField] private Transform[] SpawnerZombie;
    [SerializeField] private int currentSpawnPointCount;
    public bool _isSpawner;
    [Header("SaveLocal")]
    public List<Zombiee> ZombieeV = new();
    [Header("Links")]

    [SerializeField] private Task _task;
    public void SaveZombiePositionSO(Zombiee zombieToSave)
    {
        ZombieeV.Add(zombieToSave); 
    }
   private IEnumerator Start()
   {
       yield return new WaitForSeconds(0.15f);
        Debug.Log("Start");
       SpawnSavedZombiesSO();
   }
    private void Update()
    {
        float c = 0.5f;
        if (_isSpawner)
        {
            c = -Time.deltaTime;
            if (c < 0.0f)
            {
                _isSpawner = false;
            }
        }
    }
    public void CheckZombiesDragging(bool dragItem, int zombieType)
    {
        foreach (Transform spawner in SpawnerZombie) 
        {
            if (spawner.childCount > 0) 
            {
                Zombiee zombieComponent = spawner.GetChild(0).GetComponent<Zombiee>();
                if (zombieComponent != null)
                    if (dragItem && zombieComponent.Type == zombieType) GridGreen(spawner);
                    else GridWhite(spawner);  
            }
        }
    }
    public void GridGreen(Transform spawner)
    {
        Renderer spawnerRenderer = spawner.GetComponent<Renderer>();
        if (spawnerRenderer != null)
        {
            spawnerRenderer.material.color = Color.green;
            spawnerRenderer.material.EnableKeyword("_EMISSION");
            spawnerRenderer.material.SetColor("_EmissionColor", Color.green);
        }
    }

    public void GridWhite(Transform spawner)
    {
        Renderer spawnerRenderer = spawner.GetComponent<Renderer>();
        if (spawnerRenderer != null)
        {
            spawnerRenderer.material.color = Color.white;
            spawnerRenderer.material.EnableKeyword("_EMISSION");
            spawnerRenderer.material.SetColor("_EmissionColor", Color.white);
        }
    }
    public void SpawnSavedZombiesSO()
    {
        for (int j = 0; j < ZombieeV.Count; j++)
        {
            if (SpawnerZombie[j].childCount == 0)
            {
                    GameObject zombie = Instantiate(Mesh_Zombie[ZombieeV[j].Type].gameObject, SpawnerZombie[j].position, Quaternion.identity);
                    zombie.GetComponent<Zombiee>().customID = ZombieeV[j].customID;
                    zombie.transform.parent = SpawnerZombie[j];
            }
        }
    }
    public bool TrySpawnZombie()
    {
        int index = 0;
        foreach (Transform item in SpawnerZombie)
        {
          
            if (item.childCount == 0 )
            {
                SpawnZombieAt(index);
                return true;
            }
            index++;
        }
        return false;
    }
    private void SpawnZombieAt(int index)
    {
        Transform spawnPoint = SpawnerZombie[index].transform;
        Vector3 spawnPosition = spawnPoint.position;
        Quaternion spawnRotation = spawnPoint.rotation;
        var zombie = Instantiate(Mesh_Zombie[0], spawnPosition, spawnRotation);
        zombie.transform.parent = spawnPoint;
        zombie.CustomId();
        SaveZombiePositionSO(Mesh_Zombie[0]);
    }
    public void ReplaceZombies()
    {
        for (int i = 0; i < Mesh_Zombie.Length; i++)
        {   
            GameObject[] zombies = GameObject.FindGameObjectsWithTag(Mesh_Zombie[i].tag);
            foreach (GameObject zombie in zombies)
            {
                Vector3 spawnPosition = zombie.transform.position;
                Quaternion spawnRotation = zombie.transform.rotation;
                Destroy(zombie);
                Instantiate(Zombie[i], spawnPosition, spawnRotation);
            }
        }
    }
    
    public void DieZombie(int zombieType, GameObject zombie)
    {
        if (!_isSpawner) 
        {
            _task.UpdateConnectZombies(2);
            Transform parent = zombie.transform.parent;
            int index = 0;
            foreach (var item in SpawnerZombie)
            {
                if (item.name == parent.name)
                { 
                    break;
                }
                index++;
            }
            Vector3 spawnPosition = parent.position;
            var newZombie = Instantiate(Mesh_Zombie[zombieType], spawnPosition, Quaternion.identity);
            newZombie.CustomId();
            newZombie.transform.parent = parent;
            SaveZombiePositionSO(Mesh_Zombie[zombieType]);
            Destroy(zombie);
            _isSpawner = true;   
        }
    }
    public void RemoveZombiePositionSO(Zombiee position)
    {
        int index = 0;
        foreach (Zombiee item in ZombieeV)
        {
            if (item.Type == position.Type)
            {
                ZombieeV.Remove(item);
                break;
            }
            index++;
        }
    }
}

