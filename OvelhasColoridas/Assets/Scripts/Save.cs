using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save")]
public class Save : ScriptableObject 
{
    public int dinheiro;
	public float felicidadeOvelhas;
	public float fomeOvelhas;
	public float sedeOvelhas;
	public string quest;
	public Vector4[] corOvelhasAtuais;
	public Vector4[] corOvelhasVistas;
	public int cercas;
	public int qtdCachorro;

	public struct quests 
	{
		public int[] num;
		public string[] enunciado;
		public string[] complemento;
	}

	public quests frases;


	public struct Cercas 
	{
		public Vector3 posCercas;
		public Quaternion rotacaoCercas;
	}

	public Cercas[] dadosCercas;
}
