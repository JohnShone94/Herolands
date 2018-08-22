using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSManager
{
    //21-08-2018//
    //John Shone//

    public static class ResourceManager
    {
        public static float ScrollSpeed
        {
            get
            {
                return 25.0f;
            }
        }

        public static float RotateSpeed
        {
            get
            {
                return 100.0f;
            }
        }

        public static float RotateAmount
        {
            get
            {
                return 10.0f;
            }
        }

        public static float MinCameraHeight
        {
            get
            {
                return 10.0f;
            }
        }

        public static float MaxCameraHeight
        {
            get
            {
                return 40.0f;
            }
        }

        public static int ScrollBarrier
        {
            get
            {
                return 15;
            }
        }

        public static Vector3 InvalidPosition
        {
            get
            {
                return invalidPosition;
            }
        }

        public static Bounds InvalidBounds
        {
            get
            {
                return invalidBounds;
            }
        }

        public static GUISkin SelectBoxSkin
        {
            get
            {
                return selectBoxSkin;
            }
        }

        public static int BuildSpeed
        {
            get
            {
                return 2;
            }
        }

        public static void StoreSelectBoxItems(GUISkin skin)
        {
            selectBoxSkin = skin;
        }
        public static void SetGameObjectList(PrefabsList objectList)
        {
            prefabList = objectList;
        }
        public static GameObject GetBuilding(string name)
        {
            return prefabList.GetBuildings(name);
        }
        public static GameObject GetUnit(string name)
        {
            return prefabList.GetUnits(name);
        }
        public static GameObject GetPlayerObject()
        {
            return prefabList.GetPlayerObject();
        }
        public static Texture2D GetBuildImage(string name)
        {
            return prefabList.GetBuildImage(name);
        }


        private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
        private static Bounds invalidBounds = new Bounds(new Vector3(-99999, -99999, -99999), new Vector3(0,0,0));
        private static GUISkin selectBoxSkin;
        private static PrefabsList prefabList;
    }
}
