using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_Clomen_Assignment_5 : ProcessingLite.GP21
{

	
	public int noOfEnemyCircles = 10;
	public int n = 0;
	public float collisionController = 0;

	MyCircle mycircle;

	EnemyCircle[] enemycircle;
	
	EndLetters endletters;
	
    // Start is called before the first frame update
	
    void Start()
    {
		
     	// Frame rate settings
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 60;
		
		mycircle = new MyCircle();
		mycircle.diameter = 2;
		mycircle.maxVelocity = 20;
		mycircle.accelerationRatio = 50;
		mycircle.myCirclePosStart.x = Width / 2;
		mycircle.myCirclePosStart.y = Height / 2;
		
		enemycircle = new EnemyCircle[noOfEnemyCircles];
		
		endletters = new EndLetters();
		
		for(int i = 0; i < enemycircle.Length; i++)
		{
			enemycircle[i] = new EnemyCircle();
		}

    }

    // Update is called once per frame
    void Update()
    {
		if (collisionController == 0)
		{
		Background(0);
		
		mycircle.CircleMove();
		
		for(int i = 0; i < Mathf.Round(n / 180); i++)
		{
			enemycircle[i].UpdateEnemycirclePosition();
			enemycircle[i].Draw();
			
			// Collision check, including "ghost" circle drawn when passing horizontal boundaries 
			if (Vector2.Distance(mycircle.myCirclePos, enemycircle[i].enemyCirclePosition) < ((mycircle.diameter + 0.6f) / 2) || Vector2.Distance(new Vector2(mycircle.myCirclePos.x - Width, mycircle.myCirclePos.y), enemycircle[i].enemyCirclePosition) < ((mycircle.diameter + 0.6f) / 2) || Vector2.Distance(new Vector2(mycircle.myCirclePos.x + Width, mycircle.myCirclePos.y), enemycircle[i].enemyCirclePosition) < ((mycircle.diameter + 0.6f) / 2))
			{
				Debug.Log("collision with ball  " + i);
						
				//Set circle velocity to zero, draw END letters, reset enemy circles
				collisionController = 1;

				mycircle.maxVelocity = 0;

				endletters.LetterE();
				endletters.LetterN();
				endletters.LetterD();
			}
		}
		n++;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				collisionController = 0;
				
				n = 0;

				mycircle.maxVelocity = 20;
			}
				
		}
		
    }
}
public class MyCircle : ProcessingLite.GP21
{
	Vector2 relativePosition = Vector2.zero;
	Vector2 velocity = Vector2.zero;

	int toggleGravity = 0;

	public float diameter;
	

	
	//max speed and acceleration ratio
	public float maxVelocity;
	public float accelerationRatio;
	
	// My Circle start position
	public Vector2 myCirclePosStart;
	
	public Vector2 myCirclePos;
	
	public void CircleMove()
	{
		const float gravity = 9.82f; 
		
		// Change velocity and direction based on keys pressed
		if (Input.GetKey(KeyCode.D) && velocity.x <= maxVelocity)
		{
			velocity.x += accelerationRatio * Time.deltaTime;
		}
		
		if (!Input.GetKey(KeyCode.D) && velocity.x > 0)
		{
			velocity.x -= accelerationRatio * Time.deltaTime;
			
			if (velocity.x < 0)
			{
				velocity.x = 0;
			}
		}
		
		if (Input.GetKey(KeyCode.A) && velocity.x >= -maxVelocity)
		{
			velocity.x -= accelerationRatio * Time.deltaTime;
		}
		
		if (!Input.GetKey(KeyCode.A) && velocity.x < 0)
		{
			velocity.x += accelerationRatio * Time.deltaTime;
			
			if (velocity.x > 0)
			{
				velocity.x = 0;
			}
		}
		
		if (Input.GetKey(KeyCode.W) && velocity.y <= maxVelocity)
		{
			velocity.y += accelerationRatio * Time.deltaTime;
		}
		
		if (!Input.GetKey(KeyCode.W) && velocity.y > 0)
		{
			velocity.y -= accelerationRatio * Time.deltaTime;
			
			if (velocity.y < 0)
			{
				velocity.y = 0;
			}
		}
		
		if (Input.GetKey(KeyCode.S) && velocity.y >= -maxVelocity)
		{
			velocity.y -= accelerationRatio * Time.deltaTime;
		}
		
		if (!Input.GetKey(KeyCode.S) && velocity.y < 0)
		{
			velocity.y += accelerationRatio * Time.deltaTime;
			
			if (velocity.y > 0)
			{
				velocity.y = 0;
			}
		}
		
		// Calculate new circle position with normalized speed and draw circle
		relativePosition.x += velocity.x * Time.deltaTime * Mathf.Abs(velocity.normalized.x);
		relativePosition.y += velocity.y * Time.deltaTime * Mathf.Abs(velocity.normalized.y);
		
		myCirclePos = myCirclePosStart + relativePosition;
		
		Stroke(0, 255, 0);
		Circle(myCirclePos.x, myCirclePos.y, diameter);
		
		// Check if part of circle is off screen horisontally and if so, draw new circle on opposite side.
		// Set new circle position on other side once circle has "passed through"
		if (relativePosition.x  >= Width - myCirclePosStart.x - (diameter / 2) - 0.1f && relativePosition.x  <= Width - myCirclePosStart.x + diameter)
		{
			Circle(myCirclePos.x - Width, myCirclePos.y, diameter);
		}
			
		if (myCirclePos.x >= Width + diameter)
		{
			relativePosition.x = relativePosition.x - Width - 0.1f;
		}
		
		if (relativePosition.x <= (diameter / 2) + 0.1f - myCirclePosStart.x && relativePosition.x >= -myCirclePosStart.x - diameter)
		{
			Circle(myCirclePos.x + Width, myCirclePos.y, diameter);
		}
				
		if (myCirclePos.x  <= -diameter)
			{
				relativePosition.x = relativePosition.x + Width + 0.1f;
			}
		
		// Check and limit vertical boundaries
		if (myCirclePos.y >= Height - (diameter / 2) - 0.1f)
		{
			relativePosition.y = Height - myCirclePosStart.y - (diameter / 2) - 0.1f;
			velocity.y = -0.1f;
		}
			
		if (myCirclePos.y <= (diameter / 2) + 0.1f)
		{
			relativePosition.y =  (diameter / 2) - myCirclePosStart.y + 0.1f;
			velocity.y = 0.1f;
		}
	
		// Toggle gravity on/off
		
		if (toggleGravity == 0)
		{
			toggleGravity = 1;
		}
		
		if (Input.GetKeyDown(KeyCode.G))
		{
			toggleGravity = -toggleGravity;
		}
		if (toggleGravity <= -1)
		{
			velocity.y -= Mathf.Pow(gravity, 2) * Time.deltaTime;
		}
	}
}
public class EnemyCircle : ProcessingLite.GP21
{
	public Vector2 enemyCirclePosition;
	Vector2 enemyCircleVelocity;
	
	public EnemyCircle()
	{
		enemyCirclePosition = new Vector2();
		
		// Set random position for new enemy circle
		enemyCirclePosition.x = Random.Range(3, Width - 3);
		enemyCirclePosition.y = Random.Range(1.5f, Height - 1.5f);
		
		enemyCircleVelocity = new Vector2();
		
		// Set random direction/velocity for new enemy circle and make sure that enemy circle starting velocity is not zero (not standing still)
		enemyCircleVelocity.x = Random.Range(0, 6) - 3;
		
		if (enemyCircleVelocity.x == 0)
		{
			enemyCircleVelocity.x = -2;
		}
		
		enemyCircleVelocity.y = Random.Range(0, 6) - 3;
		
			if (enemyCircleVelocity.y == 0)
		{
			enemyCircleVelocity.y = 2;
		}
	}
	public void Draw()
	{
		Stroke(255, 51, 0);
		Circle(enemyCirclePosition.x, enemyCirclePosition.y, 0.6f);
	}
	public void UpdateEnemycirclePosition()
	{
		// Move enemy cirlce
		enemyCirclePosition.x += enemyCircleVelocity.x * Time.deltaTime;
		enemyCirclePosition.y += enemyCircleVelocity.y * Time.deltaTime;
		
		// Make enemy circle bounce on horizontal and vertical boundaries
		if (enemyCirclePosition.y >= Height - (0.6f / 2) - 0.3f || enemyCirclePosition.y <= (0.6f / 2) + 0.3f)
		{
			enemyCircleVelocity.y = -enemyCircleVelocity.y;
		}
					
		if (enemyCirclePosition.x >= Width - (0.6f / 2) - 0.3f || enemyCirclePosition.x <= (0.6f / 2) + 0.3f)
		{
			enemyCircleVelocity.x = -enemyCircleVelocity.x;
		}
	}	
}
public class EndLetters : ProcessingLite.GP21
{
	public float letterPositionX = 10;
	public void LetterE()
	{
		Stroke(0, 255, 0);
		Line(letterPositionX, 7, letterPositionX, 3);
		Line(letterPositionX, 7, letterPositionX + 2, 7);
		Line(letterPositionX, 5, letterPositionX + 2, 5);
		Line(letterPositionX, 3, letterPositionX + 2, 3);
	}
	public void LetterN()
	{
		Line(letterPositionX + 4, 7, letterPositionX + 4, 3);
		Line(letterPositionX + 4, 7, letterPositionX + 6, 3);
		Line(letterPositionX + 6, 7, letterPositionX + 6, 3);
	}
	public void LetterD()
	{
		Line(letterPositionX + 8, 7, letterPositionX + 8, 3);
		Line(letterPositionX + 8, 7, letterPositionX + 10, 5);
		Line(letterPositionX + 8, 3, letterPositionX + 10, 5);
	}
}