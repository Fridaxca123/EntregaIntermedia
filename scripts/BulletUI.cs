using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class BulletUI : MonoBehaviour
{
    [SerializeField] private TMP_Text bulletText; 

    private void Update()
    {
        bulletText.text = "Balas: " + Bullet.ActiveBullets;
    }
}

