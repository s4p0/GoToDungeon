using Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level
{
    public class RoomDoors : MonoBehaviour
    {
        public GameObject leftDoor;
        public GameObject rightDoor;
        public GameObject topDoor;
        public GameObject bottomDoor;
        
        private void Start()
        {
            
        }

        private RoomDirectionEnum Reverse(RoomDirectionEnum direction)
        {
            switch (direction)
            {
                case RoomDirectionEnum.LEFT:
                    return RoomDirectionEnum.RIGHT;
                case RoomDirectionEnum.RIGHT:
                    return RoomDirectionEnum.LEFT;
                case RoomDirectionEnum.UP:
                    return RoomDirectionEnum.DOWN;
                case RoomDirectionEnum.DOWN:
                    return RoomDirectionEnum.UP;
            }
            return RoomDirectionEnum.UP;
        }

        public void OpenDoor(RoomDirectionEnum direction, bool reverse = false)
        {
            if (reverse)
                direction = Reverse(direction);
            OpenOrClose(direction, false);
        }

        public void CloseDoor(RoomDirectionEnum direction, bool reverse = false)
        {
            if (reverse)
                direction = Reverse(direction);
            OpenOrClose(direction, true);
        }

        private void OpenOrClose(RoomDirectionEnum direction, bool enabled)
        {
            switch (direction)
            {
                case RoomDirectionEnum.LEFT:
                    Destroy(leftDoor.gameObject);
                    break;
                case RoomDirectionEnum.RIGHT:
                    Destroy(rightDoor.gameObject);
                    break;
                case RoomDirectionEnum.UP:
                    Destroy(topDoor.gameObject);
                    break;
                case RoomDirectionEnum.DOWN:
                    Destroy(bottomDoor.gameObject);
                    break;
            }
        }

    }

}