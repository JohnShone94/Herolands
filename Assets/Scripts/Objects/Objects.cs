using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSManager;

public class Objects : MonoBehaviour
{
    public string objectName;
    public Texture2D buildImage;
    public int cost;
    public int sellValue;
    public int hitPoints;
    public int maxHitPoints;


    public void SetSelection(bool selected, Rect playArea)
    {
        currentlySelected = selected;
        if(selected)
        {
            this.playingArea = playArea;
        }
    }

    public string[] GetActions()
    {
        return actions;
    }

    public virtual void PerformAction(string actionToPerform)
    {

    }

    public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
        if(currentlySelected && hitObject && hitObject.name != "Ground")
        {
            Objects objects = hitObject.transform.parent.GetComponent<Objects>();
            if(objects)
            {
                ChangeSelection(objects, controller);
            }
        }
    }

    public virtual void SetHoverState(GameObject hoverObject)
    {
        if(player && player.human && currentlySelected)
        {
            if(hoverObject.name != "Ground")
            {
                player.hud.SetCursorState(CursorState.Select);
            }
        }
    }

    public void CalculateBounds()
    {
        selectionBounds = new Bounds(transform.position, Vector3.zero);
        foreach(Renderer r in GetComponentsInChildren<Renderer>())
        {
            selectionBounds.Encapsulate(r.bounds);
        }
    }


    protected Player player;
    protected string[] actions = { };
    protected bool currentlySelected = false;
    protected Bounds selectionBounds;
    protected Rect playingArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);

    protected virtual void Awake ()
    {
        selectionBounds = ResourceManager.InvalidBounds;
        CalculateBounds();
    }

    protected virtual void Start()
    {
        player = transform.root.GetComponent<Player>();
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnGUI()
    {
        if(currentlySelected)
        {
            DrawSelection();
        }
    }

    protected virtual void DrawSelectionBox(Rect selectBox)
    {
        GUI.Box(selectBox, "");
    }

    private void DrawSelection()
    {
        GUI.skin = ResourceManager.SelectBoxSkin;
        Rect selectBox = WorkManager.CalculateSelectionBox(selectionBounds, playingArea);

        GUI.BeginGroup(playingArea);
        DrawSelectionBox(selectBox);
        GUI.EndGroup();
    }

    private void ChangeSelection(Objects objects, Player controller)
    {
        SetSelection(false, playingArea);
        if(controller.selectedObject)
        {
            controller.selectedObject.SetSelection(false, playingArea);
        }
        controller.selectedObject = objects;
        objects.SetSelection(true, playingArea);
    }
}
