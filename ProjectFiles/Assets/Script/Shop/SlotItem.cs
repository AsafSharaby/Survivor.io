using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
	
	[SerializeField] private Image itemImage;
	[SerializeField] private Text itemNameText;
	[SerializeField] private Button purchaseButton;
	[SerializeField] private Button upgradeButton;
	

	private Item myItem;
	private Shop myShop;
	private GameObject itemPrefab;
	private GameObject currentItem;

	[SerializeField] private Image[] starImages;

	public void Initialize(Item item, Shop shop)
	{
		myItem = item;
		myShop = shop;
		itemPrefab = item.itemObj;

		itemImage.sprite = myItem.image;
		itemNameText.text = myItem.name;

		purchaseButton.onClick.AddListener(PurchaseItem);
		upgradeButton.onClick.AddListener(UpgradeItem);
	}

	void PurchaseItem()
	{
		GameObject temp =  Instantiate(itemPrefab, WeaponHandler.instance.weaponHolder.transform);
		currentItem = temp;
		UpdateStarRating(myItem);
		if (currentItem.GetComponent<PowerUp>().powerUpType == PowerUpType.Guardian)
			currentItem.transform.parent = null;

		purchaseButton.gameObject.SetActive(false);
		upgradeButton.gameObject.SetActive(true);

		GameHandler.instance.shopItemContainer.gameObject.SetActive(false);
	}

	void UpgradeItem()
	{
		UpdateStarRating(myItem);

		if(myItem.purchasedStars <= 5)
		{
			switch (currentItem.GetComponent<PowerUp>().powerUpType)
			{
				case PowerUpType.Kunai:
					currentItem.GetComponent<Weapon>().IncreaseProjectileSpeed();
					break;
				case PowerUpType.Shiled:
					currentItem.GetComponent<ShiledHandler>().IncreaseScale();
					break;
				case PowerUpType.Guardian:
					currentItem.GetComponent<GuardianHandler>().IncreceSpeed();
					currentItem.GetComponent<GuardianHandler>().AddInstance();
					break;
				default:
					break;
			}
		}
		GameHandler.instance.shopItemContainer.gameObject.SetActive(false);

	}

	private void UpdateStarRating(Item item)
	{
		myItem.purchasedStars += 1;
		myItem = item;
		int displayedStars = Mathf.Min(item.purchasedStars, item.maxStars);
		for (int i = 0; i < starImages.Length; i++)
		{
			if (i < displayedStars)
				starImages[i].color = Color.yellow;
			else
				starImages[i].color = Color.grey;
		}
	}
}

