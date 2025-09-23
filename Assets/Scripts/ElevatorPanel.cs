using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] private GameObject _callButton;
    Color _callButtonColor;
    [SerializeField] private int _requiredScore;
    private Elevator _elevator;

    void Start()
    {
        _callButtonColor = _callButton.GetComponent<Renderer>().material.color;

        if (_callButton == null)
        {
            Debug.LogError("Call button not assigned in ElevatorPanel.");
        }

        _elevator = FindObjectOfType<Elevator>();
        if (_elevator == null)
        {
            Debug.LogError("Elevator not assigned in ElevatorPanel.");
        }
    }
    void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("Player") && player.GetScore() >= _requiredScore)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _callButton.GetComponent<Renderer>().material.color = Color.green;
                _elevator.CheckForCall(true);
                Debug.Log("Elevator panel opened.");
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // _callButton.GetComponent<Renderer>().material.color = _callButtonColor;
            Debug.Log("Player exited elevator panel area.");
        }
    }
    
}