using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public Hero hero;
        public Slot slotPrefab;
        private Slot[] slots;
        int previousCount = 0;

        private void Start()
        {
            slots = gameObject.GetComponentsInChildren<Slot>();
            previousCount = slots.Length;
        }

        void LateUpdate()
        {
            if (slots.Length != hero.numberOfWeapons)
                CreateInventorySlots();

            if (previousCount != hero.CollectedWeapon.Count)
                UpdateInventorySlots();
        }

        private void CreateInventorySlots()
        {
            foreach (var item in slots)
                Destroy(item.gameObject);

            slots = new Slot[hero.numberOfWeapons];

            for (int aux = 0; aux < hero.numberOfWeapons; aux++)
            {
                slots[aux] = Instantiate<Slot>(slotPrefab, transform);
                slots[aux].DisableImage();
            }

        }

        private void UpdateInventorySlots()
        {
            //previousCount = hero.CollectedWeapon.Count;

            var counter = 0;
            foreach (var item in hero.CollectedWeapon)
            {
                var slot = slots[counter];
                slot.SetImage(item);
                counter++;
            }

            //var arr = hero.CollectedWeapon.ToArray();

            for (int aux = counter; aux < slots.Length; aux++)
            {
                var slot = slots[aux];
                slot.DisableImage();
            }
        }
    }

}