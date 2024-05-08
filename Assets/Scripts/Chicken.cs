using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chicken : MonoBehaviour {
    [SerializeField] private NavMeshAgent chicken;
    public int healingAmount = 30; // Quantidade de cura ao ser morta pelo jogador
    public Slider PlayerVida; // Instancia da vida do personagem (Slider)
    private GameObject playerGameObject;

    // Distância mínima em que a galinha começará a fugir do jogador
    public float fleeDistance = 10f;

    private void Start() {
        playerGameObject = GameObject.Find("Player");
    }

     private void OnCollisionStay(Collision collision) {
        // Verifica se a galinha colidiu com o Player
        if(collision.collider.CompareTag("Player")) {
            
            // Chama o método Heal para curá-lo
            Heal(healingAmount);

            // Destroi a galinha
            Destroy(gameObject);
        }
    }

    private void Update() {
        // Verifica se o GameObject do Player está ativo antes de acessá-lo
        if (playerGameObject != null && playerGameObject.activeSelf) {
            // Calcula a direção oposta à posição do jogador
            Vector3 fleeDirection = transform.position - playerGameObject.transform.position;

            // Verifica se a distância entre a galinha e o jogador é menor que a distância de fuga
            if (fleeDirection.magnitude < fleeDistance) {
                // Normaliza a direção oposta
                fleeDirection.Normalize();

                // Define o destino da galinha como a posição oposta ao jogador
                Vector3 targetPosition = transform.position + fleeDirection * 10f; // Multiplica por uma distância para garantir que a galinha fuja
                chicken.SetDestination(targetPosition);

            } else {
                // Se o jogador estiver longe demais, para de fugir
                chicken.ResetPath(); // Limpa a rota atual
            }
        }
    }

    public void Heal(int healing) {
        if(PlayerVida.value + healing <= 100 ) {
            PlayerVida.value += healing; 
        } else {
            PlayerVida.value = 100;
        }
        print("Life atual: " + PlayerVida.value);
    }
}
