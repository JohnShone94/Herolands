using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    //21-08-2018//
    //John Shone//

    public GUISkin resourceSkin, ordersSkin;

    public bool MouseInBounds()
    {
        //The Screen coords start in the lower left corner of the screen not the top left unlike the drawing coordinates.

        Vector3 mousePos = Input.mousePosition;
        bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width - ORDERS_BAR_WIDTH;
        bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.height - RESOURCE_BAR_HEIGHT;

        return insideWidth && insideHeight;
    }

    private const int ORDERS_BAR_WIDTH = 150;
    private const int RESOURCE_BAR_HEIGHT = 40;
    private const int SELECTION_NAME_HEIGHT = 80;

    private Player player;

    private void Start()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void OnGUI()
    {
        if(player && player.human)
        {
            DrawOrdersBar();
            DrawResourceBar();
        }
    }

    private void DrawOrdersBar()
    {
        GUI.skin = ordersSkin;
        GUI.BeginGroup(new Rect(Screen.width - ORDERS_BAR_WIDTH, RESOURCE_BAR_HEIGHT, ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT));
        GUI.Box(new Rect(0, 0, ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT), "");

        string selectionName = "";
        if(player.selectedObject)
        {
            selectionName = player.selectedObject.objectName;
        }

        if(!selectionName.Equals(""))
        {
            GUI.Label(new Rect(0, 10, ORDERS_BAR_WIDTH, SELECTION_NAME_HEIGHT), selectionName);
        }

        GUI.EndGroup();
    }

    private void DrawResourceBar()
    {
        GUI.skin = resourceSkin;
        GUI.BeginGroup(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT));
        GUI.Box(new Rect(0, 0, Screen.width, RESOURCE_BAR_HEIGHT), "");
        GUI.EndGroup();
    }
}
