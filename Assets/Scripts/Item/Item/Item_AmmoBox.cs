using MoreMountains.Feedbacks;
using UnityEngine;

public class Item_AmmoBox : Item,IPickupAble
{
    [SerializeField] private MMF_Player pickUpFeedBack;
    [SerializeField] private int ammoIndex;
    [SerializeField] private int ammoAmount;
    public void Pickup()
    {
        Inventory.Instance.ammo[ammoIndex] = ammoAmount;
        pickUpFeedBack.PlayFeedbacks();
        UIManager.Instance.RefreshWeaponUI();
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }
}
