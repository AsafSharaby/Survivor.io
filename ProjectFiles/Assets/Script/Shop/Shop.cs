using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public static Shop instance;

	public List<Item> shopItems;
	public GameObject shopItemPrefab;

	public int counter = 0;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Start()
	{
		PopulateShopItems();
	}

	void PopulateShopItems()
	{
		for (int i = 0; i < shopItems.Capacity; i++)
		{
			if (i < counter)
			{
				GameObject newItem = Instantiate(shopItemPrefab, GameHandler.instance.shopItemContainer);
				newItem.GetComponent<SlotItem>().Initialize(shopItems[i], this);
			}
		}
	}
}

[System.Serializable]
public class Item
{
	public string name;
	public GameObject itemObj;
	public int maxStars = 5;
	public int purchasedStars = 0;
	public Sprite image;
}
