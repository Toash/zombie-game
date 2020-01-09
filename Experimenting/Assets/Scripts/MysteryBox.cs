using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    Animator _lidAnim;
    Animation _gunUp;
    public bool openBox;
    public GameObject[] _guns;
    // Start is called before the first frame update
    void Start()
    {
        _lidAnim = GetComponentInChildren<Animator>();
        _gunUp = GetComponentInChildren<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openBox == true)
        {
            OpenMysteryBox();
            openBox = false;
        }
        else if (_gunUp.IsPlaying("GunMysteryBox_anim"))
        {

        }
        else
        {

        }
    }
    void OpenMysteryBox()
    {
        OpenLid();
        GunUp();
    }
    void OpenLid()
    {
        _lidAnim.Play("OpenLid_anim");
    }
    void CloseLid()
    {
        _lidAnim.Play("CloseLid_anim");
    }
    void GunUp()
    {
        _gunUp.Play();
    }
}

