using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLife : MonoBehaviour {
    public int maxHealth = 100; // Vida máxima do inimigo
    private int currentHealth; // Vida atual do inimigo
    private Animator animator;
    private Renderer enemyRenderer; // Referência ao Renderer do inimigo
    private Color originalColor; // Cor original do inimigo
    private bool isDead = false; // Flag para verificar se o inimigo está morto

    // Variáveis para o efeito de piscar em vermelho
    public float flashDuration = 0.1f; // Duração do flash vermelho
    public Color flashColor = Color.red; // Cor do flash vermelho

    void Start() {
        currentHealth = maxHealth; // Configurar a vida inicial
        animator = GetComponent<Animator>();
        // Obter o componente Renderer do inimigo
        // enemyRenderer = GetComponent<Renderer>();
        // originalColor = enemyRenderer.material.color; // Salvar a cor original do inimigo
    }

    // Método para detectar colisões com projéteis
    public void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Projectile")) {
            TakeDamage(collision.collider.GetComponent<Projectile>().damageAmount); // Chamar o método TakeDamage com o dano do projétil
            Destroy(collision.gameObject);
            print("Acertou " + currentHealth);
        }
    }

    // Método para receber dano
    public void TakeDamage(int damage) {
        currentHealth -= damage; // Reduzir a vida atual pelo dano recebido
        // Verificar se a vida chegou a zero
        if (currentHealth <= 0 && !isDead) {
            Die();
        } else {
            //StartCoroutine(FlashRed()); // Caso contrário, iniciar a corrotina para piscar em vermelho
        }
    }

    // Método para a morte do inimigo
    public void Die() {
        isDead = true; // Atualizar a flag de morte
        animator.SetBool("Morto", true); // Animação de morte,
        GetComponent<EnemyChase>().setIsChasing(false); // ResetPath() aqui
        FindObjectOfType<TurnManager>().addPoints(50); // Adiciona pontos ao jogador
        Destroy(gameObject, 3f); // destruímos o objeto inimigo
    }
    // Corrotina para o efeito de piscar em vermelho
    // IEnumerator FlashRed() {
    //     enemyRenderer.material.color = flashColor; // Alterar a cor do inimigo para vermelho
    //     yield return new WaitForSeconds(flashDuration); // Esperar pela duração do flash
    //     enemyRenderer.material.color = originalColor; // Restaurar a cor original do inimigo
    // }
}
