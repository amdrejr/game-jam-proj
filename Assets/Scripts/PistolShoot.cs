using Unity.VisualScripting;
using UnityEngine;

public class PistolShoot : MonoBehaviour {
    public GameObject projectilePrefab;
    public int damageAmount = 30;
    private float projectileSpeed = 50f;
    private float fireRate = 0.75f;
    private float nextFireTime;

    public GameObject weapon;
    private Transform firePoint;
    public AudioClip shootSound;

    private void Awake() {
        // Atribuir automaticamente o Transform do objeto ao qual o script está anexado ao firePoint
        firePoint = GetComponentInParent<Transform>();
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
        // Calcula a posição de início do projétil um pouco à frente do firePoint
        Vector3 startProjectilePosition = firePoint.position; // Ajuste a distância conforme necessário

        // Criar um novo projétil e instanciá-lo na posição ajustada
        GameObject projectile = Instantiate(projectilePrefab, startProjectilePosition, firePoint.transform.rotation);

        // Destruir o projétil após 3 segundos
        Destroy(projectile, 3f);

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

        // Adicionar força ao projétil na direção em que o jogador está olhando
        rb.velocity = firePoint.transform.forward * projectileSpeed;

        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
    }
}

