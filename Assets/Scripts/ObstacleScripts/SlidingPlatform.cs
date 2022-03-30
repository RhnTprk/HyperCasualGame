using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPlatform : MonoBehaviour
{
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (material.mainTextureScale.x <3)
            material.mainTextureScale = new Vector2(5.63f, 1);

        material.mainTextureScale -= new Vector2(0.1f, 0);
    }
}
