using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class WeaponSwitching : MonoBehaviour
    {
        public int selectedWeapon = 0;
        private UIManager _uiManager;
        // Start is called before the first frame update
        void Start()
        {
            _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
            _uiManager.FindGunReference();
        }

        // Update is called once per frame
        void Update()
        {
            int previousSelectedWeapon = selectedWeapon;

            //if scroll up
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                //if selected weapon is greater than all the children minus one
                if (selectedWeapon >= transform.childCount - 1)
                    //equal it to 0
                    selectedWeapon = 0;
                //if not then add the index by 1
                else
                    selectedWeapon++;
            }
            //if scroll down
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                //if selected weapon is less than or equal to 0
                if (selectedWeapon <= 0)
                    //set the selected weapon to the amount of children minus one
                    selectedWeapon = transform.childCount - 1;
                //if not that minus the index by 1
                else
                    selectedWeapon--;
            }

            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
                _uiManager.FindGunReference();
            }
        }
        void SelectWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                if (i == selectedWeapon)
                    weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
                i++;
            }
        }
    }
}
