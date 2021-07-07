using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCanvasManager : MonoBehaviour
{
    public Canvas main;
    void Start()
    {
        main = GetComponent<Canvas>();
        main.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.rotation = Quaternion.identity;
    }
}
