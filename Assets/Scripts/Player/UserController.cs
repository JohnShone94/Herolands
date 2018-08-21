using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSManager;


public class UserController : MonoBehaviour
{
    //21-08-2018//
    //John Shone//

    private Player player;

    private void Start()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void Update()
    {
        if (player.human)
        {
            MoveCamera();
            RotateCamera();

            MouseActivity();
        }
    }

    private void MouseActivity()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LeftMouseClick();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            RightMouseClick();
        }
    }

    private void LeftMouseClick()
    {
        if(player.hud.MouseInBounds())
        {
            GameObject hitObject = FindHitObject();
            Vector3 hitPoint = FindHitPoint();
            if(hitObject && hitPoint != ResourceManager.InvalidPosition)
            {
                if(player.selectedObject)
                {
                    player.selectedObject.MouseClick(hitObject, hitPoint, player);
                }
                else if (hitObject.name != "Ground")
                {
                    Objects objects = hitObject.transform.root.GetComponent<Objects>();
                    if(objects)
                    {
                        player.selectedObject = objects;
                        objects.SetSelection(true);
                    }
                }
            }
        }
    }
    private void RightMouseClick()
    {
        if(player.hud.MouseInBounds() && !Input.GetKey(KeyCode.LeftAlt) && player.selectedObject)
        {
            player.selectedObject.SetSelection(false);
            player.selectedObject = null;
        }
    }

    private GameObject FindHitObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private Vector3 FindHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return ResourceManager.InvalidPosition;
    }

    private void MoveCamera()
    {
        float xPos = Input.mousePosition.x;
        float zPos = Input.mousePosition.y;
        Vector3 movement = new Vector3(0, 0, 0);

        //Moving the Camera Horizontally
        if(xPos >= 0 && xPos < ResourceManager.ScrollBarrier)
        {
            movement.x -= ResourceManager.ScrollSpeed;
        }
        else if(xPos <= Screen.width && xPos > Screen.width - ResourceManager.ScrollBarrier )
        {
            movement.x += ResourceManager.ScrollSpeed;
        }

        //Moving the Camera Vetically
        if (zPos >= 0 && zPos < ResourceManager.ScrollBarrier)
        {
            movement.z -= ResourceManager.ScrollSpeed;
        }
        else if (zPos <= Screen.height && zPos > Screen.height - ResourceManager.ScrollBarrier)
        {
            movement.z += ResourceManager.ScrollSpeed;
        }

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        //Zooming the camera in and out
        movement.y -= ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        //Setting the camera position
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;

        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //Limit the cameras y movement
        if(destination.y > ResourceManager.MaxCameraHeight)
        {
            destination.y = ResourceManager.MaxCameraHeight;
        }
        else if(destination.y < ResourceManager.MinCameraHeight)
        {
            destination.y = ResourceManager.MinCameraHeight;
        }

        if(destination != origin)
        {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
        }
    }

    private void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;

        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(1))
        {
            destination.x -= Input.GetAxis("Mouse Y") * ResourceManager.RotateAmount;
            destination.y += Input.GetAxis("Mouse X") * ResourceManager.RotateAmount;
        }

        if(destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
    }
}
