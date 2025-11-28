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
    
    [Header("Quest")]
    [SerializeField] private GameObject QuestTextPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RefreshQuest()
    {
        
    }

    public void RefreshWeaponUI()
    {
        weaponNameText.text = $"{Player.Instance.CurrentWeapon.weaponData.WeaponName}";
        bulletCountText.text = $"";
    }
    
}
