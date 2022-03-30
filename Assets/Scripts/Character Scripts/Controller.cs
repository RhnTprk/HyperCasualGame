using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Variables    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CollisionToObjects collisionToObjects;
    [SerializeField] private SwipeController swipeControls;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator animator;

    private float horizontal, vertical;
    private bool keyInputFlag = false, isCollisionToFloor = true;
    #endregion

    void Update()
    {
        if (gameManager.isBeginGame)
            animator.SetBool("isBegin", true);

        if (gameManager.isPaused)      
            _rigidbody.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow))
        {
            keyInputFlag = true;
        }
        else
        {
            keyInputFlag = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
            swipeControls.Jump = true;
    }
    void FixedUpdate()
    {
        if (keyInputFlag)
        {
            horizontal = Input.GetAxis("Horizontal") * 10;
        }
        else
        {
            horizontal = swipeControls.SwipeDelta.x / 25;
        }

        if (gameManager.isBeginGame && !gameManager.isPaused && swipeControls.Jump && isCollisionToFloor && !collisionToObjects.IsCollision)
        {
            swipeControls.Jump = false;

            _rigidbody.AddForce(Vector3.up * 300);
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

    #region CollisionFunctions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "RotatingPlatform")
            isCollisionToFloor = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "RotatingPlatform")
            isCollisionToFloor = false;
    }
    #endregion
}
