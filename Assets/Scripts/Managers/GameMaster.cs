using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;
    public GameObject SpawnerObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.PauseGame();
        GameManager.Instance.OnEndIsNigh += Instance_OnEndIsNigh;
    }

    private void Instance_OnEndIsNigh()
    {
        SpawnerObject.GetComponent<Spawner>().SpawnCount = 0;
    }

    public void StartTheGameAlready()
    {
        GameManager.Instance.UnPauseGame();
        SpawnerObject.SetActive(true);
    }
}
