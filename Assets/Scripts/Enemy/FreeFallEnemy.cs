using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFallEnemy : MonoBehaviour
{
    [SerializeField] EnemyHealth _health;
    [SerializeField] DetachBodyParts[] _instance;
    public GameObject[] bodyPartsHJ;
    public GameObject[] bodyPartsFJ;
    public GameObject platform;
    public bool detachButton = false;
    public bool flew = false;
    public Canvas _canva;

    bool Scoring = true;  
    bool SpawnerButton;

    // Start is called before the first frame update
    void Start()
    {
        SpawnerButton = true;

        _health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Scoring && _health.currentHealth <= 0)
        {
            Scoring = false;
            GameManager.Instance.score++;
            GameManager.Instance.audioButton = true;
        }

        foreach (var part in _instance)
        {
            if(part.hasHit || _health.currentHealth <= 0 || flew)
            {  
                _health.TakeDamage();
                _health.currentHealth = 0;
                detachButton = true;
                break;
            }
        }
        if(detachButton)
        {
            DetachParts();
        }
    }

    void DetachParts()
    {
        //foreach (var x in bodyPartsHJ)
        //{
        //    if(DetachBodyParts.instance.hasHit)
        //    {
        //        Debug.Log(x.name);
        //        x.GetComponent<HingeJoint2D>().enabled = false;
        //    }
            
        //}
        //foreach (var x in bodyPartsFJ)
        //{
        //    if(DetachBodyParts.instance.hasHit)
        //    {
        //        Debug.Log(x.name);
        //        x.GetComponent<FixedJoint2D>().enabled = false;
        //    }
            
        //}

        foreach (var item in _instance)
        {
            if(item.hasHit && item.gameObject.GetComponent<FixedJoint2D>() != null)
            {
                FixedJoint2D fj = item.gameObject.GetComponent<FixedJoint2D>();
                fj.enabled = false;
            }
        }

        StartCoroutine(Falling());

    }

    IEnumerator Falling()
    {
        yield return new WaitForSeconds(1f);
        _canva.enabled = false;

        platform.GetComponent<Rigidbody2D>().isKinematic = false;

        if (detachButton && SpawnerButton)
        {
            SpawnerButton = false;
            EnemySpawnManager.Instance.isEnemyDead = true;
        }
        
        Destroy(gameObject, 2.5f);

    }
}
