using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuUI;
    public GameObject mainCanvas;
    private bool isPaused = false;

    void Update() {
        // Verificar se o jogador pressionou a tecla Esc
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() {
        // Desativar o menu de pause
        pauseMenuUI.SetActive(false);
        mainCanvas.SetActive(true);
        // Retomar o tempo do jogo
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause() {
        // Ativar o menu de pause
        pauseMenuUI.SetActive(true);
        mainCanvas.SetActive(false);
        // Parar o tempo do jogo
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame() {
        // Retomar o tempo do jogo (caso o jogo esteja pausado)
        Time.timeScale = 1f;

        // Carregar a cena do menu principal
        SceneManager.LoadScene("MenuPrincipal");
    }

    // public void QuitGame() {
    //     // Sair do jogo (funciona no editor e no build)
    //     #if UNITY_EDITOR
    //     UnityEditor.EditorApplication.isPlaying = false;
    //     #else
    //     Application.Quit();
    //     #endif
    // }
}

