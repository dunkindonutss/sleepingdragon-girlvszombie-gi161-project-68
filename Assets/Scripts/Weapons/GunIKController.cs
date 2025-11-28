using RootMotion.FinalIK;
using UnityEngine;

public class GunIKController : MonoBehaviour
{
    [Header("IK")]
    public FullBodyBipedIK ik;
    public AimIK aimIK;

    [Header("Gun Targets")]
    public Transform rightHandTarget;
    public Transform leftHandTarget;

    [Header("Settings")]
    public float handIKWeight = 1f;
    public float blendSpeed = 6f;

    private float currentWeight = 0f;

    private void LateUpdate()
    {
        if (rightHandTarget == null || leftHandTarget == null) return;

        // Smooth blend
        currentWeight = Mathf.Lerp(currentWeight, handIKWeight, Time.deltaTime * blendSpeed);

        // Right hand
        ik.solver.rightHandEffector.target = rightHandTarget;
        //ik.solver.rightHandEffector.positionWeight = currentWeight;
        //ik.solver.rightHandEffector.rotationWeight = currentWeight;

        // Left hand
        ik.solver.leftHandEffector.target = leftHandTarget;
        //ik.solver.leftHandEffector.positionWeight = currentWeight;
        //ik.solver.leftHandEffector.rotationWeight = currentWeight;
    }

    public void SetGunTargets(Transform rightTarget, Transform leftTarget)
    {
        rightHandTarget = rightTarget;
        leftHandTarget = leftTarget;
    }
}