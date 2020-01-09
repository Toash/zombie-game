using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Door : MonoBehaviour
    {
        private Animator anim;
        private Player _player;
        private UIManager _uimanager;
        public int _costToOpen;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            _player = GameObject.Find("Player").GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError("Did not get player component");
            }
            _uimanager = GameObject.Find("UI").GetComponent<UIManager>();
            if (_uimanager == null)
            {
                Debug.LogError("Did not get ui manager component");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OpenDoor()
        {
            if (_player._points >= _costToOpen)
            {
                _uimanager.UpdatePoints(_player._points -= _costToOpen);
                StartCoroutine(OpenDoorSequence());
            }
        }
        IEnumerator OpenDoorSequence()
        {
            anim.SetBool("isOpening", true);
            yield return new WaitForSeconds(2);
            anim.SetBool("isOpening", false);

        }
    }
}
