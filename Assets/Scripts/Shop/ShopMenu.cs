using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour {

    String[] item;
    int[] hp, shield, speed, price;

	// Use this for initialization
	void Start () {
        string[] itemList = File.ReadAllLines("ShopItem.txt");
        int size = itemList.Length;
        item = new String[size];
        hp = new int[size];
        shield = new int[size];
        speed = new int[size];
        price = new int[size];

        for(int i=0; i<size; i++)
        {
            String[] itemDetails = itemList[i].Split(',');
            item[i] = itemDetails[0];

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
