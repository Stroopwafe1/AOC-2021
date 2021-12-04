using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DayFour {
	public class BingoCard {

		public int Sum { get; set; }
		public bool IsBingo { get; set; }

		public BingoItem<int>[,] Array { get; private set; } = new BingoItem<int>[5, 5];

		public BingoCard() { }

		public BingoCard(BingoItem<int>[,] array) {
			Array = array;
			for (int y = 0; y < 5; y++) {
				for (int x = 0; x < 5; x++) {
					var item = array[y, x];
					Sum += item.Item;
				}
			}
		}

		public void InsertItem(int x, int y, BingoItem<int> item) {
			Array[y, x] = item;
			Sum += item.Item;
		}

		public bool Crossout(int num) {
			// Loop through 2d array
			for (int y = 0; y < 5; y++) {
				for (int x = 0; x < 5; x++) {
					var item = Array[y, x];
					if (item.IsCrossedOut)
						continue;
					if (item.Item == num) {
						item.IsCrossedOut = true;
						Sum -= num;
					}
				}
			}
			bool bingo = CheckBingo();
			if (!IsBingo)
				IsBingo = bingo;
			return bingo;
		}

		public bool CheckBingo() {
			for (int y = 0; y < 5; y++) {
				if (Array[y, 0].IsCrossedOut && Array[y, 1].IsCrossedOut && Array[y, 2].IsCrossedOut && Array[y, 3].IsCrossedOut && Array[y, 4].IsCrossedOut)
					return true;
			}
			for (int x = 0; x < 5; x++) {
				if (Array[0, x].IsCrossedOut && Array[1, x].IsCrossedOut && Array[2, x].IsCrossedOut && Array[3, x].IsCrossedOut && Array[4, x].IsCrossedOut)
					return true;
			}
			return false;
		}

		public void Reset() {
			IsBingo = false;
			Sum = 0;
			for (int y = 0; y < 5; y++) {
				for (int x = 0; x < 5; x++) {
					var item = Array[y, x];
					item.IsCrossedOut = false;
					Sum += item.Item;
				}
			}
		}
	}
}
