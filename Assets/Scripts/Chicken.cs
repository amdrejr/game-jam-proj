using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chicken : MonoBehaviour {
    [SerializeField] private NavMeshAgent chicken;
    public int healingAmount = 30; // Quantidade de cura ao ser morta pelo jogador
    public Slider playerVida; // Instancia da vida do personagem (Slider)
    public Transform[] targetToGo; // Elementos do cenário para onde a galinha deve correr

    private void Start() {
        
    }

    private void OnCollisionStay(Collision collision) {
        // Verifica se a galinha colidiu com o Player
        if (collision.collider.CompareTag("Player")) {
            // Destroi a galinha
            Destroy(gameObject);
            // Chama o método Heal para curá-lo
            Heal(healingAmount);
        }
    }

    private void Update() {
        // Verifica se há pelo menos um destino configurado
        if (targetToGo.Length > 0) {
            // Escolhe um destino aleatório da matriz de destinos
            Transform randomTarget = targetToGo[Random.Range(0, targetToGo.Length)];

            // Define o destino do NavMeshAgent como a posição do destino selecionado
            chicken.SetDestination(randomTarget.position);

            // Verifica se a galinha chegou ao destino
            if (!chicken.pathPending && chicken.remainingDistance <= chicken.stoppingDistance) {
                // Destroi a galinha
                Destroy(gameObject);
            }
        }
    }

    public void Heal(int healing) {
        if (playerVida.value + healing <= 100 ) {
            playerVida.value += healing; 
        } else {
            playerVida.value = 100;
        }
        print("Life atual: " + playerVida.value);
    }
}
