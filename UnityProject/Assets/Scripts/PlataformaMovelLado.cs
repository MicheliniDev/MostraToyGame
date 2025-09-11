using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovelLado : MonoBehaviour
{
    public Vector3 pontoA;  // Posição inicial relativa
    public Vector3 pontoB;  // Posição final relativa
    public float velocidade = 2f; // Velocidade do movimento

    private Vector3 destino;

    private Vector3 posicaoAntiga;
    private List<Transform> passageiros = new();
    void Start()
    {
        // Define o destino inicial como ponto B
        destino = transform.position + pontoB;
        // Ajusta os pontos para serem relativos à posição inicial
        pontoA = transform.position + pontoA;
        pontoB = transform.position + pontoB;
    }

    void Update()
    {
        // Move a plataforma em direção ao destino
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);

        // Se chegou no destino, troca
        if (Vector3.Distance(transform.position, destino) < 0.05f)
        {
            destino = (destino == pontoA) ? pontoB : pontoA;
        }
    }

    private void FixedUpdate()
    {
        Vector3 delta = transform.position - posicaoAntiga;

        foreach(var passageiro in passageiros)
        {
            if (passageiro)
            {
                passageiro.position += delta;
            }
        }
        posicaoAntiga = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!passageiros.Contains(collision.transform))
        {
            passageiros.Add(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (passageiros.Contains(collision.transform))
        {
            passageiros.Remove(collision.transform);
        }
    }
}

