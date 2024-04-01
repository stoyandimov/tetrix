using System.Collections.Concurrent;

namespace Tetrix.Cli;

public class InputQueue
{
	private readonly BlockingCollection<ConsoleKey> _queue = [];
	public ConsoleKey GetNextInput() => _queue.Take();
	public void AddInput(ConsoleKey input) => _queue.Add(input);
}
