using System;
using UnityEngine;

public class Player : Character,IShooter
{
    public static Player Instance;
    public Weapons CurrentWeapon {get; private set;}

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ChangeWeapon(Weapons WeaponToChange)
    {
        CurrentWeapon = WeaponToChange;
    }

    public void Shoot(Weapons weapons)
    {
        
    }
    
    
    
}
