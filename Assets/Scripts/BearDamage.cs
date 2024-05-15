using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearDamage : MonoBehaviour {
    public int Damage;
    public float attackInterval = 1f; // Intervalo de tempo entre cada dano
    private float lastAttackTime;

    private Animator animator;

    public AudioClip damageSound;

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Player") && GetComponent<EnemyChase>().isChasing) {
            if (Time.time - lastAttackTime >= attackInterval) {
                animator.SetBool("Attack5", true);
                // Som
                
                StartCoroutine(Atacando(collision.collider.GetComponent<PlayerLife>()));
                lastAttackTime = Time.time;
            }
        }
        
    }

IEnumerator Atacando(PlayerLife playerLife){
        yield return new WaitForSeconds(0.4f); // Espera a duração da animação de ataque
        animator.SetBool("Attack5", false);
        if(damageSound != null){
                    AudioSource.PlayClipAtPoint(damageSound, transform.position);
                }
        playerLife.TakeDamage(Damage);    
    }
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        
    }
}