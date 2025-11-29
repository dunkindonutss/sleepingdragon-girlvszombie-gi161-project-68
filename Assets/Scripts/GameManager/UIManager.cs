using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Weapon")] 
    [SerializeField] private TMP_Text weaponNameText;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TMP_Text bulletCountText;
    [SerializeField] private GameObject[] panels;
    
    [Header("Quest")]
    [SerializeField] private GameObject QuestTextPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SetCursorState(false);
    }

    private void Update()
    {
        if (InputManager.Instance.TryMainMenu())
        {
            OpenPanel(0);
        }
    }

    public void RefreshQuest()
    {
        
    }

    public void RefreshWeaponUI()
    {
        weaponNameText.text = $"{Player.Instance.CurrentWeapon.weaponData.WeaponName}";
        bulletCountText.text = $"{Player.Instance.CurrentWeapon.BulletInGun}/{Inventory.Instance.ammo[Inventory.Instance.currentWeaponIndex]}";
        weaponImage.sprite = Player.Instance.CurrentWeapon.weaponData.WeaponIcon;
    }

    public void OpenPanel(int index)
    {
        panels[index].SetActive(true);
        SetCursorState(true);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        SetCursorState(true);
    }
    
    public void SetCursorState(bool isVisible)
    {
        if (isVisible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;   // ปล่อยเมาส์
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; // ล็อกเมาส์ตรงกลาง
        }
    }
    
}
