using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0;
    private GameObject player;
    private Animator animator;

    public RawImage[] weaponsIcons;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();

        // Ativar a primeira arma
        SetActiveWeapon(currentWeaponIndex);

        // Verificar e atualizar a camada de anima��o da primeira arma
        UpdateAnimationLayer(currentWeaponIndex);

        // cor da pistol 100%
        SetTransparency(weaponsIcons[currentWeaponIndex], 1f);
    }

    void Update()
    {
        // Trocar de arma com teclas num�ricas (de 1 a 9)
        SwitchWeaponByNumber();
    }

    void SwitchWeaponByNumber()
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

        // Desativa a arma atual
        weapons[currentWeaponIndex].SetActive(false);
        SetTransparency(weaponsIcons[currentWeaponIndex], .2f);

        // Atualiza o �ndice da arma atual
        currentWeaponIndex = newIndex;

        // Ativa a nova arma
        weapons[currentWeaponIndex].SetActive(true);
        SetTransparency(weaponsIcons[currentWeaponIndex], 1f);

        // Atualiza a camada de anima��o para a nova arma
        UpdateAnimationLayer(currentWeaponIndex);

    }


    void SetActiveWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }
    }

    void UpdateAnimationLayer(int weaponIndex)
    {
        // Certifique-se de que o �ndice da arma est� dentro dos limites v�lidos
        if (weaponIndex < 0 || weaponIndex >= weapons.Length)
        {
            return;
        }

        // Determina o nome da camada com base no �ndice da arma
        string layerName = (weaponIndex == 0) ? "Pistol" : "Shotgun";

        // Verifica se o Animator possui a camada com o nome especificado
        int layerIndex = animator.GetLayerIndex(layerName);
        if (layerIndex != -1)
        {
            // Desativa todas as camadas
            for (int i = 0; i < animator.layerCount; i++)
            {
                animator.SetLayerWeight(i, 0f);
            }

            // Ativa a camada correspondente � nova arma
            animator.SetLayerWeight(layerIndex, 1f);
        }
    }

    // Método para definir a transparência de uma RawImage
    void SetTransparency(RawImage rawImage, float alpha)
    {
        Color color = rawImage.color;
        color.a = alpha;
        rawImage.color = color;
    }

}
