using UnityEngine;

namespace TimeObjects
{
    [System.Serializable]
    public class PointInTime
    {
        [SerializeField]
        public Vector3 position { get; set; }
        [SerializeField]
        public Quaternion rotation { get; set; }

        public PointInTime(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }
    }
}