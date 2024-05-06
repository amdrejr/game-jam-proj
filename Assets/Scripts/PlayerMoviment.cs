using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour {
    public float Speed; 
    Rigidbody Rig;

    private void Start() {
        Rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        // Obter as entradas de movimento horizontal e vertical
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calcular a direção do movimento
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Verificar se há movimento
        if (movementDirection != Vector3.zero) {
            // Rotacionar o jogador na direção do movimento
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = targetRotation;
        }

        // Aplicar velocidade ao Rigidbody
        Rig.velocity = movementDirection * Speed;
    }
}
