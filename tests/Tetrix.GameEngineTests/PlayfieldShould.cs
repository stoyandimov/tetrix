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
	public void CreateFirstTetro()
	{
		var sut = new Playfield(10, 20, () => 0);
		var initTetro = sut.CurrTetro;

		sut.Progress();

		Assert.Null(initTetro);
		Assert.NotNull(sut.CurrTetro);
	}

	[Fact]
	public void CreateNextTetro()
	{
		var sut = new Playfield(10, 20, () => 0);
		sut.Progress();
		var nextTetro = sut.NextTetro;

		sut.ResetCurrentTetro(sut.GenerateRandomTetro());

		Assert.Same(nextTetro, sut.CurrTetro);
	}

	[Fact]
	public void SetCurrentTetro()
	{
		var sut = new Playfield(10, 20, () => 0);
		var tetro = Tetro.CreateTetro(TetroTypes.I, sut);

		sut.SetCurrTetro(tetro);

		Assert.Same(tetro, sut.CurrTetro);
	}

	[Fact]
	public void FireChangingGridWhenTetroMovesDown()
	{
		var sut = new Playfield(10, 20, () => 0);
		sut.Progress();

		Assert.Raises<Block[]>(h => sut.PlayfieldGridChanging += h, h => sut.PlayfieldGridChanging -= h, sut.MoveDown);
		Assert.Raises<Block[]>(h => sut.PlayfieldGridChanged += h, h => sut.PlayfieldGridChanged -= h, sut.MoveDown);
	}

	[Fact]
	public void FireChangingGridWhenTetroMovesLeft()
	{
		var sut = new Playfield(10, 20, () => 0);
		sut.Progress();

		Assert.Raises<Block[]>(h => sut.PlayfieldGridChanging += h, h => sut.PlayfieldGridChanging -= h, sut.MoveLeft);
		Assert.Raises<Block[]>(h => sut.PlayfieldGridChanged += h, h => sut.PlayfieldGridChanged -= h, sut.MoveLeft);
	}

	[Fact]
	public void FireChangingGridWhenTetroMovesRight()
	{
		var sut = new Playfield(10, 20, () => 0);
		sut.Progress();

		Assert.Raises<Block[]>(h => sut.PlayfieldGridChanging += h, h => sut.PlayfieldGridChanging -= h, sut.MoveRight);
		Assert.Raises<Block[]>(h => sut.PlayfieldGridChanged += h, h => sut.PlayfieldGridChanged -= h, sut.MoveRight);
	}
}