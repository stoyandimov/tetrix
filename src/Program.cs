using Tetrix;
using Tetrix.UI;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.CursorVisible = false;
Console.CancelKeyPress += (s, e) => ResetConsole();

var inputQueue = new InputQueue();

// Start listening for input on another thread
#pragma warning disable CS4014
Task.Run(() => { while (true) inputQueue.AddInput(Console.ReadKey(true).Key); });
#pragma warning restore CS4014

var settings = new GameSettings(args);
var renderer = new Renderer(settings);
var game = new Game(settings, renderer, inputQueue);
game.Bootstrap(); // blocks
game.Shutdown();
ResetConsole();

static void ResetConsole()
{
	Console.Clear();
	Console.CursorVisible = true;
	Console.ResetColor();
}
