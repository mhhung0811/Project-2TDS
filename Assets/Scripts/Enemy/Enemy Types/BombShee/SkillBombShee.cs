using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBombShee : MonoBehaviour
{
    private Animator _ani;
	private void Awake()
	{
		_ani = GetComponent<Animator>();
	}

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartScreech()
    {
        _ani.SetBool("IsStart", true);
	}

    public void LoopScreech()
    {
		_ani.SetBool("IsLoop", true);
	}

    public void EndScreech()
    {
		_ani.SetBool("IsStart", false);
		_ani.SetBool("IsLoop", false);
	}

    public void VfxBullet()
    {
        _ani.SetBool("IsBullet", true);
	}
}
