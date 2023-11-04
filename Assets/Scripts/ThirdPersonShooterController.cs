using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerAimCamera;
    
    StarterAssets.StarterAssetsInputs input;
    StarterAssets.ThirdPersonController controller;
    Animator animator;
    Rigidbody rb;
    Vector3 oldPos;
    private void Start()
    {
        input = GetComponent<StarterAssets.StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (input.aim)
        {
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
            playerAimCamera.enabled = true;
            playerFollowCamera.enabled = false;

            Vector3 camDir = Camera.main.transform.forward;
            transform.forward = Vector3.ProjectOnPlane(camDir, Vector3.up);
            Vector3 speed = (transform.position - oldPos) / Time.deltaTime;

            float forwardVelocity = Vector3.Dot(speed, transform.forward);
            float sideVelocity = Vector3.Dot(speed, transform.right);
            oldPos = transform.position;
            animator.SetFloat("ForwardVelocity", forwardVelocity);
            animator.SetFloat("SideVelocity", sideVelocity);

        }
        else
        {
            animator.SetLayerWeight(0, 1);
            animator.SetLayerWeight(1, 0);
            playerAimCamera.enabled = false;
            playerFollowCamera.enabled = true;
        }
    }
}
