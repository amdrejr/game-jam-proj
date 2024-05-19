using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLife : MonoBehaviour {
    public Slider Vida;
    public Canvas canvas;

    private Vector3 pointsPosition;
    private Vector3 roundPosition;

    // Start is called before the first frame update
    void Start() {
        Vida.value = 100f;
    }

    // Update is called once per frame
    void Update() {
        if(Vida.value <= 0) {
            gameOver();
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage) {
        Vida.value -= damage;
    }

    private void gameOver() {
        canvas.transform.Find("Game Over").gameObject.SetActive(true);
        canvas.transform.Find("Slider Life").gameObject.SetActive(false);
        canvas.transform.Find("CabecaLobo").gameObject.SetActive(false);
        canvas.transform.Find("Boss Slider Life").gameObject.SetActive(false);
        canvas.transform.Find("Armas Icons").gameObject.SetActive(false);


        pointsPosition = canvas.transform.Find("Points").gameObject.GetComponent<RectTransform>().position;
        roundPosition = canvas.transform.Find("Round").gameObject.GetComponent<RectTransform>().position;

        canvas.transform.Find("Points").gameObject.GetComponent<RectTransform>().position = new Vector3(640, 290, 0);
        canvas.transform.Find("Round").gameObject.GetComponent<RectTransform>().position = new Vector3(640, 330, 0);
    }

    public void disableGameOverUI() {
        canvas.transform.Find("Game Over").gameObject.SetActive(false);
        canvas.transform.Find("Slider Life").gameObject.SetActive(true);
        canvas.transform.Find("CabecaLobo").gameObject.SetActive(true);
        canvas.transform.Find("Armas Icons").gameObject.SetActive(true);

        canvas.transform.Find("Points").gameObject.GetComponent<RectTransform>().position = new Vector3(1120, 640, 0);
        canvas.transform.Find("Round").gameObject.GetComponent<RectTransform>().position = new Vector3(640, 60, 0);
    }
}
