using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {

    [SerializeField] private NavMeshAgent enemy;
    private Transform Player;

    // Start is called before the first frame update
    void Start() {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        // enemy.SetDestination(Player.position);
        if (Player != null) {
            enemy.SetDestination(Player.position);
        }
    }
}
