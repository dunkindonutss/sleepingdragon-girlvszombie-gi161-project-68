using UnityEngine;

public class TargetAim : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float sensitivity = 0.1f; 
    [SerializeField] private float maxY = 45f;
    [SerializeField] private float minY = -45f;

    private float currentY;

    private void Start()
    {
        currentY = targetTransform.localPosition.y;
    }

    private void Update()
    {
        ControlTarget();
    }

    private void ControlTarget()
    {
        // 1. อ่านค่าจาก InputManager (ที่แก้แล้ว)
        // ค่าจะเป็น 0 เองทันทีที่เราหยุดขยับเมาส์ ไม่ต้องกลัวไหล
        float deltaY = InputManager.Instance.GetMouseDelta().y;

        // 2. คำนวณตำแหน่ง (เอา Deadzone ออกเพื่อให้เล็งละเอียดได้)
        currentY += deltaY * sensitivity;

        // 3. จำกัดขอบเขต
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // 4. อัปเดตตำแหน่ง
        Vector3 pos = targetTransform.localPosition;
        pos.y = currentY;
        targetTransform.localPosition = pos;
    }
}