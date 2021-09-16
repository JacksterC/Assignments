using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jack_Clomen_prg_fun : ProcessingLite.GP21
{
	
		//Scroller variables
	
		float scrollstep;
		float totscrstps;
		float actscrstps;
		float oper;
		float fx;
		
		//Cube and DrawLines variables
		
		float[] startingpoint = {40, 1f}; // The base (bottom corner) start coordinates for the cube
		
		float cubesidelength = 10f;		// virtual length of cube side
		
		float spacing = 5; // distance between lines
		
		float xi;
		float yi;
		float yi2;
		float xi2;
		

		
		float xrel;
		float yrel;
		
		float xpos1;
		float xpos2;
		
		float ypos1;
		float ypos2;
		float ypos3;
		float ypos4;
		
		double dblxrel;
		double dblyrel;
		
		int colorr;
		
		int linecntr = 1;
		
	
    // Start is called before the first frame update
	void Start()
    {
		
		//Frame rate settings
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 20;
		
		Background(1,1,1);

		//Scroller variables
		scrollstep = 1;
		totscrstps = scrollstep * 4;
		actscrstps = scrollstep / 20;
		
		oper = 1;
		fx = actscrstps;
		
		//Cube and DrawLines variables
		dblxrel = Math.Round(Math.Sqrt(cubesidelength * cubesidelength / 1.8), 1);
		dblyrel = Math.Round(Math.Sqrt(cubesidelength * cubesidelength / 2.25), 1);

		xrel = Convert.ToSingle(dblxrel);
		yrel = Convert.ToSingle(dblyrel);
		
		xpos1 = startingpoint[0] + xrel;
		xpos2 = startingpoint[0] - xrel;
		
		ypos1 = startingpoint[1] + yrel * 0.75f;
		ypos2 = startingpoint[1] + yrel * 1.5f;
		ypos3 = startingpoint[1] + yrel * 2.25f;
		ypos4 = startingpoint[1] + yrel * 3;
		
		xi = startingpoint[0];
		yi = startingpoint[1];
		
		xi2 = startingpoint[0];
		yi2 = ypos1;
		
		colorr = 0;
		
	}

    // Update is called once per frame
    void Update()
    {
      Scroller();
	  DrawLines();
	  Cube();
    }
	
	void Scroller()
	{
	
		if (fx >= totscrstps - 1)
		{
			oper = -1;
		}
		if (fx <= 1)
		{
			oper = 1;
		}
		
		fx = fx + actscrstps*oper;
				
		//Erase previous letters before drawing new ones
		
		Stroke(1,1,1);
		
		LetterJ(fx - actscrstps*oper);
		LetterA(fx - actscrstps*oper);
		LetterC(fx - actscrstps*oper);
		LetterK(fx - actscrstps*oper);
		
		//Draw letters
		
		Stroke(255,51,0);
					
		LetterJ(fx);
		LetterA(fx);
		LetterC(fx);
		LetterK(fx); 
	}
	void LetterJ(float ft = 0)
	{
		Line(3, ft+7, 3, ft+4);
		Line(3, ft+4, 2, ft+3);
		Line(2, ft+3, 1, ft+4);
	}
	void LetterA(float ft = 0)
	{
		Line(5, ft+7, 4, ft+3);
		Line(5, ft+7, 6, ft+3);
		Line(4, ft+5, 6, ft+5);
	}
	void LetterC(float ft = 0)
	{
		Line(7, ft+6, 9, ft+6);
		Line(7, ft+3, 9, ft+3);
		Line(7, ft+3, 7, ft+6);
	}
	void LetterK(float ft = 0)
	{
		Line(10, ft+7, 10, ft+3);
		Line(10, ft+5, 12, ft+7);
		Line(10, ft+5, 12, ft+3);
	}
	void Cube()
	{
		Stroke(255,0,255);
		
		//Draw cube
		
		Line(startingpoint[0], ypos2, xpos2, ypos3);
		Line(xpos2, ypos3, startingpoint[0], ypos4);
		Line(startingpoint[0], ypos2, xpos1, ypos3);
		Line(xpos1, ypos3, startingpoint[0], ypos4);
		
		Line(startingpoint[0], ypos2, startingpoint[0], startingpoint[1]);
		Line(xpos2, ypos3, xpos2, ypos1);
		Line(xpos1, ypos3, xpos1, ypos1);
		
		Line(xpos2, ypos1, startingpoint[0], startingpoint[1]);
		Line(xpos1, ypos1, startingpoint[0], startingpoint[1]);
	}
	void DrawLines()
	{	
		if (xi <= xpos1)
		{
			colorr = colorr + Convert.ToInt32(xi*spacing/20); // Changes color nuance based on number of lines (distance between lines) drawn
			Stroke(255, colorr, 255);
			
			//Changing line colours using Modulus
			
			if (linecntr % 3 == 2)
			{
				Stroke(255,255,0);
			}
			linecntr++;
			
			//Draw parabolic lines that fit in the cube
			
			Line(xi, yi, xpos1, yi2);
			Line(xi2, yi, xpos2, yi2);
			Line(startingpoint[0], ypos2 - yi2 + ypos1, xi, yi);
			Line(startingpoint[0], ypos2 - yi2 + ypos1, xi2, yi);
			
			Line(startingpoint[0], ypos1 + yi2 - ypos2 , xi, yi + ypos2 - startingpoint[1]);
			Line(startingpoint[0], ypos1 + yi2 - ypos2 , xi2, yi - startingpoint[1] + ypos2);
			Line(xi,  yi - startingpoint[1] + ypos2, xpos1, ypos3 + ypos1 - yi2);
			Line(xi2, yi - startingpoint[1] + ypos2, xpos2, ypos3 + ypos1 - yi2);
			
			Line(xi, yi + ypos2 - startingpoint[1], xi2 + xpos1 - startingpoint[0],yi - startingpoint[1] + ypos3);
			Line(xi2, yi +ypos2 - startingpoint[1], xi + xpos2 - startingpoint[0], yi + ypos3 - startingpoint[1]);
			Line(xi + xpos2 - startingpoint[0], yi + ypos3 - startingpoint[1], xi, ypos3 - yi + ypos1);
			Line(xpos1 + startingpoint[0] - xi, ypos2 - yi + ypos1, xi2, yi +ypos2 - startingpoint[1]);
			
			float spacing2 = 100 / spacing;
			
			xi = xi + xrel / spacing2;
			xi2 = xi2 - xrel / spacing2;
			yi = yi + yrel / spacing2 * 0.75f; // 0.75f is default
			yi2 = yi2 + yrel / spacing2 * 1.5f; //1.5f is default
		}
	}
}
