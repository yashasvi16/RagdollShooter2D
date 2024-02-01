using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject _ballPrefab;
    public Transform _spawnPos;
    public float force;

    bool flag;
    private Camera camera;
    Vector3 bottomLeftWorld;
    Vector3 topRightWorld;

    private void Start()
    {
        camera = Camera.main;
        bottomLeftWorld = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        topRightWorld = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

        flag = true;

        StartCoroutine(ShootBall());
    }

    private void Update()
    {
        if(flag)
        {
            transform.Translate(Vector2.up * 0.75f * Time.deltaTime, Space.World);
            if(Vector2.Distance(transform.position, topRightWorld) <= 0.5f)
            {
                flag = false;
            }
        }
        else
        {
            Vector2 pos = new Vector2(topRightWorld.x, bottomLeftWorld.y);
            transform.Translate(-1 * Vector2.up * 0.5f * Time.deltaTime, Space.World);

            if(Vector2.Distance(transform.position, pos) <= 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ShootBall()
    {
        GameObject ball = Instantiate(_ballPrefab, _spawnPos.position, Quaternion.identity);

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = -1 * Vector2.right * force;

        yield return new WaitForSeconds(2.5f);
        
        StartCoroutine(ShootBall());
    }
}
