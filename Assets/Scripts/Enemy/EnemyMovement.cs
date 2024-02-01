using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject m_EnemyShooter;
    [SerializeField] Transform bow;

    Rigidbody2D rb;

    Vector2 AimingPosition;
    Vector2 direction;

    [SerializeField] int speed = 30;
    [SerializeField] int frequency = 5;
    public string handLeftORRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(EnemyAim());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandMovement();
    }
    void HandMovement()
    {
        if (handLeftORRight == "Right")
        {
            if (!m_EnemyShooter.GetComponent<EnemyShooter>().shooting)
                rb.MovePosition(bow.position);
        }
        else if (handLeftORRight == "Left")
        {
            Vector2 handPosition = transform.position;
            direction = AimingPosition - handPosition;
            float rotationZ = Mathf.Atan2(direction.x, -1 * direction.y) * Mathf.Rad2Deg;

            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed * Time.fixedDeltaTime));

        }

    }

    IEnumerator EnemyAim()
    {
        AimingPosition.x = -1 * Screen.width;
        AimingPosition.y = Random.Range(-1 * Screen.height, Screen.height);

        yield return new WaitForSeconds(frequency);
        StartCoroutine(EnemyAim());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, direction);
    }
}
