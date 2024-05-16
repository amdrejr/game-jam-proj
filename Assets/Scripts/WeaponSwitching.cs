using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] weapons;
    private int currentWeaponIndex = 0;
    private GameObject player;
    private Animator animator;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = player.GetComponent<Animator>();

        // Ativar a primeira arma
        SetActiveWeapon(currentWeaponIndex);

        // Verificar e atualizar a camada de animação da primeira arma
        UpdateAnimationLayer(currentWeaponIndex);
    }

    void Update()
    {
        // Trocar de arma com teclas numéricas (de 1 a 9)
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

        // Atualiza o índice da arma atual
        currentWeaponIndex = newIndex;

        // Ativa a nova arma
        weapons[currentWeaponIndex].SetActive(true);

        // Atualiza a camada de animação para a nova arma
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
        // Certifique-se de que o índice da arma está dentro dos limites válidos
        if (weaponIndex < 0 || weaponIndex >= weapons.Length)
        {
            return;
        }

        // Determina o nome da camada com base no índice da arma
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

            // Ativa a camada correspondente à nova arma
            animator.SetLayerWeight(layerIndex, 1f);
        }
    }

}
