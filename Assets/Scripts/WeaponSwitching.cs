using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0; 

    void Start()
    {
        SetActiveWeapon(currentWeaponIndex);
    }

    void Update()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i);
                break;
            }
        }
    }

    void SwitchWeapon(int newIndex)
    {
        if (newIndex < 0 || newIndex >= weapons.Length)
            return;

        weapons[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = newIndex;
        weapons[currentWeaponIndex].SetActive(true);
    }

    void SetActiveWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }
    }
}
