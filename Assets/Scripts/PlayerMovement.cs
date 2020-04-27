using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float moveSpeed = 100f;
    public float dashSpeed = 200f;
    public float DashTime;
    private float time;
    public Rigidbody2D rb;
    public Animator an;
    Vector2 Dir;
    Vector2 DashDir;
    public enum State
    {
        Normal,
        Rolling,
        Pause,
    }

    public State moveState = State.Normal;
    // Update is called once per frame



    public void InputUpdate()
    {
        if (moveState != State.Pause) {
        Dir.x = Input.GetAxisRaw("Horizontal");
        Dir.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
            Dash();
        if (moveState == State.Rolling)
            Dashing();
        }
    }

    /// <summary>
    /// Start Dash action if normal
    /// </summary>
    void Dash()
    {
        if (moveState == State.Normal)
        {
            time = Time.time;
            moveState = State.Rolling;
            DashDir = Dir;
            an.SetBool("Dashing", true);
        }
        
    }
    /// <summary>
    /// Processes legnth of roll and when to end
    /// </summary>
    void Dashing()
    {
            if (time + DashTime < Time.time)
            {
                moveState = State.Normal;
                an.SetBool("Dashing", false);
            }
    }
    //Fixed Update not linked to framerate
    public void PhysicsUpdate()
    {
        //Movements 

            updateAnimationVariables();
            movePlayer();
            flipForDir();
        
    }
    /// <summary>
    ///Passes movement to Animator
    /// </summary>
    void updateAnimationVariables()
    {
        an.SetFloat("Horizontal", rb.velocity.x);
        an.SetFloat("Vertical", rb.velocity.y);
        an.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }
    /// <summary>
    /// Perfroms player movement based on state
    /// </summary>
    void movePlayer()
    {
        switch (moveState)
        {
            case State.Normal:
                rb.velocity = (Dir.normalized * moveSpeed * Time.fixedDeltaTime);
                break;
            case State.Rolling:
                rb.velocity = (DashDir.normalized * dashSpeed * Time.fixedDeltaTime);
                break;
            case State.Pause:
                rb.velocity = Vector2.zero;
                break;
        }
    }
    /// <summary>
    /// Flips The player sprite based on left or right
    void flipForDir()
    {
        //Flip to side
        if (Dir.x < 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }
}
