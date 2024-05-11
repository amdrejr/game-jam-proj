using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chicken : MonoBehaviour {
    [SerializeField] private NavMeshAgent chicken;
    public int healingAmount = 30; // Quantidade de cura ao ser morta pelo jogador
    public Slider playerVida; // Instancia da vida do personagem (Slider)
    public Transform[] targetToGo; // Elementos do cenário para onde a galinha deve correr

    public float fleeDistance = 20f; // Distância mínima em que a galinha começará a fugir do jogador
    private float fleeTimer = 0f;
    private float fleeCooldown = 1f; // Tempo de espera antes de calcular a direção de fuga novamente

    private void OnCollisionStay(Collision collision) {
        // Verifica se a galinha colidiu com o Player
        if (collision.collider.CompareTag("Player")) {
            // Destroi a galinha
            Destroy(gameObject);
            // Chama o método Heal para curá-lo
            Heal(healingAmount);

            // Adiciona pontos ao jogador
            FindObjectOfType<TurnManager>().addPoints(10);
        }
    }

    private void Update() {
        // Verifica se há pelo menos um destino configurado
        if (targetToGo.Length > 0) {
            // Verifica se o GameObject do Player está ativo antes de acessá-lo
            GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");
            if (playerGameObject != null && playerGameObject.activeSelf) {
                // Calcula a direção oposta à posição do jogador
                Vector3 fleeDirection = transform.position - playerGameObject.transform.position;

                // Incrementa o temporizador
                fleeTimer += Time.deltaTime;

                // Verifica se o temporizador atingiu o tempo de espera
                if (fleeTimer >= fleeCooldown) {
                    // Verifica se a distância entre a galinha e o jogador é menor que a distância de fuga
                    if (fleeDirection.magnitude < fleeDistance) {
                        // Define o destino da galinha como a posição oposta ao jogador
                        Vector3 targetPosition = transform.position + fleeDirection;
                        chicken.SetDestination(targetPosition);

                        // Reinicia o temporizador
                        fleeTimer = 0f;
                    }
                }

                // Se não estiver fugindo, escolhe um destino aleatório da matriz de destinos
                if (fleeTimer >= fleeCooldown || fleeDirection.magnitude >= fleeDistance) {
                    Transform randomTarget = targetToGo[Random.Range(0, targetToGo.Length)];
                    chicken.SetDestination(randomTarget.position);

                    // Verifica se a galinha chegou ao destino
                    if (!chicken.pathPending && chicken.remainingDistance <= chicken.stoppingDistance) {
                        // Destroi a galinha
                        Destroy(gameObject);
                    }
                }
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
