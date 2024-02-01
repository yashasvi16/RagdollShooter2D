using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingApple : MonoBehaviour
{
    public GameObject[] _prefabs;
    public float minRange, maxRange;
    float reSpawnTime;
    int idx;

    private Camera camera;
    Vector3 bottomLeftWorld;
    Vector3 topRightWorld;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        bottomLeftWorld = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRightWorld = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        StartCoroutine(PopApple());
    }

    IEnumerator PopApple()
    {
        reSpawnTime = Random.Range(minRange, maxRange);
        yield return new WaitForSeconds(reSpawnTime);

        idx = Random.Range(0, _prefabs.Length);
        float x = Random.Range(-2.75f, 5f);
        float y = bottomLeftWorld.y - Random.Range(2f,2.5f);

        GameObject apple = Instantiate(_prefabs[idx], new Vector2(x, y), Quaternion.identity);

        Rigidbody2D rb = apple.gameObject.GetComponent<Rigidbody2D>();
        float upwardForce = Random.Range(50, 80);
        rb.AddForce(Vector2.up * upwardForce);
        apple.transform.RotateAround(apple.transform.position, Vector3.forward, upwardForce);

        Destroy(apple.gameObject, 10f);

        StartCoroutine(PopApple());
        
    }
}
