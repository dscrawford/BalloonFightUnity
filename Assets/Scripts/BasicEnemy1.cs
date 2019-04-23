using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicEnemy1 : MonoBehaviour
{
    public Animator anim;

    public float EnemySpeedMAX = .5f;
    public float EnemySpeedMIN = .2f;
    public float EnemyCurrentSpeed = .31f;
    public float speedIncrement = 0.01f;
    private bool facingRight = false;
    Vector2 lastPos;
    int movePath = 0 ;
    int moveCounter = 1;
    System.Random rand = new System.Random();
    int randChoice;
    bool isBlowingUpBalloon = true;
    bool isChasing = false;
    public int enemyJumpPower = 20;
    private int currentHP = 2;
    public int bounciness = 1;
    private bool isTouchingGround;
    private bool justCollided;
    private bool isKillable;
    private int countCollider;
    public float deathPos = -1;
    public bool dead;
    private const float invincibleTime = 0.4f;
    private float currInvincible = 0;
    private bool invincible;



    public bool getFacingRight()
    {
        return facingRight;
    }

    //The actual player
    public Transform target;

    void Invincible()
    {
        if (currInvincible > 0)
            currInvincible -= Time.deltaTime;
        else
            invincible = false;
    }

    void makeInvincible()
    {
        currInvincible = invincibleTime;
        invincible = true;
    }

    void randJump()
    {

    }

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //SET THE PLAYER'S TAG TO "Player" IN THE INSPECTOR FOR THIS TO WORK.
        lastPos = gameObject.GetComponent<Rigidbody2D>().position;
        isTouchingGround = true;
        justCollided = false;
        isKillable = false;
        dead = false;
        invincible = false;
        countCollider = 0;
    }
    void Update()
    {
        Invincible();
        checkGotHit();
        //SPEED
        //increase enemy speed if it's farther away from the player.
        if (Vector2.Distance(transform.position, target.position) > .13)
        {
            if (EnemyCurrentSpeed < EnemySpeedMAX)
                EnemyCurrentSpeed += speedIncrement;
        }
        else
        {
            if (EnemyCurrentSpeed > EnemySpeedMIN)
                EnemyCurrentSpeed -= speedIncrement;
        }

        //ANIMATION
        if (lastPos.y < gameObject.GetComponent<Rigidbody2D>().position.y)
        {
            anim.SetBool("Ground", false);
            anim.SetBool("Ascending", true);
            
            isTouchingGround = false;
        }
        else if (lastPos.y >= gameObject.GetComponent<Rigidbody2D>().position.y)
            anim.SetBool("Ascending", false);

        //ENEMY FACING DIRECTION
        SwitchedDir();
        lastPos = gameObject.GetComponent<Rigidbody2D>().position; //for direction purposes



        if(transform.position.y < deathPos)
        {
            this.gameObject.SetActive(false);
        }


        //PHYSICS
        if(dead)
        {
            
            if (transform.position.y > deathPos)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1f);
            }
            else
                this.gameObject.SetActive(false);
        }
        else if (movePath != 0 && !isBlowingUpBalloon)
        {

            if (Vector2.Distance(transform.position, target.position) >= .7 && !isChasing)
            {

                if (movePath == 1) // idle
                {

                }
                else if (movePath == 2) //move right
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * EnemyCurrentSpeed, 0);
                }
                else if (movePath == 3) //move left
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * EnemyCurrentSpeed, 0);
                }

                //prevent falling off the map
                if(gameObject.GetComponent<Rigidbody2D>().position.y < -.78)
                {
                    //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 1);
                    GetComponent<Rigidbody2D>().AddForce(Vector2.up * enemyJumpPower);
                }
            }
            else if(justCollided)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;                
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -.27f);

                if (isTouchingGround)
                {
                    justCollided = false;
                    anim.SetInteger("Hit", 0);
                    anim.SetBool("Ground", true);
                    anim.Play("e_pump");
                }
                
            }
            else if(isTouchingGround)
            {
                
                //Debug.Log(countCollider);
                if (++countCollider > 100)
                {
                    isTouchingGround = false;
                    isKillable = false;
                    countCollider = 0;
                }
            }
            else if(!isKillable)
            {
                isChasing = true;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                
                if (Vector2.Distance(transform.position, target.position) > .7) //move towards character
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, EnemyCurrentSpeed * Time.deltaTime);

                }
                else if (Vector2.Distance(transform.position, target.position) < .7) //move towards the balloons
                {
                    Vector2 targetDest = new Vector2(target.position.x, target.position.y);
                    targetDest.y += 0.25f;
                    transform.position = Vector2.MoveTowards(transform.position, targetDest, EnemyCurrentSpeed * Time.deltaTime);
                }
                else if (Vector2.Distance(transform.position, target.position) < .3)
                {

                }

                if (!isTouchingGround && !justCollided)
                {
                    isKillable = false;
                    anim.SetInteger("Hit", 0);
                    //Debug.Log("NO LONGER KILLABLE");
                }

            }
        }

        updateMovePath();

    }

    void updateMovePath()
    {
        //UPDATE MOVEPATH
        if (moveCounter % 100 == 0)
        {

            isBlowingUpBalloon = false;

            randChoice = rand.Next(101); //Chooses a # between 0 and 100.
            //Debug.Log(randChoice);
            if (randChoice > 90) //10% chance
            {
                movePath = 4;
            }
            else if (randChoice > 60) //30% chance
            {
                movePath = 3;
            }
            else if (randChoice > 30) //30%
            {
                movePath = 2;
            }
            else //31% (0-30)
            {
                movePath = 1;
            }
            //Debug.Log(movePath);
        }

        moveCounter++;
    }

    void SwitchedDir()
    {
        if ( (lastPos.x < gameObject.GetComponent<Rigidbody2D>().position.x && !facingRight) ||
             (lastPos.x > gameObject.GetComponent<Rigidbody2D>().position.x && facingRight))
            FlipPlayer();
        
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void ChangePosition(Vector3 pos)
    {
        //this.transform.position = pos;
    }

    public void ChangeVelocity(Vector3 pos)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = pos;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player_Move>() != null)
            {
                if(justCollided || isKillable) //if he was just hit
                {

                    anim.SetInteger("Hit", 2);
                    anim.Play("e_falling");
                    collision.gameObject.GetComponent<Player_Move>().ChangeVelocity(new Vector3(-collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounciness, 0));
                    Death();
                    return;
                }

                collision.gameObject.GetComponent<Player_Move>().ChangeVelocity(new Vector3(-collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounciness, 0));
                
                justCollided = true;
                isKillable = true;
                
                anim.SetBool("Ascending", false);
                anim.SetBool("Ground", false);
                anim.SetInteger("Hit", 1);

            }
        }
        */
        if (collision.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
            if (isKillable)
            {
                anim.SetInteger("Hit", 0);
                anim.SetBool("Ground", true);
                
            }
                
        }
    }

    private void checkGotHit()
    {
        GameObject hit = target.GetComponent<Player_Move>().isOnEnemy();
        if (hit != null && !invincible)
        {
            if (hit == this.gameObject)
            {
                makeInvincible();
                Debug.Log(invincible);
                Transform collision = target;
                if (collision.gameObject.GetComponent<Player_Move>() != null)
                {
                    collision.gameObject.GetComponent<Player_Move>().ChangeVelocity(new Vector3(-collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounciness, 0));

                    if (justCollided || isKillable) //if he was just hit
                    {
                        anim.SetInteger("Hit", 2);
                        anim.Play("e_falling");
                        Death();
                        return;
                    }



                    justCollided = true;
                    isKillable = true;

                    anim.SetBool("Ascending", false);
                    anim.SetBool("Ground", false);
                    anim.SetInteger("Hit", 1);

                }
            }
        }
        
    }

    private bool isOnPlayer()
    {
        float modelWidth = GetComponent<BoxCollider2D>().bounds.max.x - GetComponent<BoxCollider2D>().bounds.min.x;
        Debug.DrawRay(new Vector3(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y - 0.03f, 0), Vector3.right * modelWidth);

        RaycastHit2D right = Physics2D.Raycast(new Vector2(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y - 0.03f), Vector2.right, modelWidth);
        RaycastHit2D left = Physics2D.Raycast(new Vector2(GetComponent<BoxCollider2D>().bounds.min.x, GetComponent<BoxCollider2D>().bounds.min.y - 0.03f), Vector2.left, modelWidth);

        //Check if the right collider hit something
        if (right.collider != null)
            if (right.collider.tag == "Player")
                return true;

            //Check if the left collider hit something
            else if (left.collider != null)
                return left.collider.tag == "Player";

        //If both null, then no collision.
        return false;
    }

    private void hitPlayer()
    {
        if (isOnPlayer())
        {
            target.gameObject.GetComponent<Player_Move>().DecrementPlayerHealth();
        }
    }

    private void Death()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + .2f), EnemyCurrentSpeed);

        dead = true;
    }
}
