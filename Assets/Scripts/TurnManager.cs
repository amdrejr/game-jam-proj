using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject chickenPrefab;
    public int initialEnemiesPerWave = 3;
    public int enemiesIncreasePerWave = 2;
    public float spawnInterval = 2f;
    public float waveInterval = 3f; // Intervalo entre ondas
    private int currentWave = 1;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    public AudioClip[] listAudio;

    // Pontos de spawn nos cantos do mapa
    public Transform[] spawnPointsEnemies;
    public Transform[] spawnPointsChicken;
    public AudioSource audioSource;

    // Referências para os objetos de texto na UI
    public Text textAlert;
    public Text textRound;
    public Text textPoints;
    private int points = 0;

    private void Start() {
        StartNextWave();
        audioSource = GetComponent<AudioSource>();
    }

    private void StartNextWave() {
        int enemiesToSpawn = initialEnemiesPerWave + (currentWave - 1) * enemiesIncreasePerWave;
        print("Iniciando Wave: " + enemiesToSpawn + " Inimigos");
        StartCoroutine(SpawnWave(enemiesToSpawn));
    }

    private IEnumerator SpawnWave(int enemiesToSpawn) {
        StartCoroutine(ShowAndHideMessage(textAlert, "O ataque começou!", 3f));
        audioSource.clip = listAudio[0];
        audioSource.Play();
        for (int i = 0; i < enemiesToSpawn; i++) {
            // Escolhe aleatoriamente um ponto de spawn nos cantos do mapa
            Transform randomSpawnPoint = spawnPointsEnemies[Random.Range(0, spawnPointsEnemies.Length)];

            // Spawn do inimigo no ponto selecionado
            GameObject newEnemy = Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);

            yield return new WaitForSeconds(spawnInterval);
        }

        // Aguarda até que todos os inimigos da onda atual tenham sido derrotados
        while (spawnedEnemies.Count > 0) {
            yield return null;
        }

        StartCoroutine(ShowAndHideMessage(textAlert, "Wave finalizada", 3f));
        audioSource.clip = listAudio[1];
        audioSource.Play();

        // Chama a função para spawnar a galinha
        print("spawnando galinha");
        SpawnChicken();

        yield return new WaitForSeconds(waveInterval);
        currentWave++;
        textRound.text = "ROUND " + currentWave.ToString("D3");

        StartNextWave();
    }

    private void Update() {
        CheckEnemyStatus();
        textPoints.text = "Points " + points.ToString("D5");
    }

    private void CheckEnemyStatus() {
        // Verifica se os inimigos estão vivos e remove aqueles que foram derrotados
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--) {
            if (spawnedEnemies[i] == null) {
                spawnedEnemies.RemoveAt(i);
            }
        }
    }

    private IEnumerator ShowAndHideMessage(Text messageText, string message, float duration) {
        // Mostra a mensagem
        messageText.text = message;
        messageText.enabled = true;

        // Espera o tempo de duração
        yield return new WaitForSeconds(duration);

        // Esconde a mensagem
        messageText.enabled = false;
    }

    private void SpawnChicken() {
        // Escolhe aleatoriamente um ponto de spawn nos cantos do mapa para a galinha
        Transform randomSpawnPoint = spawnPointsChicken[Random.Range(0, spawnPointsChicken.Length)];

        // Spawn da galinha no ponto selecionado
        Instantiate(chickenPrefab, randomSpawnPoint.position, Quaternion.identity);
    }

    public void addPoints(int points) {
        this.points += points;
        print("POINTS: " + this.points);
    }
}
