using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

public class Jack_Clomen_prg_fun : ProcessingLite.GP21
{
    // Start is called before the first frame update
    void Start()
    {
		//Line(4, 7, 4, 3);
		//Line(4, 5, 6, 5);
		//Line(6, 7, 6, 3);
	
		//Line(8, 5.5f, 8, 3);
		//Line(8, 7, 8, 6.8f);

		Background(1,1,1);
		
		float scrollstep = 2;					// defines how many pixels the letters will scroll each time
		float totscrstps = scrollstep * 10;
		float actscrstps = scrollstep/10;
		
		for (float fx = actscrstps; fx < totscrstps; fx = fx + actscrstps)
		{
			string tg = fx.ToString("0.0");
			fx = float.Parse(tg);
				
			//Stroke(1,1,1);	// This and the follwoing line erases previously drawn lines but since the script is running too fast u can't see the scrolling anyway
				
			LetterJ(fx - actscrstps);
			LetterA(fx - actscrstps);
			LetterC(fx - actscrstps);
			LetterK(fx - actscrstps);
		
			Stroke(255,51,0);
					
			LetterJ(fx);
			LetterA(fx);
			LetterC(fx);
			LetterK(fx);
				
			
			//Thread.Sleep(20);
			//for (int counter = 0; counter <= 1000000; counter++)
			//{
			//}
		}
		for (float fx = totscrstps; fx > 0; fx = fx - actscrstps)
		{	
			string tg = fx.ToString("0.0");
			fx = float.Parse(tg);
				
			//Stroke(1,1,1);	// This and the following lines erases previously drawn lines but since the script is running too fast u can't see the scrolling anyway
				
			LetterJ(fx + actscrstps);
			LetterA(fx + actscrstps);
			LetterC(fx + actscrstps);
			LetterK(fx + actscrstps);
				
			Stroke(255,51,0);
					
			LetterJ(fx);
			LetterA(fx);
			LetterC(fx);
			LetterK(fx);
				
			//Thread.Sleep(20);
			//for (int counter = 0; counter <= 1000000; counter++)
			//{
			//}
		}
	}
	

    // Update is called once per frame
    void Update()
    {

    }
	void LetterJ(float ft)
	{
		Line(3, ft+7, 3, ft+4);
		Line(3, ft+4, 2, ft+3);
		Line(2, ft+3, 1, ft+4);
	}
	void LetterA(float ft)
	{
		Line(5, ft+7, 4, ft+3);
		Line(5, ft+7, 6, ft+3);
		Line(4, ft+5, 6, ft+5);
	}
	void LetterC(float ft)
	{
		Line(7, ft+6, 9, ft+6);
		Line(7, ft+3, 9, ft+3);
		Line(7, ft+3, 7, ft+6);
	}
	void LetterK(float ft)
	{
		Line(10, ft+7, 10, ft+3);
		Line(10, ft+5, 12, ft+7);
		Line(10, ft+5, 12, ft+3);
	}
	
}
