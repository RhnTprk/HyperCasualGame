using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAI : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CollisionToObjects collisionToObjects;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject mapCenterPoint;

    private bool isCollisionToFloor = true, jump = false;
    private float horizontal, vertical;

    void Start()
    {

    }
    //Opponent must look at to finish area when running. If game begin, all players begin to run and if game is paused , all players stop.
    void Update()
    {
        transform.LookAt(gameManager.gameObject.transform);

        if (gameManager.isBeginGame)
            animator.SetBool("isBegin", true);

        if (gameManager.isPaused) 
            _rigidbody.velocity = Vector3.zero;

    }
    //Rigidbody functions must be in FixedUpdate function.
    void FixedUpdate()
    {
        if (gameManager.isBeginGame && !gameManager.isPaused && isCollisionToFloor && !collisionToObjects.IsCollision && jump)
        {
            _rigidbody.AddForce(Vector3.up * 370);
            isCollisionToFloor = false;
        }

        if (gameManager.isBeginGame && !gameManager.isPaused && !animator.GetBool("isDead") && !collisionToObjects.IsCollision)
        {
            _rigidbody.AddForce(transform.forward * 600);
            _rigidbody.velocity = new Vector3(horizontal, _rigidbody.velocity.y, vertical);
        }
        else if (animator.GetBool("isDead"))
        {
            _rigidbody.AddForce(Vector3.back * 6f);
        }
    }
    // If opponent collide to rotating platform, must move right. If collide to slidingPlatform, must move right again.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor") // change isCollisionToFloor value for jumping. If it false, opponent can't jump Because opponent doesnt touch to floor
            isCollisionToFloor = true;

        if (collision.gameObject.tag == "RotatingPlatform")
        {
            isCollisionToFloor = true;
            horizontal = 13.5f;
        }

        if (collision.gameObject.tag == "SlidingPlatform")
        {
            isCollisionToFloor = true;
            horizontal = 5;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isCollisionToFloor = false;

        if (collision.gameObject.tag == "RotatingPlatform" || collision.gameObject.tag == "SlidingPlatform")
        {
            isCollisionToFloor = false;
            horizontal = 0;
        }
    }
    // This is for box collider. This is like opponent's field of view. If any object enters the field of view, opponent will move left or right.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishArea")
        {
            animator.SetBool("celebration", true);
        }


        if (other.gameObject.tag == "Obstacle")
        {
            if (mapCenterPoint.transform.position.x < other.gameObject.transform.position.x)
                horizontal = -10;
            else
                horizontal = 10;
        }

        if (other.gameObject.tag == "RotatorStick")
            jump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
            horizontal = 0;

        if (other.gameObject.tag == "RotatorStick")
            jump = false;
    }
}
