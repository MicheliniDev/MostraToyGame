using UnityEngine;

public class FimDeFase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Fase finalizada!");
            Time.timeScale = 0f; // Pausa o jogo
        }
    }
}
