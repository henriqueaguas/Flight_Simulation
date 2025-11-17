using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoBehaviour
{
    public float RotateSpeed = 0f;

    public static RotateSky instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        if(RotateSpeed > 0f)
        {
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
        }
        
    }
}
