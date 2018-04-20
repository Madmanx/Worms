using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {


    private static CameraManager singleton;
    public static CameraManager Instance
    {
        get
        {
            return singleton;
        }
    }

    public void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void MainCameraFollow(Transform transformToFollow)
    {
        GetComponent<CinemachineVirtualCamera>().Follow = transformToFollow;
    }
}
