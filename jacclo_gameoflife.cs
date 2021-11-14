using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jacclo_gameoflife : ProcessingLite.GP21
{
	public float size;
	public int initPercentPopulation;
	
	public int numofXSquares;
	public int numofYSquares;
	
	public bool[,] liveSquares;
	public bool[,] liveSquaresTemp;
	
	public int numofTotalSquares;
	
	public int squareCounter;
		
	
    // Start is called before the first frame update
    void Start()
    {
	   Background(0);
	   
	   	//Frame rate settings
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 10;
		
		size = 0.2f;
		
		// TODO - Only works up to ~10 percent
		initPercentPopulation = 20;
		
		float percentPop = initPercentPopulation * 0.01f;
	   
	   	numofXSquares = (int)(Width / size);
		numofYSquares = (int)(Height / size);
	   
		numofTotalSquares = (int)(numofXSquares * numofYSquares * percentPop);
	   
		int randomX = 0;
		int randomY = 0;
		
		liveSquares = new bool[numofXSquares + 1, numofYSquares + 1];
		liveSquaresTemp = new bool[numofXSquares + 1, numofYSquares + 1];
		
		float x = 0;
		float y = 0;
		
		NoStroke();
		Fill(128);
		
		squareCounter = 0;
		
		while (squareCounter < numofTotalSquares)
		{
			randomX = Random.Range(0, numofXSquares - 1);
			randomY = Random.Range(0, numofYSquares - 1);
			
			if (randomX == 0)
			{
				x = 1;
			}
			else
			{
				x = (randomX + 1) * size;
			}
			if (randomY == 0)
			{
				y = 1;
			}					
			else
			{
				y = (randomY + 1) * size;
			}
			
			if (!liveSquares[randomX, randomY])
			{
				Square(x, y, size);
				
				liveSquares[randomX, randomY] = true;
				squareCounter++;
			}
		}	
    }

    // Update is called once per frame
    void Update()
    {
		
		Background(0);
				
		//Any live cell with fewer than two live neighbors dies as if caused by underpopulation.
		//Any live cell with two or three live neighbors lives on to the next generation.
		//Any live cell with more than three live neighbors dies, as if by overpopulation.
		//Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
		
		int neighbours = 0;
		
		for (int k = numofXSquares; k >= 0; k--)
		{
			for (int l = numofYSquares; l >= 0; l--)
			{
				if (k < numofXSquares)
				{
					if (liveSquares[k + 1, l]) // 1 0
					{
						neighbours = neighbours + 1;
					}
				}
				if (l < numofYSquares)
				{
					if (liveSquares[k, l + 1]) // 0 1
					{
						neighbours = neighbours + 1;
					}
				}
				if (k < numofXSquares && l < numofYSquares)
				if	(liveSquares[k + 1, l + 1]) // 1 1
				{
					neighbours = neighbours + 1;
				}
				if (k > 0)
				{
					if (liveSquares[k - 1, l]) // -1 0
					{
						neighbours = neighbours + 1;
					}
				}
				if ( l > 0)
				{
					if (liveSquares[k ,l - 1]) // 0 -1
					{
						neighbours = neighbours + 1;
					}
				}
				if (k > 0 && l > 0)
				{
					if	(liveSquares[k - 1, l - 1]) // -1 -1
					{
						neighbours = neighbours + 1;
					}
				}
				if (k < numofXSquares && l > 0)
				{
					if (liveSquares[k + 1, l - 1]) // 1 -1
					{
						neighbours = neighbours + 1;
					}
				}
				if (k > 0 && l < numofYSquares)
				{
					if	(liveSquares[k - 1, l + 1]) // -1 1
					{
						neighbours = neighbours + 1;
					}
				}
				if (neighbours == 3 || (neighbours == 2 && liveSquares[k, l]))
				{
					liveSquaresTemp[k, l] = true;
				}
				else
				{
					liveSquaresTemp[k, l] = false;
				}

				neighbours = 0;
			}
		}
	//if (neighbours > 10000)
	//{		
		squareCounter = 0;
		
		float x = 0;
		float y = 0;
		
		for (int g = numofXSquares; g >= 0; g--)
		{
			for (int h = numofYSquares; h >= 0; h--)
			{
				liveSquares[g, h] = liveSquaresTemp[g, h];
				
				if (liveSquares[g, h])
				{
					if (g == 0)
					{
						x = 0;
					}
					else
					{
						x = g * size;
					}
					if (h == 0)
					{
						y = 0;
					}
					else
					{
						y = h * size;
					}
					
					Square(x, y, size);
					
					squareCounter++;
				}
			}
			
		}
    }
	//}
}

//class Checkcellsandupdate : ProcessingLite.GP21
//{
//	public Vector2 position;
//	public bool[,] positionStatus = new bool[Height/cellsize, ;
//	public int size;
//	public int initPopulation;
	
//	public Checkcellsandupdate()
//	{
		
//	}
	
	
	//void Initialize(int size, int initPopulation)
	//{
	//	
	//}
	

//		for (position.x = 0; (int)(position.x / size) <= (int)(Width / size); position.x++)
//		{
//			for (position.y = 0; (int)(position.y / size) <= (int)(Height / size); position.y++)
//			{
//				
//			}
//		}
//	}
//	void CheckAndUpdate()
//	{
//		For (position.x = 0; (int)(position.x / size) <= (int)(Width / size); position.x++)
//		{
//			For (position.y = 0; (int)(position.y / size) <= (int)(Height / size); position.y++)
//			{
				
//			}
//		}
//	}
//	void DrawCell(float x, float y, float size2)
//	{
//		size2 = size2 / 10;
//		Square(x, y, size2);
//	}
//}
