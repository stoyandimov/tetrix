using System;

namespace Tetrix.GameEngine;

public class RowRemovedEventArgs : EventArgs
{
	public int RowCount { get; private set; }

	public RowRemovedEventArgs(int rowCount) => RowCount = rowCount;

	public static RowRemovedEventArgs Create(int rowCount)=> new (rowCount);
}
