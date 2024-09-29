using UnityEngine;

namespace GpuRayTracing.Scenes.Prototype
{
    public class SceneManager : MonoBehaviour
    {
        public GameObject Prefab;

        public int CountX = 1;
        public int CountY = 1;
        public int CountZ = 1;

        public float SpheresDist = 2f;
        public float WallsDist = 2f;

        public Transform ParentLeft;
        public Transform ParentRight;

        private float _halfX;
        private float _halfY;
        private float _halfZ;

        private void Start()
        {
            _halfX = (CountX - 1) * SpheresDist / 2f;
            _halfY = (CountY - 1) * SpheresDist / 2f;
            _halfZ = (CountZ - 1) * SpheresDist / 2f;

            InitWallPosition();
            
            InitWall(ParentLeft);
            InitWall(ParentRight);
        }

        private void InitWallPosition()
        {
            var halfWalls = WallsDist / 2f + _halfX;
            ParentLeft.position = Vector3.left * halfWalls;
            ParentRight.position = Vector3.right * halfWalls;
        }

        private void InitWall(Transform parentTransform)
        {

            var startPos = new Vector3(-_halfX, -_halfY, -_halfZ);

            for (var iz = 0; iz < CountZ; iz++)
            {
                for (var iy = 0; iy < CountY; iy++)
                {
                    for (var ix = 0; ix < CountX; ix++)
                    {
                        var localPos = new Vector3(ix * SpheresDist, iy * SpheresDist, iz * SpheresDist);
                        var newObj = Instantiate(Prefab, parentTransform);
                        newObj.transform.localPosition = startPos + localPos;

                        var scale = Random.Range(0.2f, 1.7f);
                        newObj.transform.localScale = Vector3.one * scale;
                    }
                }
            }
        }

        private void Update()
        {
        }
    }
}
