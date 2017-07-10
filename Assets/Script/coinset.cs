using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinset : MonoBehaviour {

    private Text CoinText;
    private int num = 0;

	// Use this for initialization
	void Start () {
        CoinText = GameObject.Find("Canvas/coin").GetComponent<Text>();
	}
	
    public void Add(int tmp)
    {
        num += tmp;
    }

    public void Show()
    {
        CoinText.text = " " + num;
    }

	// Update is called once per frame
	void Update () {
        Show();
	}
}
