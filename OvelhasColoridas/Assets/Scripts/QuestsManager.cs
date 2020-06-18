using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestsManager : MonoBehaviour
{
	public TextMeshProUGUI texto;

	public Quests quests;

	public struct Quest
	{
		public string enunciados;
		public string complementos;
	}

	public Quest[] frases;

	private bool questCompleted;

	private string questAtual;

	private int rand;

	private int numero;

	public Save save;

    // Start is called before the first frame update
    void Start()
    {
		frases[0].enunciados = "Crie ";
		frases[1].enunciados = "Alimente as Ovelhas ";
		frases[2].enunciados = "Tose as Ovelhas ";
		frases[3].enunciados = "Brinque com as Ovelhas ";

		frases[0].complementos = " novas Ovelhas";
		frases[1].complementos = " vezes";
		frases[2].complementos = " vezes";
		frases[3].complementos = " vezes";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void NewQuest()
	{
		rand = Random.Range(0, 4);

		questAtual = frases[rand].enunciados + numero + frases[rand].complementos;

		texto.SetText(questAtual);
	}
}
