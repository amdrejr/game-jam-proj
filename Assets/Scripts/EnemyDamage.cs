using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    public int Damage;
    public float attackInterval = 1f; // Intervalo de tempo entre cada dano
    private float lastAttackTime;

    private Animator animator;
    public AudioSource audioSource;
    public AudioClip damageSound;

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Player") && GetComponent<EnemyChase>().isChasing) {
            if (Time.time - lastAttackTime >= attackInterval) {
                animator.SetBool("Ataque", true);
                // Som
                
                StartCoroutine(Atacando(collision.collider.GetComponent<PlayerLife>()));
                lastAttackTime = Time.time;
            }
        }
        
    }

IEnumerator Atacando(PlayerLife playerLife){
        yield return new WaitForSeconds(0.4f); // Espera a duração da animação de ataque
        animator.SetBool("Ataque", false);
        if(damageSound != null){
                audioSource.clip = damageSound;
                audioSource.Play();
        }
        playerLife.TakeDamage(Damage);    
    }
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
