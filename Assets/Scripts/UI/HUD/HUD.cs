using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTSManager;

public class HUD : MonoBehaviour
{
    //21-08-2018//
    //John Shone//

    public GUISkin resourceSkin, ordersSkin, selectBoxSkin, mouseCursorSkin;

    public Texture2D activeCursor, selectCursor, leftCursor, rightCursor, upCursor, downCursor;
    public Texture2D[] moveCursors, attackCursors, harvestCursors;


    public bool MouseInBounds()
    {
        //The Screen coords start in the lower left corner of the screen not the top left unlike the drawing coordinates.

        Vector3 mousePos = Input.mousePosition;
        bool insideWidth = mousePos.x >= 0 && mousePos.x <= Screen.width - ORDERS_BAR_WIDTH;
        bool insideHeight = mousePos.y >= 0 && mousePos.y <= Screen.height - RESOURCE_BAR_HEIGHT;

        return insideWidth && insideHeight;
    }

    public Rect GetPlayingArea()
    {
        return new Rect(0, RESOURCE_BAR_HEIGHT, Screen.width - ORDERS_BAR_WIDTH, Screen.height - RESOURCE_BAR_HEIGHT);
    }

    public void SetCursorState(CursorState newState)
    {
        activeCursorState = newState;
        switch(newState)
        {
            case CursorState.Select:
                activeCursor = selectCursor;
                break;
            case CursorState.Attack:
                currentFrame = (int)Time.time % attackCursors.Length;
                activeCursor = attackCursors[currentFrame];
                break;
            case CursorState.Harvest:
                currentFrame = (int)Time.time % harvestCursors.Length;
                activeCursor = harvestCursors[currentFrame];
                break;
            case CursorState.Move:
                currentFrame = (int)Time.time % moveCursors.Length;
                activeCursor = moveCursors[currentFrame];
                break;
            case CursorState.PanDown:
                activeCursor = downCursor;
                break;
            case CursorState.PanLeft:
                activeCursor = leftCursor;
                break;
            case CursorState.PanRight:
                activeCursor = rightCursor;
                break;
            case CursorState.PanUp:
                activeCursor = upCursor;
                break;
            default:
                break;
        }
    }

    private const int ORDERS_BAR_WIDTH = 150;
    private const int RESOURCE_BAR_HEIGHT = 40;
    private const int SELECTION_NAME_HEIGHT = 80;

    private Player player;

    private CursorState activeCursorState;
    private int currentFrame = 0;

    private void Start()
    {
        ResourceManager.StoreSelectBoxItems(selectBoxSkin);
        player = transform.root.GetComponent<Player>();

        SetCursorState(CursorState.Select);
    }

    private void OnGUI()
    {
        if(player && player.human)
        {
            DrawOrdersBar();
            DrawResourceBar();
            DrawMouseCursor();
        }
    }



    private void DrawMouseCursor()
    {
        bool mouseOverHud = !MouseInBounds() && activeCursorState != CursorState.PanRight && activeCursorState != CursorState.PanUp;

        if(mouseOverHud)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            GUI.skin = mouseCursorSkin;
            GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));
            UpdateCursorAnimation();
            Rect cursorPosition = GetCursorDrawPosition();
            GUI.Label(cursorPosition, activeCursor);
            GUI.EndGroup();
        }
    }

    private void UpdateCursorAnimation()
    {
        //Takes 2 or more images and runs them through a sequence animation, loops through an array of images.
        if(activeCursorState == CursorState.Move)
        {
            currentFrame = (int)Time.time % moveCursors.Length;
            activeCursor = moveCursors[currentFrame];
        }
        else if(activeCursorState == CursorState.Attack)
        {
            currentFrame = (int)Time.time % attackCursors.Length;
            activeCursor = attackCursors[currentFrame];
        }
        else if (activeCursorState == CursorState.Harvest)
        {
            currentFrame = (int)Time.time % harvestCursors.Length;
            activeCursor = harvestCursors[currentFrame];
        }
    }

    private Rect GetCursorDrawPosition()
    {
        float leftPos = Input.mousePosition.x;
        float topPos = Screen.height - Input.mousePosition.y;

        if(activeCursorState == CursorState.PanRight)
        {
            leftPos = Screen.width - activeCursor.width;
        }
        else if(activeCursorState == CursorState.PanDown)
        {
            topPos = Screen.height - activeCursor.height;
        }
        else if(activeCursorState == CursorState.Move || activeCursorState == CursorState.Select || activeCursorState == CursorState.Harvest)
        {
            topPos -= activeCursor.height / 2;
            leftPos -= activeCursor.width / 2;
        }

        return new Rect(leftPos, topPos, activeCursor.width, activeCursor.height);
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
