using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercasManager : MonoBehaviour
{

	private GameObject cerca;
	private ButtonManager buttonManager;
	private Vector3 position_new;

	private bool rotacao = false;

	private bool emAndamento = false;

	public Save save;


	// Start is called before the first frame update
	void Start()
    {
		position_new.y = 1;

		buttonManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ButtonManager>();
	}

    // Update is called once per frame
    void Update()
    {
		if (buttonManager.colocarCerca && !emAndamento)
		{
			ColocarCercas();
		}
    }

	private void ColocarCercas()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);

				if (Physics.Raycast(ray, out RaycastHit hit, 1000))
				{
					if (hit.transform.gameObject.CompareTag("GridCollider"))
					{
						cerca = Resources.Load<GameObject>("cerca");

						cerca = Instantiate(cerca);

						buttonManager.cercasYesNo.SetActive(true);

						position_new.x = hit.transform.position.x;
						position_new.z = hit.transform.position.z;

						cerca.transform.position = position_new;

						rotacao = false;

						emAndamento = true;
					}
				}
			}
		}
	}

	public void BtnYes() //confirma a compra da cerca
	{
		if (save.dinheiro >= 10)
		{
			save.dinheiro -= 10; //desconta o dinheiro
			SaveCercaPosition(); //salva a posiçao e a rotacao da cerca
			buttonManager.colocarCerca = false; //impede de entrar no loop de verificaçao do touch
			emAndamento = false; //indica que a compra de cercas não está em andamento 
			buttonManager.grid.SetActive(false); //desativa a grid de posiçao das cercas e os colisores junto
			buttonManager.cercasYesNo.SetActive(false); //desativa os botoes de confirmas, rotacionar e cancelar
			buttonManager.painelDeBaixo.SetActive(true); //ativa novamente os botoes de comprar cerca e ver quests/ovelhas
		}
		else
		{
			//fazer piscar o dinheiro, indicando dinheiro insuficiente
		}
		
	}

	public void BtnRotacionar()
	{
		if (!rotacao)
		{
			rotacao = true;

			cerca.transform.Rotate(0.0f, 90.0f, 0.0f, Space.World); //rotaciona em 90° no eixo y
		}
		else
		{
			rotacao = false;

			cerca.transform.Rotate(0.0f, -90.0f, 0.0f, Space.World); //rotaciona de volta para a rotaçao inicial
		}
	}

	public void BtnCancel()
	{
		Destroy(cerca); //destroi a cerca instanciada quando a compra é cancelada
		buttonManager.colocarCerca = false;
		emAndamento = false;
		buttonManager.grid.SetActive(false);
		buttonManager.cercasYesNo.SetActive(false);
		buttonManager.painelDeBaixo.SetActive(true);
	}

	private void SaveCercaPosition()
	{
		save.dadosCercas[save.cercas].posCercas = cerca.transform.position; //salva a posiçao da cerca
		save.dadosCercas[save.cercas].rotacaoCercas = cerca.transform.rotation; //salva a rotacao da cerca
		save.cercas++; //aumenta o numero de cercas compradas (é a referencia de posicao no array)
	}
}