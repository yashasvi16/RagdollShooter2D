using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform shootingSpot;
    [SerializeField] float force;
    [SerializeField] float timeGap;

    public bool shooting;

    Animator _animator;
    bool flag = false;

    private void Awake()
    {

    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(ShootArrow());
    }

    private void Update()
    {
        if(GameManager.Instance.score % 45 == 0 && flag)
        {
            flag = false;
            timeGap -= 0.5f;
        }
        else if(GameManager.Instance.score % 45 != 0)
        {
            flag = true;
        }
    }

    IEnumerator ShootArrow()
    {
        GameObject arrow = Instantiate(_arrowPrefab, shootingSpot.position, Quaternion.identity);
        arrow.transform.SetParent(transform);

        arrow.transform.rotation = transform.rotation;
        shooting = false;

        _animator.SetTrigger("bowHold");

        yield return new WaitForSeconds(timeGap);

        arrow.GetComponent<Rigidbody2D>().isKinematic = false;
        arrow.GetComponent<Rigidbody2D>().velocity = transform.right * force;

        shooting = true;

        _animator.SetTrigger("bowFree");

        yield return new WaitForSeconds(timeGap);

        StartCoroutine(ShootArrow());
    }
}
