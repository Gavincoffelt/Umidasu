using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCPlayerController : MonoBehaviour
{
    #region
    public static GCPlayerController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public float TurnSmoothTime = 0.2f;
    private float turnSmoothVel;
    private float speedSmoothVel;

    public float SpeedSmoothTime = 0.1f;
    public float curSpeed;

    public float Speed;
    public float DashInterval = 1.0f;
    public float CurInterval;
    public float DashSpeed = 30.0f;

    private Vector3 pos;
    private float ySpeed = 0;
    private float gravity = 9.8f;

    private Animator animator;
    private CharacterController cc;
    private float animspeed = 0;
    void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out cc);
        CurInterval = DashInterval;

    }

    void Update()
    {
        if (CurInterval > 0) { CurInterval -= Time.deltaTime; } // Dash Interval Time.

        // Calculating The input of the player.
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = playerInput.normalized;

        // if the input Direction isnt zero then calculate the target angle and smooth the angle between the two.
        if (inputDir != Vector2.zero)
        {
            float targetAngle = (Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg) + Camera.main.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, TurnSmoothTime);
        }

        float targetSpeed = (Speed) * inputDir.magnitude; // Targetspeed calculation.

        // If Space key is pressed then initiate Dash and Dash animation.
        if (Input.GetKeyDown(KeyCode.Space) && CurInterval <= 0)
        {
            animator.SetTrigger("Dash"); //trigger Dash
            curSpeed = curSpeed * DashSpeed;
            CurInterval = DashInterval;

        }
        else { curSpeed = Mathf.SmoothDamp(curSpeed, targetSpeed, ref speedSmoothVel, SpeedSmoothTime); }
        if (cc.isGrounded) { ySpeed = 0; } // If it is grounded, no gravity;

        // If the character is Animating the idle-run OR is dashing.
          ySpeed -= gravity * Time.deltaTime;

            pos.x = transform.forward.x;
            pos.y = ySpeed;
            pos.z = transform.forward.z;
            cc.Move(pos * curSpeed * Time.deltaTime);

        if(curSpeed > 4 && animspeed < 1) { animspeed += .01f; }
        else if (animspeed > 0){ animspeed -= .01f; }
        animator.SetFloat("speed", animspeed);
        Debug.Log(animspeed);
    }

}
