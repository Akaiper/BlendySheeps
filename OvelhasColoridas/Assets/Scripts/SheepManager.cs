using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SheepManager : MonoBehaviour
{
	public GameObject ovelhaVermelha;
	public GameObject ovelhaVerde;
	public GameObject ovelhaAzul;

	private GameObject filhozao;
	private Transform ovelhaSemLa;
	private GameObject pai1 = null;
	private GameObject pai2 = null;
	private Vector4 corPai1;
	private Vector4 corPai2;
	private Color filhinho;
	private Vector3 posPai1;
	private Vector3 posPai2;

	private float tempoTouch = 0;
	RaycastHit hit;

	void Start()
	{


		ovelhaVerde.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(0, 1, 0);
		ovelhaAzul.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(0, 0, 1);
		ovelhaVermelha.GetComponentInChildren<SkinnedMeshRenderer>().material.color = new Color(1, 0, 0);
	}

	// Update is called once per frame
	void Update()
	{
		TestaTouch();
	}

	private void TestaTouch()
	{
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Stationary)
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);

				Debug.Log("entrou no touch");

				if (Physics.Raycast(ray, out hit, 1000))
				{
					if (hit.transform.gameObject.CompareTag("Ovelha"))
					{
						Debug.Log("Unico?");

						tempoTouch += Time.deltaTime;

						hit.transform.gameObject.GetComponent<OvelhaAI>().IsBeingTouched = true;

						if (tempoTouch >= 3)
						{
							FazFilhinho();

							Debug.Log("Touch ok");

							tempoTouch = 0;
						}

					}
				}
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				hit.transform.gameObject.GetComponent<OvelhaAI>().IsBeingTouched = false;

				tempoTouch = 0;
			}
		}
	}

	private void FazFilhinho()
	{
		

		if (pai1 == null)
		{
			pai1 = hit.transform.gameObject;
		}

		else
		{
			pai2 = hit.transform.gameObject;

			CriarFilhinhos();

			pai1 = null;
			pai2 = null;
		}
		
	}

	private void CriarFilhinhos()
	{
		corPai1 = pai1.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
		corPai2 = pai2.GetComponentInChildren<SkinnedMeshRenderer>().material.color;


		posPai1 = pai1.transform.position;
		posPai2 = pai2.transform.position;

		filhinho.r = Mathf.Round((1 - (1 - corPai1.x / 255) * (1 - corPai2.x / 255)) * 255);
		filhinho.g = Mathf.Round((1 - (1 - corPai1.y / 255) * (1 - corPai2.y / 255)) * 255);
		filhinho.b = Mathf.Round((1 - (1 - corPai1.z / 255) * (1 - corPai2.z / 255)) * 255);

		filhozao = Resources.Load<GameObject>("Ovelha");

		filhozao = Instantiate(filhozao);

		ovelhaSemLa = filhozao.transform.GetChild(1);

		ovelhaSemLa.gameObject.SetActive(false);

		filhozao.transform.position = (posPai1 + posPai2) / 2;

		filhozao.GetComponentInChildren<SkinnedMeshRenderer>().material.color = filhinho;
	}
}
