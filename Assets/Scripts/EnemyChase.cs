using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {
    [SerializeField] public NavMeshAgent enemy;
    private GameObject playerGameObject;
    public bool isChasing = true;
    private Animator animator;
    private float distAtk;
    [SerializeField] private float distLimit;
    
    
    // Start is called before the first frame update
    void Start() {
        playerGameObject = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update() {
        // Verifica se o GameObject do Player está ativo antes de acessá-lo
        if (playerGameObject != null && playerGameObject.activeSelf && isChasing) {
            enemy.SetDestination(playerGameObject.transform.position);

            
        } else {
            enemy.ResetPath();
        }
    }
                
    public void setIsChasing(bool b) {
        isChasing = b;
    }
   



    // public void AtaqueVilao(){
    //     if(playerGameObject != null && Vector3.Distance(transform.position, playerGameObject.transform.position) <= distAtack){
    //         animator.SetTrigger("Ataque");
    //         atack = true;
    //     }else{
    //         animator.SetBool("Ataque", false);
    //     }

    //     if(atack){
    //         enemy.speed = 0;
    //         enemy.isStopped = true;
    //     }else{
    //         enemy.speed = 10;
    //         enemy.isStopped = false;
    //     }
    // }
}
