using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3 _offset;

    void Start()
    {
        if (_playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned in the CameraFollow script.");
        }
        transform.position = _playerTransform.position + _offset;
    }

    void LateUpdate()
    {
        Vector3 newPosition = _playerTransform.position + _offset;
        transform.position = newPosition;
    }
}
