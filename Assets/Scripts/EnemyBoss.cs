using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour {

    private GameObject player;
    [SerializeField] public Slider bossLifeSlider;
    [SerializeField] public NavMeshAgent agent;
    private int Damage = 10;
    public float attackInterval = 1f; // Intervalo de tempo entre cada dano
    private float lastAttackTime;
    private Animator animator;
    public AudioClip damageSound;
     //private int maxHealth = 1000; // Vida máxima do inimigo
    private bool isDead = false;
    private string actualAttack;

    private bool canAttack = true;

    public float stoppingDistance = 0.2f;

    
    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(VelocityIncrease());
        agent.stoppingDistance = stoppingDistance;
        player = GameObject.Find("Player");
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Projectile")) {
            TakeDamage(collision.collider.GetComponent<Projectile>().damageAmount); // Chamar o método TakeDamage com o dano do projétil
            Destroy(collision.gameObject);
        }

        
        if (collision.collider.CompareTag("Player")&& GetComponent<EnemyChase>().isChasing  ) {
          
            if (Time.time - lastAttackTime >= attackInterval) {
                
                // animator.SetBool(actualAttack, true);
                // Som
              // print(actualAttack + " Atacou, " + bossLifeSlider);
                // StartCoroutine(Atacando(collision.collider.GetComponent<PlayerLife>()));
                collision.collider.GetComponent<PlayerLife>().TakeDamage(Damage);
                GetComponent<DamageAnimation>().PlayDamageAnimation(player.transform);
                
                lastAttackTime = Time.time;
            }
        } 

    }



    IEnumerator Atacando2(){
        
       // GetComponent<EnemyChase>().setCanAttack(false);
       agent.isStopped = true;
        animator.SetBool(actualAttack, true);
        yield return new WaitForSeconds(2.1f); // Espera a duração da animação de ataque
        if(actualAttack == "Attack1"){
            GetComponent<DamageAnimation>().PlayImpactAnimation(transform);
        }
       // yield return new WaitForSeconds(0.5f);
        agent.isStopped = false;
        if(damageSound != null){
            AudioSource.PlayClipAtPoint(damageSound, transform.position);
        }
         animator.SetBool(actualAttack, false);
         //

       //  GetComponent<EnemyChase>().setCanAttack(true);
         
         yield return new WaitForSeconds(2.5f);
        
        canAttack = true;
          
    }

    // IEnumerator Atacando(PlayerLife playerLife){
    //     yield return new WaitForSeconds(0.3f); // Espera a duração da animação de ataque
    //      animator.SetBool(actualAttack, false);
    //     if(damageSound != null){
    //         AudioSource.PlayClipAtPoint(damageSound, transform.position);
    //     }
    //     playerLife.TakeDamage(Damage);    
    // }
    // Start is called before the first frame update

    IEnumerator VelocityIncrease(){
        while (true) {
            agent.acceleration = 6;
            // Define a velocidade normal
            agent.speed = 8;
            yield return new WaitForSeconds(5f);
            // Define a velocidade aumentada
            agent.speed = 18;
            yield return new WaitForSeconds(3f);
        }
    }

    // Update is called once per frame
    void Update() {
        if(Vector3.Distance(transform.position, player.transform.position ) <= 45f && canAttack){
            
            
            canAttack = false;
            StartCoroutine(Atacando2());
            }/*else{
                animator.SetBool(actualAttack, false);
            } */
        if ( bossLifeSlider.value <= 1000) {
            actualAttack = "Attack2";
            Damage = 30;
        } else {
            actualAttack = "Attack1";
            Damage = 15;
        }
        //print(Vector3.Distance(transform.position, player.transform.position ));
        /*if (bossLifeSlider.value <= 550 && bossLifeSlider.value > 550) {
            actualAttack = "Attack3";
            Damage = 20;
        } else if (bossLifeSlider.value <= 400 && bossLifeSlider.value > 200) {
            actualAttack = "Attack4";
            Damage = 30;
        } else if (bossLifeSlider.value <= 200 && bossLifeSlider.value > 0) {
            actualAttack = "Attack5";
            Damage = 40;
        } */ 
           
            


    }

    public void TakeDamage(int damage) {
        bossLifeSlider.value -= damage; // Reduzir a vida atual pelo dano recebido
        // Verificar se a vida chegou a zero
        if (bossLifeSlider.value <= 0 && !isDead) {
            Die();
        } else {
            //StartCoroutine(FlashRed()); // Caso contrário, iniciar a corrotina para piscar em vermelho
        }
    }

    public void Die() {
        GetComponent<EnemyChase>().setIsChasing(false); // ResetPath() aqui
        isDead = true; // Atualizar a flag de morte
        // animator.SetBool("Death", true); // Animação de morte,
        FindObjectOfType<TurnManager>().addPoints(50); // Adiciona pontos ao jogador
        Destroy(gameObject, 3f); // destruímos o objeto inimigo
    }
}
