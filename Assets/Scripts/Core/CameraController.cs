using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private bool triggerMode = false;
    private float lookAhead;

    private void Update()
    {
       
        
        
        if(Input.GetKey("v"))
        {
            if (triggerMode == false)
                triggerMode = true;
            else
                triggerMode = false;
        }

        if(triggerMode == false)
            //Room changing camera
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);
        else
        {
            //PLAYER FOLLOWER camera
            transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);

        }

    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }


}
