using UnityEngine;

public class PlataformaMovel : MonoBehaviour
{
    public float velocidade = 2f; // Velocidade do movimento
    public float distancia = 3f;  // Distância máxima para cima e para baixo

    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        // Calcula o novo Y com base no Mathf.PingPong
        float novaPosY = posicaoInicial.y + Mathf.PingPong(Time.time * velocidade, distancia);
        transform.position = new Vector3(posicaoInicial.x, novaPosY, posicaoInicial.z);
    }
}
