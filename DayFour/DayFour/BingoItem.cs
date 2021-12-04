using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayFour {
	public class BingoItem<T> {

		public T Item { get; set; }
		public bool IsCrossedOut { get; set; }

		public BingoItem(T item) {
			Item = item;
		}
	}
}
