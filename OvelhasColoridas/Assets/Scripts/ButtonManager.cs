using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource click;

    //Botoes & paineis GAMEPLAY
    public GameObject menuCeleiro;
	public GameObject menuLivro;
    public GameObject menuPause;
    public GameObject questPanel;
	public GameObject ovelhasDescobertas;
	public GameObject painelDeBaixo;
	public GameObject cercasYesNo;

	public GameObject grid;
	public bool colocarCerca = false;

    // Start is called before the first frame update
    void Start()
    {

		grid = GameObject.FindGameObjectWithTag("Grid");

		Debug.Log(grid.name);

		grid.SetActive(false);

    }

	// Update is called once per frame
	void Update()
	{
		if (!colocarCerca)
		{
			TouchCeleiro();
		}
	}

	public void TouchCeleiro() //função que pega o input do touch e testa se tocou no celeiro
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);

				if (Physics.Raycast(ray, out RaycastHit hit, 1000))
				{
					if (hit.transform.gameObject.CompareTag("Celeiro"))
					{
						menuCeleiro.SetActive(true);
					}
					else
					{
						menuCeleiro.SetActive(false);
					}
				}
			}

		}
	}

	public void BtnLivro()
	{
		if (!menuLivro.activeSelf)
			menuLivro.SetActive(true);
		else
			menuLivro.SetActive(false);
	}

	public void BtnCerca()
	{
		if (!grid.activeSelf)
		{
			grid.SetActive(true);
			colocarCerca = true;
			painelDeBaixo.SetActive(false);
		}
	}

    public void BtnQuest() //Abre janela que contem quests
    {
        click.Play();
        menuLivro.SetActive(false);
        questPanel.SetActive(true);
    }

    public void OkBtnQuest() //Fecha janela que contem quests
    {
        click.Play();
        questPanel.SetActive(false);
    }

    public void BtnOvelha()
    {
        click.Play();
        menuLivro.SetActive(false);
        ovelhasDescobertas.SetActive(true);
    }

    public void OkBtnOveia() //Fecha janela que contem quests
    {
        click.Play();
        ovelhasDescobertas.SetActive(false);
    }

    public void BtnPause()
    {

        if (!menuPause.activeSelf)
        {
            Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
            menuPause.SetActive(true);
        }
        else {
            Time.timeScale = 1f;
            menuPause.SetActive(false);
        }
    }

    public void BtnReturnMenu()
    {
        click.Play();
        SceneManager.LoadScene(0);
    }

}
