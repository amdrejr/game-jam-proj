using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab; // Prefab do projétil que será disparado

    private Rigidbody rig;
    private Camera mainCamera; // Referência para a câmera principal

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        mainCamera = Camera.main; // Obter a referência para a câmera principal
    }

    void Update()
    {
        // Movimento do jogador
        MovePlayer();

        // Atualizar a mira do jogador com base na posição do mouse no plano XZ
        UpdateAimPosition();
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime;

        rig.MovePosition(newPosition);
    }

    void UpdateAimPosition()
    {
       if (mainCamera != null) {
            // Criar um raio a partir da posição do mouse na tela
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verificar se o raio atinge o plano XZ do jogo
            if (Physics.Raycast(ray, out hit)) {

                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y;

                Vector3 direction = targetPosition - transform.position;

                if (direction != Vector3.zero) {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = targetRotation;
                }
            }
        }
    }
}

