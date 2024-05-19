using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatController : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player").transform; // Encontra o GameObject do jogador

        // Mira no jogador
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

        // Move a cabra para frente
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void Update()
    {
        // Verifica se a cabra alcançou o outro lado da tela
        if (transform.position.x > 10f) // Ajuste conforme a largura da sua tela
        {
            Destroy(gameObject);
        }
    }
}
