using Tetrix.GameEngine.Tetroes;

namespace Tetrix.GameEngineTests;

public class PlayfiledShould
{
	[Fact]
	public void Create()
	{
		int h = 10, w = 20;

		var sut = new Playfield(h, w, null);

		Assert.Equal(h, sut.H);
		Assert.Equal(w, sut.W);
	}

	[Fact]
	public void Progress()
	{
		var sut = new Playfield(10, 20, () => 0);

		var result = sut.Progress();

		Assert.True(result);
	}

	[Fact]
	public void CreateFirstTetro()
	{
		var sut = new Playfield(10, 20, () => 0);
		var initTetro = sut.CurrentTetro;

		sut.Progress();

		Assert.Null(initTetro);
		Assert.NotNull(sut.CurrentTetro);
	}

	[Fact]
	public void CreateNextTetro()
	{
		var sut = new Playfield(10, 20, () => 0);
		sut.Progress();
		var nextTetro = sut.NextTetro;

		sut.ResetCurrentTetro(0);

		Assert.Same(nextTetro, sut.CurrentTetro);
	}

	[Fact]
	public void SetCurrentTetro()
	{
		var sut = new Playfield(10, 20, () => 0);
		var tetro = Tetro.CreateTetro(TetroTypes.I, sut);

		sut.SetCurrentTetro(tetro);

		Assert.Same(tetro, sut.CurrentTetro);
	}
}