using System;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovelLado : MonoBehaviour
{
    public Vector3 pontoA;  
    public Vector3 pontoB;  
    public float velocidade = 2f; 

    private Vector3 destino;

    private Vector3 posicaoAntiga;
    private List<Transform> passageiros = new();
    void Start()
    {
        destino = transform.position + pontoB;
        pontoA = transform.position + pontoA;
        pontoB = transform.position + pontoB;
        velocidade = Mathf.Clamp(velocidade, 0.1f, 2.3f);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.fixedDeltaTime);
        
        Vector3 delta = transform.position - posicaoAntiga;

        foreach(var passageiro in passageiros)
        {  
            if (passageiro)
            {
                passageiro.position += delta;
            }
        }
        posicaoAntiga = transform.position;
        
        if (Vector3.Distance(transform.position, destino) < 0.05f)
        {
            destino = (destino == pontoA) ? pontoB : pontoA;
        }
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

