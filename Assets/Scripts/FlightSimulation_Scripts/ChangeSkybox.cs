using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{

    [SerializeField] private Material _skybox;
    [SerializeField] private GameObject _clouds;
    [SerializeField] private GameObject _airport;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _enableAirport;


    private void Start()
    {
        // if(_clouds == null)
        // {
        //    _clouds = GameObject.Find("Clouds");
        // }
        // if (_airport)
        // {
        //    _airport = GameObject.Find("Airport");
        // }  
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        RenderSettings.skybox = _skybox;
        RotateSky.instance.RotateSpeed = _rotationSpeed;
        _airport.SetActive(_enableAirport);
    }

    private void OnTriggerExit(Collider other)
    {   
        if (_clouds != null) _clouds.SetActive(false);
        
    }
}