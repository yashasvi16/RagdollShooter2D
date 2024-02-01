using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    public Vector2 touchPosition;

    public Touch touch;
    public int phase = 0;

    public float stamina;
    public RectTransform staminaUI;
    bool staminaSwitch;

    public bool isTouchUI;
    public bool jumping;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);

        stamina = 100;
        staminaSwitch = true;

    }

    // Update is called once per frame
    void Update()
    {
        TakingInput();
        
        staminaUI.sizeDelta = new Vector2(stamina, staminaUI.sizeDelta.y);
    }

    void TakingInput()
    {

        if (Input.touchCount > 0 && stamina > 5 && !jumping)
        {
            if (staminaSwitch)
            {
                StopAllCoroutines();
                StartCoroutine(LowStamina());
                staminaSwitch = false;
            }

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                phase = 1;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                phase = 2;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                phase = 3;
            }
        }
        else
        {
            if (!staminaSwitch)
            {
                StopAllCoroutines();
                StartCoroutine(HighStamina());
                staminaSwitch = true;
            }
            phase = 0;
        }
    }

    IEnumerator LowStamina()
    {
        yield return new WaitForSeconds(0.25f);
        if (stamina > 1.125)
            stamina -= 0.75f;

        StartCoroutine(LowStamina());
    }

    IEnumerator HighStamina()
    {
        yield return new WaitForSeconds(1f);

        if (stamina <= 98)
            stamina += 3.5f;

        StartCoroutine(HighStamina());
    }
}
