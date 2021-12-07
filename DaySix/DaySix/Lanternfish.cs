namespace DaySix {
	public class Lanternfish {
		public int Spawntime { get; set; }

		public Lanternfish(int _spawntime) {
			Spawntime = _spawntime;
		}

		public bool NextDay() {
			Spawntime--;
			if (Spawntime == -1) {
				Spawntime = 6;
				return true;
			}

			return false;
		}
	}
}