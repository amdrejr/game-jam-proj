using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLife : MonoBehaviour {
    public Slider Vida;

    // Start is called before the first frame update
    void Start() {
        Vida.value = 100f;
    }

    // Update is called once per frame
    void Update() {
        if(Vida.value <= 0) {
            Debug.Log("Game Over");
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage) {
        Vida.value -= damage;
    }
}
