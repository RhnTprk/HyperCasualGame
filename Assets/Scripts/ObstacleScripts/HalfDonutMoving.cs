using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutMoving : MonoBehaviour
{
    private bool goForward;
    void Update()
    {
        if (transform.localPosition.x >= 0.15f)
            goForward = true;

        if (transform.localPosition.x < -0.12f)
            goForward = false;
    }

    private void FixedUpdate()
    {
        if (goForward)
            transform.localPosition += new Vector3(-0.009f, 0, 0);
        else
            transform.localPosition += new Vector3(0.009f, 0, 0);           
    }
}
