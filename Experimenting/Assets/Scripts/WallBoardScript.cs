using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class WallBoardScript : MonoBehaviour
    {
        public int boards;
        public Animator[] _boardAnim;
        public GameObject[] _boards;
        // Start is called before the first frame update
        void Start()
        {
            _boardAnim = GetComponentsInChildren<Animator>();
            //creates integer in i, runs while i is less than 6, adds 1 to i
            for (int i = 0; i < 6; i++)
            {
                _boardAnim[i].Play("Board_anim" + (i + 1).ToString());
            }
            boards = 6;
        }
        private void Update()
        {
            if (boards > 0)
            {
                transform.GetComponent<Renderer>().enabled = true;
            }
            else
            {
                transform.GetComponent<Renderer>().enabled = false;
            }
        }

        public void AddBoard()
        {
            if (boards < 6)
            {
                //_boards is the array of boards, boards is the number of boards we have
                _boards[boards].SetActive(true);
                //_boards[boards].SendMessage("EnableBoard",SendMessageOptions.RequireReceiver);
                //_boards[boards].renderer.enabled = true;
                _boardAnim[boards].Play("Board_anim" + (boards + 1).ToString());
                boards += 1;
            }
        }
        public void RemoveBoard()
        {
            if (boards > 0)
            {
                _boards[boards - 1].SendMessage("DisableBoard", SendMessageOptions.RequireReceiver);
                boards -= 1;
            }
        }
    }
