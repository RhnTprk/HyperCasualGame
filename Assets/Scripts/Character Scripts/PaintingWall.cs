using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingWall : MonoBehaviour
{
    [SerializeField] private CollisionToObjects collisionToObjects;
    [SerializeField] private GameObject wall;
    [SerializeField] private Text txtPainting;
    private float paintCount = 0;
    
    void Start()
    {

    }
    
    //Painting wall. 
    void Update()
    {
        if (collisionToObjects.IsReadyToPaint)
        {
            wall.transform.localScale += new Vector3(0.005f, 0, 0);
            wall.transform.localPosition += new Vector3(-0.0025f, 0, 0);
            transform.position += new Vector3(-0.005f, 0, 0);
            paintCount += 0.0278f;
            txtPainting.text = "PAINTING % " + Convert.ToInt32(paintCount);
        }
    }
}
