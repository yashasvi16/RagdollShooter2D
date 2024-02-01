using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [SerializeField] float reSpawnTime;
    public GameObject[] _prefabs; 
    public bool isEnemyDead;
    int idx;
    bool flag = true;

    private IEnumerator spawnCoroutine;

    private Camera camera;

    Vector3 bottomLeftWorld;
    Vector3 topRightWorld;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        bottomLeftWorld = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRightWorld = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        idx = Random.Range(0, _prefabs.Length);
        float x = Random.Range(0, topRightWorld.x - 2f);
        float y = Random.Range(-topRightWorld.y + 3, topRightWorld.y - 0.5f);

        Instantiate(_prefabs[idx], new Vector3(x, y, 0), Quaternion.identity);

        isEnemyDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyDead)
        {
            isEnemyDead = false;
            StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(reSpawnTime);
        idx = Random.Range(0, _prefabs.Length);
        float x = Random.Range(0, topRightWorld.x - 2f);
        float y = Random.Range(-topRightWorld.y + 3, topRightWorld.y - 0.5f);
        
        Instantiate(_prefabs[idx], new Vector3(x, y, 0), Quaternion.identity);
        
    }
}
