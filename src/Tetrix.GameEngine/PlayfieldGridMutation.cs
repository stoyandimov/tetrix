namespace Tetrix.GameEngine;

public class PlayfieldGridMutation
{
	public List<Block> SourcePositions { get; private set; } = [];
	public List<Block> TargetPositions { get; private set; } = [];

	public void AddSource(Block b) => SourcePositions.Add(b);
	public void AddSource(int x, int y) => SourcePositions.Add(new (x, y, Tetroes.TetroTypes.I));
	public void AddSources(IEnumerable<Block> blocks) => SourcePositions.AddRange(blocks);

	public void AddTarget(Block b) => TargetPositions.Add(b);
	public void AddTargets(IEnumerable<Block> b) => TargetPositions.AddRange(b);

	public static PlayfieldGridMutation Create(Block source, Block target)
	{
		var m = new PlayfieldGridMutation();
		m.AddSource(source);
		m.AddTarget(target);

		return m;
	}

}
