using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{

    public class Slot : MonoBehaviour
    {
        private Image slotImage;
        private Text slotText;
        private Button slotButton;

        private void Awake()
        {
            slotButton = gameObject.GetComponent<Button>();
            slotImage = gameObject.GetComponentsInChildren<Image>()[1];
            slotText = gameObject.GetComponentInChildren<Text>();

        }

        internal void SetImage(Sprite sprite)
        {
            slotImage = gameObject.GetComponentsInChildren<Image>()[1];
            slotImage.sprite = sprite; 
        }

        internal void SetImage(Weapon weapon)
        {
            EnableOrDisable(true);

            
            slotImage.sprite = GetWeaponSprite(weapon);

            slotText.text = GetWeaponText(weapon);
        }

        private string GetWeaponText(Weapon weapon)
        {
            return weapon.durability.ToString();
        }

        private Sprite GetWeaponSprite(Weapon weapon)
        {
            return null;
            //return weapon.WeaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        }

        internal void DisableImage()
        {
            EnableOrDisable(false);
        }

        private void EnableOrDisable(bool enable)
        {
            slotButton.enabled = enable;
            slotButton.interactable = enable;
            slotText.enabled = enable;
            slotImage.enabled = enable;
        }

    }

}