using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

	public AudioSource click;

	//Botoes & paineis do MENU
	public GameObject BtnIniciar;
	public GameObject BtnOpcoes;
	public GameObject BtnAbout;
	public GameObject PainelSettings;
	public GameObject PainelAbout;


	// Start is called before the first frame update
	void Start()
    {
		PainelSettings.SetActive(false);
		PainelAbout.SetActive(false);

	}

	// Update is called once per frame
	void Update()
    {
        
    }


	//Abaixo tem comandos do MENU APENAS
	public void BtnStart()
	{
		click.Play();
		SceneManager.LoadScene(1);
	}

	public void BtnSettings()
	{
		click.Play();
		BtnIniciar.SetActive(false);
		BtnOpcoes.SetActive(false);
		BtnAbout.SetActive(false);
		PainelSettings.SetActive(true);
	}

	public void CloseSettings()
	{
		click.Play();
		PainelSettings.SetActive(false);
		BtnIniciar.SetActive(true);
		BtnOpcoes.SetActive(true);
		BtnAbout.SetActive(true);

	}


	public void BtnSobre()
	{
		click.Play();
		BtnIniciar.SetActive(false);
		BtnOpcoes.SetActive(false);
		BtnAbout.SetActive(false);
		PainelAbout.SetActive(true);
	}

	public void CloseAbout()
	{
		click.Play();
		PainelAbout.SetActive(false);
		BtnIniciar.SetActive(true);
		BtnOpcoes.SetActive(true);
		BtnAbout.SetActive(true);
	}
}
