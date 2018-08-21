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

        public static Vector3 InvalidPosition { get { return invalidPosition; } }
        private static Vector3 invalidPosition = new Vector3(-99999, -99999, -99999);
    }
}
