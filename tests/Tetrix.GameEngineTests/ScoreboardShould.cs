namespace Tetrix.GameEngineTests;

public class ScoreboardShould
{
	[Fact]
	public void StartWithZeroScore()
	{
		var sut = new Scoreboard();

		Assert.Equal(0, sut.GetScore());
	}

	[Fact]
	public void StartWithScore()
	{
		int score = new Random().Next(1, 100);

		var sut = new Scoreboard(score);

		Assert.Equal(score, sut.GetScore());
	}

	[Fact]
	public void IncrementScore()
	{
		var sut = new Scoreboard(0);

		sut.IncrementScore(1);

		Assert.Equal(1, sut.GetScore());
	}

}