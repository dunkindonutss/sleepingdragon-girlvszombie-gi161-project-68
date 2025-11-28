using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    [SerializeField] private GameObject[] weaponObject;
    [SerializeField] private GunIKController ikController;

    private int direction = 1; // 1 = ไปข้างหน้า, -1 = ไปข้างหลัง

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        ChangeWeapon();
    }

    public void ShowCurrentWeapon()
    {
        // ปิดอาวุธทั้งหมด
        foreach (var weapon in weaponObject)
            weapon.SetActive(false);

        int index = Inventory.Instance.currentWeaponIndex;
        if (index >= 0 && index < weaponObject.Length)
            weaponObject[index].SetActive(true);
    }

    public void ChangeWeapon()
    {
        if (!InputManager.Instance.TryChangeWeapon()) return;

        int weaponCount = Inventory.Instance.WeaponsList.Count;
        if (weaponCount <= 1) return; // มีปืนเดียวไม่ต้องเปลี่ยน

        int currentIndex = Inventory.Instance.currentWeaponIndex;

        // คำนวณ index ถัดไป
        int nextIndex = currentIndex + direction;

        // ถ้าไปสุดฝั่ง ให้กลับทิศทาง
        if (nextIndex >= weaponCount) 
        {
            direction = -1;
            nextIndex = currentIndex + direction;
        }
        else if (nextIndex < 0)
        {
            direction = 1;
            nextIndex = currentIndex + direction;
        }

        Inventory.Instance.currentWeaponIndex = nextIndex;

        Player.Instance.ChangeWeapon(nextIndex);
        ShowCurrentWeapon();
        ikController.SetGunTargets(Player.Instance.CurrentWeapon.r_handEffector,Player.Instance.CurrentWeapon.l_handEffector);
        UIManager.Instance.RefreshWeaponUI();
    }
}