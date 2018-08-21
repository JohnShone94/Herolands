using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public string objectName;
    public Texture2D buildImage;
    public int cost;
    public int sellValue;
    public int hitPoints;
    public int maxHitPoints;


    public void SetSelection(bool selected)
    {
        currentlySelected = selected;
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
            Objects objects = hitObject.transform.root.GetComponent<Objects>();
            if(objects)
            {
                ChangeSelection(objects, controller);
            }
        }
    }

    protected Player player;
    protected string[] actions = { };
    protected bool currentlySelected = false;

    protected virtual void Awake ()
    {

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

    }

    private void ChangeSelection(Objects objects, Player controller)
    {
        SetSelection(false);
        if(controller.selectedObject)
        {
            controller.selectedObject.SetSelection(false);
        }
        controller.selectedObject = objects;
        objects.SetSelection(true);
    }
}
