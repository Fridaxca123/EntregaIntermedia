using UnityEngine;
public class Bullet : MonoBehaviour
{
    //constante para que la bala desaparesca
    private const float max_life_time = 3f;
    //tiempo de vida de la bala(en que empieza)
    private float _lifeTime = 0f;
    public Vector2 Velocity;
    public static int ActiveBullets = 0;  // contador de balas
    private void OnEnable()
    { ActiveBullets++;}
    private void OnDisable()
    { ActiveBullets--;}
    private void Update()
    {
        //Velocidad y direccion en la que se movera en cada fotograma
        transform.position += (Vector3)Velocity * Time.deltaTime;
        //agregar a cada fotograma
        _lifeTime += Time.deltaTime;
        //una vez que lifetime sea mayor a la constante desactivamos bala 
        if (_lifeTime > max_life_time)
            Disable();
    }

    //funcion diable destruye objeto
    private void Disable()
    {
        Destroy(gameObject);
    }
}
