using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Jack_Clomen_mouse_circle : ProcessingLite.GP21
{
	
		public Vector2 cP;
		float diam = 3;
		Vector2 tmpV;
		Vector2 direct;
		Vector2 tmpV2;
		float magnit;
		Vector2 spd;
		
		int flsf1 = 1;
		int flsf2 = 1;
		int flsf3 = 1;
		int flsf4 = 1;
		
		float tmpMX = 1;
		float tmpMY = 1;
		

	
    // Start is called before the first frame update
    void Start()
    {
		//Frame rate settings
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 60;
		
		Background(1,1,1);
		
		cP.x = 5;cP.y = 5;
		Circle(cP.x, cP.y, diam);
		
		tmpV.x = 0; tmpV.y = 0;
		direct.x = 0; direct.y = 0;
		

		
    }

    // Update is called once per frame
    void Update()
    {

		
		if (Input.GetMouseButtonDown(0))
		{
			Stroke(0, 0, 0);
			
			Circle(cP.x, cP.y, diam+1);
			
			
			Stroke(255, 255, 255);
			
			if(MouseX - diam/2 >= 0.1f)
			{
				if(MouseX + diam/2 <= 19.5f)
				{
					if(MouseY - diam/2 >= 0.3f)
					{		
						if(MouseY + diam/2 <= 10)
						{	
							cP.x = MouseX;
							cP.y = MouseY;
							Circle(cP.x, cP.y, diam);
						}
					}
				}
			}
		}
		if (Input.GetMouseButton(0))
		{			
			Stroke(0, 0, 0);
			StrokeWeight(2);
			Line(tmpV2.x, tmpV2.y, tmpMX, tmpMY);
			tmpV2 = cP;
			tmpMX = MouseX;
			tmpMY = MouseY;
			
			Stroke(255, 0, 255);
			StrokeWeight(1);
			Line(tmpV2.x, tmpV2.y, MouseX, MouseY);
			flsf1 = 1;
			flsf2 = 1;
			flsf3 = 1;
			flsf4 = 1;
			
			Stroke(255, 51, 0);
			Circle(cP.x, cP.y, diam);
		}
		if (Input.GetMouseButtonUp(0))
		{
			
			tmpV.x = MouseX;
			tmpV.y = MouseY;
			
			direct = (cP - tmpV).normalized;
		
			magnit = (cP - tmpV).magnitude;
			
			spd = direct * magnit / 100;
			
		}
		
		if (Input.GetMouseButton(0))
		{
		}
		else
		{
			Stroke(0, 0, 0);
			Circle(cP.x, cP.y, diam + 0.2f);
			
			cP = cP - spd;
		
			Stroke(255, 255, 255);
			Circle(cP.x, cP.y, diam);
			
			
			if(cP.x - diam/2 <= 0)
			{
				if(flsf1 == 1)
				{
					flsf1 = 2;
					flsf2 = 1;
					
					spd.x = -spd.x;
				}
			}
			if(cP.x + diam/2 >= 19.5f)
			{
				if(flsf2 == 1)
				{
					flsf2 =2;
					flsf1 =1;
					
					spd.x = -spd.x;
				}
			}
			if(cP.y - diam/2 <= 0)
			{
				if(flsf3 == 1)
				{
					flsf3 = 2;
					flsf4 = 1;
					
					spd.y = -spd.y;
				}
			}
			if(cP.y + diam/2 >= 10)
			{
				if(flsf4 == 1)
				{
					flsf4 = 2;
					flsf3 = 1;
					
					spd.y = -spd.y;
				}
			}
		}
    }
}
