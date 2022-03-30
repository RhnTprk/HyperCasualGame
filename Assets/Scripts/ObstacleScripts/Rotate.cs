using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    void Update()
    {
        if(!gameManager.isPaused)
        {
            if (gameObject.tag == "RotatingPlatform")
                transform.Rotate(0, 0, 0.08f);
            else
                transform.Rotate(0, 0.3f, 0);
        }
    }
}
