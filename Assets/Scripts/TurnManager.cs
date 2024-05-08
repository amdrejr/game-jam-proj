using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public GameObject chickenPrefab;
    public int initialEnemiesPerWave = 3;
    public int enemiesIncreasePerWave = 2;
    public float spawnInterval = 2f;
    public float waveInterval = 15f; // Intervalo entre ondas
    private int currentWave = 1;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    // Pontos de spawn nos cantos do mapa
    public Transform[] spawnPoints;

    // Referências para os objetos de texto na UI
    public Text textAlert;

    private void Start() {
        StartNextWave();
    }

    private void StartNextWave() {
        int enemiesToSpawn = initialEnemiesPerWave + (currentWave - 1) * enemiesIncreasePerWave;
        print("Iniciando Wave: " + enemiesToSpawn + " Inimigos");
        StartCoroutine(SpawnWave(enemiesToSpawn));
    }

    private IEnumerator SpawnWave(int enemiesToSpawn) {
        StartCoroutine(ShowAndHideMessage(textAlert, "O ataque começou!", 3f));
        for (int i = 0; i < enemiesToSpawn; i++) {
            // Escolhe aleatoriamente um ponto de spawn nos cantos do mapa
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

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

        // Chama a função para spawnar a galinha
        print("spawnando galinha");
        SpawnChicken();

        yield return new WaitForSeconds(waveInterval);
        currentWave++;

        StartNextWave();
    }

    private void Update() {
        CheckEnemyStatus();
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
    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

    // Spawn da galinha no ponto selecionado
    Instantiate(chickenPrefab, randomSpawnPoint.position, Quaternion.identity);
}
}
