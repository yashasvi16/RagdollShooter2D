using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBow : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform shootingSpot;
    [SerializeField] float force;
    private AudioSource _audio;

    Vector2 touchPosition;
    Vector2 direction;

    GameObject arrow;
    Animator _animator;

    public int phase = 0;
    public bool shooting = false;

    bool canSpawn;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();

        canSpawn = true;
    }
    // Update is called once per frame
    void Update()
    {  
        if(PlayerInput.instance.stamina > 10)
        {
            TakingInput();
            BowAnimation();

        }
    }

    void TakingInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = PlayerInput.instance.touch;
            touchPosition = PlayerInput.instance.touchPosition;
            phase = PlayerInput.instance.phase;
        }
    }
    void BowAnimation()
    {
        if (phase == 1 && canSpawn)
        {
            SpawnArrow();

            _animator.SetTrigger("bowHold");
            canSpawn = false;
        }
        else if(phase == 2 && !canSpawn)
        {
            if(arrow != null)
                ArrowAim();
        }
        else if (phase == 3 && !canSpawn)
        {
            if(arrow != null)
            {
                ShootArrow();

                _animator.SetTrigger("bowFree");
            }

            canSpawn = true;
        }
    }

    void SpawnArrow()
    {
        arrow = Instantiate(_arrowPrefab, shootingSpot.position, Quaternion.identity);
    }

    void ArrowAim()
    {
        arrow.transform.position = transform.position;
        arrow.transform.rotation = transform.rotation;
    }
    void ShootArrow()
    {
        Vector2 bow = arrow.transform.position;
        direction = bow - touchPosition;
        arrow.GetComponent<Rigidbody2D>().isKinematic = false;
        arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.right * force;
        shooting = true;

        _audio.Play();
    }
}
