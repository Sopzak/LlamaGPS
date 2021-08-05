using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAiInput : MonoBehaviour, IInput
{
    public Action<Vector2> OnMovementInput { get; set; }
    public Action<Vector3> OnMovementDirectionInput { get; set;}

    bool playerDetectionResult =false;
    public Transform eyesTransform;
    public Transform playerTransform;
    public LayerMask playerLayer;
    public float visionDistance, stoppingDistance = 1.2f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetMovementInput();
        //GetMovementDirection();
        playerDetectionResult = DetectPlayer();
        if(playerDetectionResult){
            var directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer = Vector3.Scale(directionToPlayer, Vector3.forward + Vector3.right);
            if(directionToPlayer.magnitude > stoppingDistance){
                directionToPlayer.Normalize();
                OnMovementInput?.Invoke(Vector2.up);
                OnMovementDirectionInput?.Invoke(directionToPlayer);
                return;
            }
        }
        OnMovementInput?.Invoke(Vector2.zero);
        OnMovementDirectionInput?.Invoke(transform.forward);
    }

    private bool DetectPlayer(){
        Collider[] hitColliders = Physics.OverlapSphere(eyesTransform.position, visionDistance, playerLayer);
        foreach (var collider in hitColliders)
        {
            playerTransform = collider.transform;
            return true;
        }
        playerTransform = null;
        return false;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        if(playerDetectionResult){
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(eyesTransform.position, visionDistance);
    }

    private void GetMovementInput(){
        var cameraForewardDirection = Camera.main.transform.forward;
        //Debug.DrawRay(Camera.main.transform.position,cameraForewardDirection * 10, Color.red);
        var directionToMoveIn = Vector3.Scale(cameraForewardDirection, (Vector3.right + Vector3.forward));
        //Debug.DrawRay(Camera.main.transform.position,directionToMoveIn * 10, Color.blue);
        OnMovementDirectionInput?.Invoke(directionToMoveIn);
    }

    private void GetMovementDirection(){
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMovementInput?.Invoke(input);
    }
}
