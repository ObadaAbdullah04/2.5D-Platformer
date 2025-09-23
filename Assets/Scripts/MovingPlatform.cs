using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private float _speed = 1f;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = _startPosition;
        target = _endPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Vector3.Distance(transform.position, _endPosition) < 0.1f)
        {
            target = _startPosition;
        }
        else if (Vector3.Distance(transform.position, _startPosition) < 0.1f)
        {
            target = _endPosition;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
