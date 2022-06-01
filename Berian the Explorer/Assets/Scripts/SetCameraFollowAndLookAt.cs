using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SetCameraFollowAndLookAt : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtual;

    #endregion

    #region MonoBehaviour Callbacks
    void Start()
    {
        cinemachineVirtual.LookAt = GameObject.Find("LookAt").transform;
        cinemachineVirtual.Follow = GameObject.Find("Player").transform;

    }

    #endregion
}
