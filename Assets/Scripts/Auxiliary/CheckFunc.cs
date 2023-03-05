using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckFunc : MonoBehaviour
{
    bool check1 = false;
    bool check2 = false;
    bool check3 = false;
    int aux = 0;
    void Update()
    {
        //Chamando as Funções de Check
        if (check1)
		{
			Check1Func();
		}
        if (check2)
        {
            Check2Func();
        }
        if (check3)
        {
            Check3Func();
        }
        
    }
    public void Check1Func()
	{
        //Virando a Carta após a chamada 1
		GameObject.FindWithTag("Check1").transform.Rotate(-180, 0, 0);

        check1 = false;

        aux = 1;
	}

    public void Check2Func()
	{
        //Virando a Carta após a chamada 2
		GameObject.FindWithTag("Check2").transform.Rotate(-180, 0, 0);

        check2 = false;

        aux = 2;
	}

    public void Check3Func()
	{
        //Virando a Carta após a chamada 3
		GameObject.FindWithTag("Check3").transform.Rotate(-180, 0, 0);

        check3 = false;

        aux = 3;
	}

    public void ButtonCheck()
	{
        //Primeira chamada
        if (aux == 0)
        {
		    check1 = true;
        }
        //Segunda chamada
        if (aux == 1)
        {
            check2 = true;
        }
        //Terceira chamada
        if (aux == 2)
        {
            check3 = true;
        }
	}
}
