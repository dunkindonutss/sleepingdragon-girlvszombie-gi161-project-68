using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [Header("Ammo")]
    //0= pistol 1 = Ar
    [field: SerializeField] public int[] ammo {get; private set;}
    [Header("Weapons")]
    [SerializeField] private Weapons[] AllWeapons;
    [field: SerializeField] public List<Weapons> WeaponsList {get; private set;}
    public int currentWeaponIndex;

    private void Start()
    {
        AddWeapon(0);
        AddWeapon(1);
    }
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public int CountAmmo(int index)
    {
        return ammo[index];
    }

    public void AddAmmo(int index, int amount)
    {
        ammo[index] += amount;
    }

    public void AddWeapon(int index)
    {
        WeaponsList.Add(AllWeapons[index]);
    }

    public Weapons GetCurrentWeapon()
    {
        return WeaponsList[currentWeaponIndex];
    }
}
