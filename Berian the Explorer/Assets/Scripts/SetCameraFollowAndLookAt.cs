using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SetCameraFollowAndLookAt : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;
    void Start()
    {
        cinemachineVirtual.LookAt = GameObject.Find("LookAt").transform;
        cinemachineVirtual.Follow = GameObject.Find("Player").transform;

    }
}
