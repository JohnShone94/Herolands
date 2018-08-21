using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSManager
{
    public static class WorkManager
    {
        public static Rect CalculateSelectionBox(Bounds selectionBounds, Rect playingArea)
        {
            //Variabled have been shorthanded for example CX means Center X and EX mean Extents X
            float cx = selectionBounds.center.x;
            float cy = selectionBounds.center.y;
            float cz = selectionBounds.center.z;
            float ex = selectionBounds.extents.x;
            float ey = selectionBounds.extents.y;
            float ez = selectionBounds.extents.z;

            //We are finding the screen coords for each corner of the selection bounds
            List<Vector3> corners = new List<Vector3>
            {
                Camera.main.WorldToScreenPoint(new Vector3(cx + ex, cy + ey, cz + ez)),
                Camera.main.WorldToScreenPoint(new Vector3(cx + ex, cy + ey, cz - ez)),
                Camera.main.WorldToScreenPoint(new Vector3(cx + ex, cy - ey, cz + ez)),
                Camera.main.WorldToScreenPoint(new Vector3(cx - ex, cy + ey, cz + ez)),
                Camera.main.WorldToScreenPoint(new Vector3(cx + ex, cy - ey, cz - ez)),
                Camera.main.WorldToScreenPoint(new Vector3(cx - ex, cy - ey, cz + ez)),
                Camera.main.WorldToScreenPoint(new Vector3(cx - ex, cy + ey, cz - ez)),
                Camera.main.WorldToScreenPoint(new Vector3(cx - ex, cy - ey, cz - ez))
            };

            //We must find the bounds on screen for the selection bounds.
            Bounds screenBounds = new Bounds(corners[0], Vector3.zero);
            for(int i = 1; i < corners.Count; i++)
            {
                screenBounds.Encapsulate(corners[i]);
            }

            //finally we correct the position of the selection box as screen coords start in the bottom left corner rather than the top
            float selectionBoxTop = playingArea.height - (screenBounds.center.y + screenBounds.extents.y);
            float selectionBoxLeft = screenBounds.center.x - screenBounds.extents.x;
            float selectionBoxWidth = 2 * screenBounds.extents.x;
            float selectionBoxHeight = 2 * screenBounds.extents.y;

            return new Rect(selectionBoxLeft, selectionBoxTop, selectionBoxWidth, selectionBoxHeight);
        }
    }
}
