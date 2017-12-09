using System;

namespace Tetrix
{
    public class RowRemovedEventArgs : EventArgs
    {
        public int RowCount { get; private set; }

        public RowRemovedEventArgs(int rowCount)
            => RowCount = rowCount;

        public static RowRemovedEventArgs Create(int rowCount)
            => new RowRemovedEventArgs(rowCount);
    }
}
