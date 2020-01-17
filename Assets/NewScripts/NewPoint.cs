using UnityEngine;

namespace Assets.NewScripts
{
    public class NewPoint
    {
        public bool Free { get; set; }
        public Vector3 Position { get { return point.transform.position; } }

        private GameObject point;

        public NewPoint(GameObject newPoint)
        {
            Free = true;
            point = newPoint;
        }
    }
}
