using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CleanCode
{
	public class Board
	{
		private readonly Cell[,] cells = new Cell[8,8];

		public Board(TextReader inp)
		{
			for (int y = 0; y < 8; y++)
			{
				string line = inp.ReadLine();
				if (line == null) throw new Exception("incorrect input");
				for (int x = 0; x < 8; x++)
				{
					char figureSign = line[x];
				    PieceColor color = Char.IsUpper(figureSign) ? PieceColor.White : PieceColor.Black;
					Set(new Location(x, y), new Cell(Piece.FromChar(figureSign), color));
				}
			}
		}

		public IEnumerable<Location> GetPieces(PieceColor color)
		{
			return Location.AllBoard().Where(loc => Get(loc).Piece != null && Get(loc).Color == color);
		}

		public Cell Get(Location location)
		{
			return !location.InBoard ? Cell.Empty : cells[location.X, location.Y];
		}

		public void Set(Location location, Cell cell)
		{
			cells[location.X, location.Y] = cell;
		}

		public override string ToString()
		{
			var b = new StringBuilder();
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
					b.Append(Get(new Location(x, y)));
				b.AppendLine();
			}
			return b.ToString();
		}

		public Move PerformMove(Location from, Location to)
		{
			Cell old = Get(to);
			Set(to, Get(from));
			Set(from, Cell.Empty);
			return new Move(this, from, to, old);
		}
	}
}