using UnityEngine;

public class TargetAim : MonoBehaviour
{
    [SerializeField] public Transform targetTransform;
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private float maxY = 45f;
    [SerializeField] private float minY = -45f;

    [Header("Recoil Settings")]
    [SerializeField] private float recoilReturnSpeed = 5f;
    [SerializeField] private float recoilSmooth = 0.1f;

    private float mouseY;
    private float recoilOffset;     
    private float recoilVelocity;

    private void Update()
    {
        ControlTarget();
        ProcessRecoil();
        ApplyToTransform();
    }

    private void ControlTarget()
    {
        float deltaY = InputManager.Instance.GetMouseDelta().y;

        // ⭐ เฉพาะการขยับเมาส์ ไม่ปน recoil
        mouseY += deltaY * sensitivity;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);
    }

    private void ProcessRecoil()
    {
        // ⭐ Recoil ทำงานแยกจากเมาส์
        recoilOffset = Mathf.SmoothDamp(recoilOffset, 0f, ref recoilVelocity, recoilSmooth);
    }

    private void ApplyToTransform()
    {
        float finalY = mouseY + recoilOffset; // ⭐ รวม mouse + recoil
        finalY = Mathf.Clamp(finalY, minY, maxY);

        Vector3 pos = targetTransform.localPosition;
        pos.y = finalY;
        targetTransform.localPosition = pos;
    }

    public void AddRecoil(float recoilAmount)
    {
        recoilOffset += recoilAmount;
        recoilOffset = Mathf.Clamp(recoilOffset, minY, maxY);
    }
}