using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Ammo")]
    //0= pistol 1 = Ar
    public int[] ammo {get; private set;}
    [Header("Weapons")]
    public List<Weapons> WeaponsList {get; private set;}

    public int CountAmmo(int index)
    {
        return ammo[index];
    }

    public void AddAmmo(int index, int amount)
    {
        ammo[index] += amount;
    }

    public void AddWeapon(Weapons weaponToAdd)
    {
        WeaponsList.Add(weaponToAdd);
    }
}
