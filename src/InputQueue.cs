using System;
using System.Collections.Concurrent;

namespace Tetrix
{
    public class InputQueue
    {
        private readonly BlockingCollection<ConsoleKey> _queue = new BlockingCollection<ConsoleKey>();
		public ConsoleKey GetNextInput()
		{
			System.Console.WriteLine("Get NEXT");
			return _queue.Take();
		}

		public void AddInput(ConsoleKey input)
		{
			System.Console.WriteLine("Add NEXT");
			_queue.Add(input);
		}
	}
}
