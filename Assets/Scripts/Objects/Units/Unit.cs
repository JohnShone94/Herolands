using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSManager;

public class Unit : Objects
{
    public float moveSpeed, rotateSpeed;
    public override void SetHoverState(GameObject hoverObject)
    {
        base.SetHoverState(hoverObject);
        if(player && player.human && currentlySelected)
        {
            if(hoverObject.name == "Ground")
            {
                player.hud.SetCursorState(CursorState.Move);
            }
        }
    }



    public override void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
        base.MouseClick(hitObject, hitPoint, controller);

        //we want to only accept input if the unit is owned by the player and is currently selected.
        if(player && player.human && currentlySelected)
        {
            if(hitObject.name == "Ground" && hitPoint != ResourceManager.InvalidPosition)
            {
                float x = hitPoint.x;
                float y = hitPoint.y + player.selectedObject.transform.position.y;
                float z = hitPoint.z;

                Vector3 destination = new Vector3(x, y, z);
                StartMove(destination);
            }
        }
    }

    public void StartMove(Vector3 destination)
    {
        this.destination = destination;
        targetRotation = Quaternion.LookRotation(destination - transform.position);
        rotating = true;
        moving = false;
    }

    protected bool moving = false, rotating = false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(rotating)
        {
            TurnToTarget();
        }
        else if(moving)
        {
            MoveToTarget();
        }
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }

    private Vector3 destination;
    private Quaternion targetRotation;

    private void TurnToTarget()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed);

        //may get stuck at exactly 180 degree. need fixing.

        Quaternion inverseTargetRotation = new Quaternion(-targetRotation.x, -targetRotation.y, -targetRotation.z, -targetRotation.w);
        if(transform.rotation == targetRotation || transform.rotation == inverseTargetRotation)
        {
            rotating = false;
            moving = true;
        }
        CalculateBounds();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * moveSpeed);
        if(transform.position == destination)
        {
            moving = false;
        }

        CalculateBounds();
    }
}
