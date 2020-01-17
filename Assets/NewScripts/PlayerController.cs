using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Assets.NewScripts
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public Animator playerAnim;
        public static bool isInKitchen;

        public CinemachineVirtualCamera storeFrontCam;
        public CinemachineVirtualCamera kitchenCam;

        private Rigidbody2D rb;
        private Vector2 moveVelocity;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput.normalized * speed;
            CheckRoom();
        }

        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

            float lastInputX = Input.GetAxis("Horizontal");
            float lastInputY = Input.GetAxis("Vertical");

            if (lastInputX != 0 || lastInputY != 0)
            {
                playerAnim.SetTrigger("walk");
            }
        }

        public void CheckRoom()
        {
            if (CinemachineCore.Instance.IsLive(storeFrontCam))
            {
                isInKitchen = false;
            }
            if (CinemachineCore.Instance.IsLive(kitchenCam))
            {
                isInKitchen = true;
            }
        }
    }
}
