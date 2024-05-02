using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {
    public int Damage;
    public float attackInterval = 1f; // Intervalo de tempo entre cada dano
    private float lastAttackTime;

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            if (Time.time - lastAttackTime >= attackInterval) {
                collision.collider.GetComponent<PlayerLife>().TakeDamage(Damage);
                lastAttackTime = Time.time;
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
