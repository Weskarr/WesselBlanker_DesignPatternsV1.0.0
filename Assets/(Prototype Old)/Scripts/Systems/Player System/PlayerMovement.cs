using Unity.Cinemachine;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 15f;
        [SerializeField] private float zoomScrollSpeed = 15f;
        [SerializeField] private float zoomKeysSpeed = 5f;
        [SerializeField] private float minZoom = 10f;
        [SerializeField] private float maxZoom = 40f;
        [SerializeField] private float curZoom = 0f;

        [SerializeField] private CinemachineCamera cmCam;
        [SerializeField] private CinemachineFollow cmFollow;

        [SerializeField] private GameObject targetToFollow;

        [SerializeField] private Collider cameraBounds;

        [SerializeField] private bool invertScroll;

        private void Start()
        {
            curZoom = maxZoom;
            curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
            Vector3 targetPos = targetToFollow.transform.position;
            targetToFollow.transform.position = new Vector3(targetPos.x, curZoom, targetPos.z);
        }

        private void Update()
        {
            HandleMovement();
            ClampPosition();
            HandleZoom();
        }

        void ClampPosition()
        {
            Vector3 clampedPos = cameraBounds.ClosestPoint(targetToFollow.transform.position);
            targetToFollow.transform.position = clampedPos;
        }

        void HandleMovement()
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 move = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * input;
            targetToFollow.transform.position += move * moveSpeed * Time.deltaTime;
        }

        void HandleZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomScrollSpeed;

            if (Input.GetKey(KeyCode.Q))
                scroll = 0.1f * zoomKeysSpeed;

            if (Input.GetKey(KeyCode.E))
                scroll = -0.1f * zoomKeysSpeed;

            if (Mathf.Abs(scroll) > 0.01f)
            {
                if (invertScroll)
                    curZoom += scroll;
                else
                    curZoom -= scroll;

                curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
                Vector3 targetPos = targetToFollow.transform.position;
                targetToFollow.transform.position = new Vector3(targetPos.x, curZoom, targetPos.z);
            }
        }
    }
}