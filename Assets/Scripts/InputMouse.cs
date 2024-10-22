using UnityEngine;

namespace GpuRayTracing
{
    public class InputMouse
    {
        public Vector3 Position => _position + _delta;

        private Vector3 _lastPos = Input.mousePosition;
        private Vector3 _position;
        private Vector3 _delta;

        public void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _delta = Input.mousePosition - _lastPos;
                return;
            }

            _position += _delta;
            _delta = Vector3.zero;
            _lastPos = Input.mousePosition;
        }
    }
}
