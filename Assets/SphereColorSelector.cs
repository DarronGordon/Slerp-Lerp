using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereColorSelector : MonoBehaviour
{
    [SerializeField]MeshRenderer mr;

    void Start()
    {
        SetColor();
    }

    private void SetColor()
    {
        Color newCol = new Color(UnityEngine.Random.Range(0f,1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), 1f);

        mr.materials[0].color = newCol;
    }

}
