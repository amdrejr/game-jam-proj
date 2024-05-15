using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rig;
    private Camera mainCamera;
    private Animator animator;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MovePlayer();
        UpdateAimPosition();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime;
        rig.MovePosition(newPosition);

        UpdateMovementAnimations(movement);

        animator.SetFloat("horizontal", horizontalInput);
        animator.SetFloat("vertical", verticalInput);
    }

    private void UpdateMovementAnimations(Vector3 movement)
    {
        if (movement != Vector3.zero)
        {
            // Determinar a direção do movimento em relação à orientação do jogador
            float forwardMovement = Vector3.Dot(movement.normalized, transform.forward);
            float rightMovement = Vector3.Dot(movement.normalized, transform.right);

            // Verificar se o jogador está indo para frente ou para trás
            if (Mathf.Abs(forwardMovement) > Mathf.Abs(rightMovement))
            {
                // Movimento principal é para frente ou para trás
                if (forwardMovement > 0.5f) // Movendo-se para frente
                {
                    print("frente");
                    animator.SetBool("IsMovingForward", true);
                    animator.SetBool("IsMovingBackward", false);
                    animator.SetBool("IsMovingLeft", false);
                    animator.SetBool("IsMovingRight", false);
                    animator.SetBool("Idle", false);
                }
                else if (forwardMovement < -0.5f) // Movendo-se para trás
                {
                    print("tras");
                    animator.SetBool("IsMovingForward", false);
                    animator.SetBool("IsMovingBackward", true);
                    animator.SetBool("IsMovingLeft", false);
                    animator.SetBool("IsMovingRight", false);
                    animator.SetBool("Idle", false);
                }
            }
            else
            {
                // Movimento principal é lateral (esquerda ou direita)
                if (rightMovement > 0.5f) // Movendo-se para a direita
                {
                    print("direita");
                    animator.SetBool("IsMovingForward", false);
                    animator.SetBool("IsMovingBackward", false);
                    animator.SetBool("IsMovingLeft", false);
                    animator.SetBool("IsMovingRight", true);
                    animator.SetBool("Idle", false);
                }
                else if (rightMovement < -0.5f) // Movendo-se para a esquerda
                {
                    print("esquerda");
                    animator.SetBool("IsMovingForward", false);
                    animator.SetBool("IsMovingBackward", false);
                    animator.SetBool("IsMovingLeft", true);
                    animator.SetBool("IsMovingRight", false);
                    animator.SetBool("Idle", false);
                }
            }
        }
        else
        {
            // Se o jogador não estiver se movendo, definir Idle como true
            animator.SetBool("IsMovingForward", false);
            animator.SetBool("IsMovingBackward", false);
            animator.SetBool("IsMovingLeft", false);
            animator.SetBool("IsMovingRight", false);
            animator.SetBool("Idle", true);
        }
    }


    void UpdateAimPosition()
    {
        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y;

                Vector3 direction = targetPosition - transform.position;

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = targetRotation;
                }
            }
        }
    }
}
