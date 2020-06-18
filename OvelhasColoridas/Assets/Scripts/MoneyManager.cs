using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{

	private int money;
	public TextMeshProUGUI moneyText;
	public Save save;

    // Start is called before the first frame update
    void Start()
    {
		money = save.dinheiro;
    }

    // Update is called once per frame
    void Update()
    {
		money = save.dinheiro;

		moneyText.SetText("$" + money);
    }
}
