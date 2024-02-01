using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBounds : MonoBehaviour
{
    private Camera camera;
    Vector3 bottomLeftWorld;
    Vector3 topRightWorld;

    [SerializeField] GameObject[] bodyParts;
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
        foreach (var part in bodyParts)
        {
            if (part.transform.position.x >= topRightWorld.x || part.transform.position.y <= bottomLeftWorld.y)
            {
                FreeFallEnemy flew = gameObject.GetComponent<FreeFallEnemy>();
                flew.flew = true;
            }
        }
    }
}
