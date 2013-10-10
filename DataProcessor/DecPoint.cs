using System;

namespace TonjeConverter
{
	public struct DecPoint
	{
		public DecPoint (decimal x, decimal y)
		{
			_x = x;
			_y = y;
		}

		private decimal _x;
		public decimal X {
			get{ return _x;}
		}

		private decimal _y;
		public decimal Y {
			get { return _y;}
		}
	}
}

