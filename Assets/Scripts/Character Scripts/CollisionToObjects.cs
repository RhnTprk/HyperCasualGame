using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionToObjects : MonoBehaviour
{
    [SerializeField] private CurrentRanking currentRanking;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject startPointToPaint, paintText;

    private bool isCollision = false, paint = false;
    private bool rotatingPlatform, PlayerOrOpponent, halfDonut, leftHalfDonut, slidingPlatform, rotatorStick;
    private Transform stickTransform;

    void Start()
    {
        rotatingPlatform = PlayerOrOpponent = halfDonut = leftHalfDonut = slidingPlatform = rotatorStick = false;
    }

    void FixedUpdate()
    {
        if (rotatingPlatform)
            _rigidbody.AddForce(Vector3.left * 800f, ForceMode.Acceleration);

        if (PlayerOrOpponent)
            _rigidbody.AddForce(transform.forward * -1f, ForceMode.Acceleration);

        if (halfDonut)
            _rigidbody.AddForce(Vector3.left * 1000f, ForceMode.Acceleration);

        if (leftHalfDonut)
            _rigidbody.AddForce(Vector3.right * 1000f, ForceMode.Acceleration);

        if (slidingPlatform)
            _rigidbody.AddForce(Vector3.left * 700f, ForceMode.Acceleration);

        if (rotatorStick)
            _rigidbody.AddForce(stickTransform.right * 70f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                animator.SetBool("isDead", true);
                StartCoroutine(AnimationFinish(1.5f, true));
                break;
            case "RotatingPlatform":
                rotatingPlatform = true;
                break;
            case "Player":
            case "Opponent":
                isCollision = true;
                animator.SetBool("collision", true);
                PlayerOrOpponent = true;
                StartCoroutine(AnimationFinish(1.5f, false));
                break;
            case "HalfDonut":
                halfDonut = true;
                break;
            case "LeftHalfDonut":
                leftHalfDonut = true;
                break;
            case "SlidingPlatform":
                slidingPlatform = true;
                break;
            case "RotatorStick":
                stickTransform = collision.gameObject.transform;
                rotatorStick = true;
                break;
            case "FinishArea":
                if (currentRanking.FirstPlayer == "Boy")
                {
                    animator.SetBool("paint", true);
                    gameManager.isPaused = true;
                    paint = true;
                }
                else
                {
                    animator.SetBool("celebration", true);
                    return;
                }
              
                if (paintText != null)
                    paintText.SetActive(true);

                if (startPointToPaint != null)
                    transform.position = startPointToPaint.transform.position;
                break;
            case "FinishPaint":
                paint = false;
                animator.SetBool("paint", false);
                animator.SetBool("celebration", true);
                break;
            default:
                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "RotatingPlatform":
                rotatingPlatform = false;
                break;
            case "Player":
            case "Opponent":
                PlayerOrOpponent = false;
                break;
            case "HalfDonut":
                halfDonut = false;
                break;
            case "LeftHalfDonut":
                leftHalfDonut = false;
                break;
            case "SlidingPlatform":
                slidingPlatform = false;
                break;
            case "RotatorStick":
                rotatorStick = false;
                break;
            default:
                break;
        }
    }


    IEnumerator AnimationFinish(float time, bool isDead)
    {
        yield return new WaitForSeconds(time);

        if (isDead)
        {
            transform.position = new Vector3(Random.Range(-9.2f, 6), 0, Random.Range(-2, -20));
            animator.SetBool("isDead", false);
        }
        else
        {
            animator.SetBool("collision", false);
            isCollision = false;
        }
    }

    public bool IsCollision { get { return isCollision; } }
    public bool IsReadyToPaint { get { return paint; } }
}
