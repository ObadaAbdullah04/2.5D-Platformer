using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _speed = 1f;
    private Vector3 _target;
    private bool _isCalled = false;

    void Start()
    {
        transform.position = _startPosition;
        _target = _endPosition;
    }

    public void CheckForCall(bool isCalled)
    {
        _isCalled = isCalled;
        if (_isCalled)
        {
            ElevatorCalled();
        }
        else
        {
            ElevatorGoesUp();
        }
    }
    void FixedUpdate()
    {
        CheckForCall(_isCalled);
    }
    public void ElevatorCalled()
    {
        _target = _endPosition;
        transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speed);
    }
    public void ElevatorGoesUp()
    {
        _target = _startPosition;
        transform.position = Vector3.MoveTowards(transform.position, _target, Time.deltaTime * _speed);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            CheckForCall(false);
            other.transform.SetParent(transform);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
