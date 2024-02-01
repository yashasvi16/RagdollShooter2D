using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{
    public GameObject _prefab;
    public float minRange, maxRange;

    float reSpawnTime;
    bool flag = true;

    private Camera camera;
    Vector3 bottomLeftWorld;
    Vector3 topRightWorld;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        bottomLeftWorld = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRightWorld = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.score >= 15 && flag)
        {
            flag = false;
            StartCoroutine(FireCannon());
        }
    }

    IEnumerator FireCannon()
    {
        Instantiate(_prefab, new Vector2(topRightWorld.x, bottomLeftWorld.y), _prefab.transform.rotation);

        reSpawnTime = Random.Range(minRange, maxRange);
        yield return new WaitForSeconds(reSpawnTime);

        StartCoroutine(FireCannon());
    }
}
