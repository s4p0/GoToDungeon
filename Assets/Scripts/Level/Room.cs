using UnityEngine;

namespace Level
{

    [System.Serializable]
    public class Room
    {
        //public enum Side { Top, Left, Right, Bottom };
        public Grid GridPrefab;
        public Vector2Int GridSize;
        public bool overrideHorizontalVertical = false;
        public bool overridedHorizontal = false;
        public bool IsHorizontal
        {
            get
            {
                if (overrideHorizontalVertical)
                    return overridedHorizontal;
                return GridSize.x > GridSize.y;
            }
        }

        public Vector3 Wide
        {
            get { return GridPrefab.CellToWorld(new Vector3Int(GridSize.x, 0, 0)); }

        }

        public Vector3 Height
        {
            get { return GridPrefab.CellToWorld(new Vector3Int(0, GridSize.y, 0)); }
        }

        public Vector3 HorizontalOffset(Room other)
        {
            return GridPrefab.CellToWorld(new Vector3Int(Mathf.Abs(GridSize.x - other.GridSize.x) / 2, 0, 0));
        }

        public Vector3 VerticalOffset(Room other)
        {
            return GridPrefab.CellToWorld(new Vector3Int(0, Mathf.Abs(GridSize.y - other.GridSize.y) / 2, 0));
        }

        public Bounds GetWorldBounds(Vector3 pos)
        {
            var b = new Bounds();
            b.Encapsulate(pos);
            b.Encapsulate(pos + Wide + Height);

            return b;
        }

    }

}

