using UnityEngine;

public class Player : Character,IShooter
{
    public Weapons CurrentWeapon {get; private set;}

    public void ChangeWeapon(Weapons WeaponToChange)
    {
        CurrentWeapon = WeaponToChange;
    }

    public void Shoot(Weapons weapons)
    {
        
    }
    
    
    
}
