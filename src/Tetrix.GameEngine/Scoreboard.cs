namespace Tetrix.GameEngine;

public class Scoreboard(int score = 0)
{
	private int _score = score;

	public int GetScore() => _score;

	public void IncrementScore(int count) => _score += count;

}
