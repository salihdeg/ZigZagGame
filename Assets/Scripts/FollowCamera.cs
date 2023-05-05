using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerT;
    [SerializeField] private Vector3 _camDistance;

    private void Start()
    {
        _camDistance = transform.position - _playerT.position; // distance between camera and player
    }

    private void LateUpdate()
    {
        transform.position = _playerT.position + _camDistance;
    }

}
