using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // Velocidade de rotação do rosto
    public float rotationSpeed = 5f;

    void Update()
    {
        // Obter a posição do mouse na tela
        Vector3 mousePosition = Input.mousePosition;

        // Converter a posição do mouse de pixels para unidades do mundo
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y - transform.position.y));

        // Calcular a direção do mouse em relação ao objeto
        Vector3 direction = mousePosition - transform.position;

        // Calcular a rotação necessária para olhar na direção do mouse
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Ajustar a rotação
        targetRotation *= Quaternion.Euler(5f, 120f, 35f);

        // Aplicar a rotação suavizada
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
