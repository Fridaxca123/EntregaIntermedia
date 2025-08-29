using UnityEngine;
using System.Collections;

public class ShootTest : MonoBehaviour
{
    // Prefab de la bala 
    [SerializeField] private Bullet bulletPrefab;

    // Tiempo entre disparo
    [SerializeField] private float shootCooldown = 0.2f;

    // Velocidad de las balas
    [SerializeField] private float bulletSpeed = 10f;
    //donde sale la bala
    [SerializeField] private Transform firePoint;
    //para la flor
    [SerializeField] private int Filas = 200;
    //para el espiral
    //rotacion
    private float espiralAngleOffset = 0f;
    //velocidad del espiral
    [SerializeField] private float espiralRotationSpeed = 50f;

    //incrementa el angulo con el tiempo
    private void Update()
    {
        espiralAngleOffset += espiralRotationSpeed * Time.deltaTime;
    }

    // Método cuando se habilita
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    // Método cuando se deshabilita
    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    // Método que verifica la hora actual cuando cambia el minuto
    private void TimeCheck()
    {
        if (TimeManager.Hour == 10 && TimeManager.Minute == 01)
        {
            // Dispara por 5f
            StartCoroutine(ShootPatternSequence());
        }
    }

    // Corrutina para turnos del disparo
    private IEnumerator ShootPatternSequence()
    {
        float timer = 0f;
        while (timer < 5f)
        {
            Lleno();
            yield return new WaitForSeconds(shootCooldown);
            timer += shootCooldown;
        }

        while (timer > 5f && timer < 8f)
        {
            Flor();
            yield return new WaitForSeconds(shootCooldown);
            timer += shootCooldown;
        }
        while (timer > 8f && timer < 14f)
        {
            Espiral();
            yield return new WaitForSeconds(shootCooldown);
            timer += shootCooldown;
        }
    }

    private void Flor()
    {
        for (int i = 0; i < Filas; i++)
        {
            // Ángulo 
            float Angulo= (2 * Mathf.PI * i) / Filas;

            // Formula de estrella
            float r = Mathf.Sin(5 * Angulo);

            // Pasar a coordenadas cartesianas
            float x = r * Mathf.Cos(Angulo);
            float y = r * Mathf.Sin(Angulo);

            Vector3 dir = new Vector3(x, y, 0);

            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Velocidad
            bullet.Velocity = dir * bulletSpeed;
        }
    }

    private void Lleno()
    {
        int Filas = 20;
        float Angulo = 360f / Filas;

        for (int i = 0; i < Filas; i++)
        {
            float angle = i * Angulo;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Bullet newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            newBullet.Velocity = rotation * Vector3.up * bulletSpeed;
        }
    }
    
    private void Espiral()
    {
        int puntos = 36; // balas
        float radioInicial = 0.3f;
        float radioFinal = 2f;
        float vueltas = 3f;
        
        for (int i = 0; i < puntos; i++)
        {
            // hace girar el espiral
            float angle = espiralAngleOffset + (2 * Mathf.PI * vueltas * i) / puntos;
            
            // Radio que crece desde el centro hacia afuera
            float t = (float)i / puntos;
            float r = Mathf.Lerp(radioInicial, radioFinal, t);
            
            // Coordenadas cartesianas
            float x = r * Mathf.Cos(angle);
            float y = r * Mathf.Sin(angle);
            
            Vector3 dir = new Vector3(x, y, 0).normalized;
            
            Bullet bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.Velocity = dir * bulletSpeed;
        }
    }

}