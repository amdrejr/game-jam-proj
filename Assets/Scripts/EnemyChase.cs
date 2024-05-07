using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {

    [SerializeField] private NavMeshAgent enemy;
    private GameObject playerGameObject;

    // Start is called before the first frame update
    void Start() {
        playerGameObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void Update() {
        // Verifica se o GameObject do Player está ativo antes de acessá-lo
        if (playerGameObject != null && playerGameObject.activeSelf) {
            enemy.SetDestination(playerGameObject.transform.position);
        } else {
            // Caso contrário, para de seguir
            enemy.ResetPath(); // Limpa a rota atual
        }
    }
}
