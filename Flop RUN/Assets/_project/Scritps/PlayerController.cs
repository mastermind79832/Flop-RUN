using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    public float VelocityY;

    public bool IsControlStart;
    // Update is called once per frame

    public float Score;
    public TextMeshProUGUI ScoreText;

    public void GameStart()
    {
		rb.linearVelocity = Vector2.up * 30 + Vector2.right * 30;
        rb.simulated = true;
		StartCoroutine(WaitForStarting());
	}

	private IEnumerator WaitForStarting()
	{
		yield return new WaitForSeconds(1);
        StartControl();
	}

	public void StartControl()
    {
        IsControlStart = true;
        Score = 0;
	}

    void Update()
    {
        if (!GameManager.instance.IsGameStart || !IsControlStart)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            rb.gravityScale = 8;
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            rb.gravityScale = 1;

        }

        VelocityY = Mathf.Min(rb.linearVelocityY, VelocityY);

        if (rb.linearVelocityX <= 0.8f || transform.position.y < -40)
        {
            rb.simulated = false;
			GameManager.instance.GameOver(Score);

            IsControlStart=false;
        }

        Score += Time.deltaTime;
        ScoreText.text = Score.ToString();
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
        OceanBlock block = collision.gameObject.GetComponentInParent<OceanBlock>();

		if (block)
        {

            Debug.Log("hit");
			
			Debug.Log(rb.linearVelocity);
			VelocityY = -VelocityY;

            float value = 0;
			switch (block.ObstacleType)
            {
                case ObstacleTypeEnum.Ocean:
					VelocityY += 2;
					value -= 1;
					break;
                case ObstacleTypeEnum.Dolphin:
                    VelocityY += 7;
					value += 20 * rb.gravityScale;
                    break;
                case ObstacleTypeEnum.Jellyfish:
                    VelocityY -= 0.2f;
					value -= 0.2f;
                    break;
                case ObstacleTypeEnum.Shark:
                   VelocityY += 5;
					value += 15 * rb.gravityScale;
					break;
                case ObstacleTypeEnum.Whale:
                    VelocityY += 10;
                    value += 10 * rb.gravityScale;
                    break;
                case ObstacleTypeEnum.Turtle:
                    VelocityY += 7 * rb.gravityScale;
                    break;
                case ObstacleTypeEnum.Boat:
                   VelocityY -= 3;
                    break;
                case ObstacleTypeEnum.Mine:
                    VelocityY = 10;
                    value = 200 * rb.gravityScale;
                    break;
                default:
                    break;
            }

			VelocityY = Mathf.Clamp(VelocityY, 0, 15);
            rb.linearVelocity += Vector2.up * VelocityY + Vector2.right * value;
            rb.linearVelocityY = VelocityY;
			VelocityY = 0;
			rb.gravityScale = 1;
			collision.gameObject.SetActive(false);
        }
	}
}
