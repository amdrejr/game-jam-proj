using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : MonoBehaviour {
    public GameObject projectilePrefab;
    public int damageAmount = 8;
    public int pelletsCount = 8; // Número de pellets (tiros) do shotgun
    private float projectileSpeed = 50f;
    private float fireRate = 1.2f; // Quantidade de tempo entre cada tiro
    public float spreadAngle = 25f; // Ângulo de dispersão dos pellets
    private float nextFireTime;

    private Transform firePoint; // Ponto de origem dos tiros
    private Transform playerTransform; // Transform do jogador

    private void Awake() {
        // Atribuir automaticamente o Transform do objeto ao qual o script está anexado ao firePoint
        firePoint = GetComponent<Transform>();

        // Obter o Transform do jogador
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update() {
        // Verificar se o jogador pressionou o botão de atirar ou está segurando-o
        bool shouldShoot = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);

        // Verificar se é hora de atirar novamente
        if (shouldShoot && Time.time >= nextFireTime) {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot() {
        // Loop para cada pellet (tiro) do shotgun
        for (int i = 0; i < pelletsCount; i++) {
            // Calcula a direção do tiro com base no ângulo de dispersão
            Quaternion spreadRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f);

            // Calcula a direção do tiro
            Vector3 shootDirection = spreadRotation * firePoint.forward;

            // Calcula a posição de início do projétil um pouco à frente do jogador
            Vector3 startProjectilePosition = playerTransform.position + playerTransform.forward * 0.5f; // Ajuste a distância conforme necessário

            // Criar um novo projétil e instanciá-lo na posição ajustada
            GameObject projectile = Instantiate(projectilePrefab, startProjectilePosition, Quaternion.identity);

            // Destruir o projétil após 3 segundos
            Destroy(projectile, 1f);

            // Obter o componente Projectile do projétil
            if (projectile.TryGetComponent<Projectile>(out var projectileComponent)) {
                projectileComponent.damageAmount = damageAmount;
            }

            // Obter o Rigidbody do projétil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // Se o Rigidbody não estiver presente, adicionar um
            if (rb == null) {
                rb = projectile.AddComponent<Rigidbody>();
            }

            // Adicionar força ao projétil na direção calculada
            rb.velocity = shootDirection * projectileSpeed;
        }
    }
}