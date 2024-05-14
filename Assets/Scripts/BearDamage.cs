using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearBoss : MonoBehaviour {
    [SerializeField] public NavMeshAgent agent;
    private int Damage = 10;
    public float attackInterval = 1f; // Intervalo de tempo entre cada dano
    private float lastAttackTime;
    private Animator animator;
    public AudioClip damageSound;
    private int maxHealth = 1000; // Vida máxima do inimigo
    private int currentHealth; // Vida atual do inimigo
    private bool isDead = false;
    private string actualAttack;

    void Start() {
        currentHealth = maxHealth; // Configurar a vida inicial
        animator = GetComponent<Animator>();
        StartCoroutine(VelocityIncrease());
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Projectile")) {
            TakeDamage(collision.collider.GetComponent<Projectile>().damageAmount); // Chamar o método TakeDamage com o dano do projétil
            Destroy(collision.gameObject);
        }

        if (collision.collider.CompareTag("Player") && GetComponent<EnemyChase>().isChasing) {
            if (Time.time - lastAttackTime >= attackInterval) {
                animator.SetBool(actualAttack, true);
                // Som
                print(actualAttack + " Atacou, " + currentHealth);
                StartCoroutine(Atacando(collision.collider.GetComponent<PlayerLife>()));
                lastAttackTime = Time.time;
            }
        }
    }

    IEnumerator Atacando(PlayerLife playerLife){
        yield return new WaitForSeconds(0.4f); // Espera a duração da animação de ataque
        animator.SetBool(actualAttack, false);
        if(damageSound != null){
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
        playerLife.TakeDamage(Damage);    
    }
    // Start is called before the first frame update

    IEnumerator VelocityIncrease(){
        while (true) {
            agent.acceleration = 6;
            // Define a velocidade normal
            agent.speed = 5;
            yield return new WaitForSeconds(5f);
            // Define a velocidade aumentada
            agent.speed = 25;
            yield return new WaitForSeconds(3f);
        }
    }

    // Update is called once per frame
    void Update() {
        if ( currentHealth <= 800 && currentHealth > 600) {
            actualAttack = "Attack2";
            Damage = 2;
        } else if (currentHealth <= 600 && currentHealth > 400) {
            actualAttack = "Attack3";
            Damage = 3;
        } else if (currentHealth <= 400 && currentHealth > 200) {
            actualAttack = "Attack4";
            Damage = 4;
        } else if (currentHealth <= 200 && currentHealth > 0) {
            actualAttack = "Attack5";
            Damage = 5;
        } else {
            actualAttack = "Attack1";
            Damage = 1;
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage; // Reduzir a vida atual pelo dano recebido
        // Verificar se a vida chegou a zero
        if (currentHealth <= 0 && !isDead) {
            Die();
        } else {
            //StartCoroutine(FlashRed()); // Caso contrário, iniciar a corrotina para piscar em vermelho
        }
    }

    public void Die() {
        GetComponent<EnemyChase>().setIsChasing(false); // ResetPath() aqui
        isDead = true; // Atualizar a flag de morte
        animator.SetBool("Death", true); // Animação de morte,
        FindObjectOfType<TurnManager>().addPoints(50); // Adiciona pontos ao jogador
        Destroy(gameObject, 3f); // destruímos o objeto inimigo
    }
}
