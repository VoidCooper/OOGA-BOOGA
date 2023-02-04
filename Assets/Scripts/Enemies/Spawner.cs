using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform TargetAround;

    public float MinDistance = 5;
    public float MaxDistance = 10;

    public GameObject[] PrefabsToSpawn;

    public float SpawnDelay = 1;
    public int SpawnCount = 2;
    public int SpawnIndex = 0;
    private float curTime = 0;

    private ObjectPool[] _pools;

    private void Awake()
    {
        if (PrefabsToSpawn == null || PrefabsToSpawn.Length == 0)
            gameObject.SetActive(false);

        _pools = new ObjectPool[PrefabsToSpawn.Length];

        for (int i = 0; i < PrefabsToSpawn.Length; i++)
        {
            GameObject item = PrefabsToSpawn[i];
            ObjectPool pool = gameObject.AddComponent<ObjectPool>();
            _pools[i] = pool;
            pool.PrefillObj = item;
        }
    }

    private void Start()
    {
        TargetAround = GlobalReferenceManager.Instance.Player.transform;
        GameManager.Instance.OnEndIsNigh += OneEndIsNigh;
    }

    private void OneEndIsNigh()
    {
        gameObject.BroadcastMessage("StartFleeing");
    }

    private void Update()
    {
        curTime += Time.deltaTime;

        if (curTime < SpawnDelay)
            return;

        curTime = 0;

        SpawnIndex++;
        if (SpawnIndex == PrefabsToSpawn.Length)
            SpawnIndex = 0;

        Spawn();
    }

    public void Spawn()
    {

        for (int i = 0; i < SpawnCount; i++)
        {
            Vector3 randomPos = new Vector3(Random.value * 2 - 1, 0, Random.value * 2 - 1).normalized;
            Vector3 target = randomPos * Mathf.Lerp(MinDistance, MaxDistance, Random.value) + TargetAround.position;
            target.y = PrefabsToSpawn[SpawnIndex].transform.position.y;
            GameObject go = _pools[SpawnIndex].Take();
            go.SetActive(true);
            go.transform.position = target;
            go.SendMessage("SpwanInit");
        }
    }
}
