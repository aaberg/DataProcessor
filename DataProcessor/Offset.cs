using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace TonjeConverter
{
	public class Offset
	{
		public Offset (String[] args)
		{
			this.Data = new OffsetData (args);

		}

		public OffsetData Data {
			get;
			private set;
		}

		public void PerformOffset() {
			Console.WriteLine (String.Format ("Processing data from {0}", this.Data.InputFileName));
			Console.WriteLine (String.Format ("Writing to file {0}", this.Data.OutputFileName));

			List<DecPoint> dataPoints = new List<DecPoint> ();
			decimal offsetVal = 0;

			// read file
			using (StreamReader reader = new StreamReader (this.Data.InputFileName)) {
				string line;

				while ((line = reader.ReadLine()) != null) {
					if (line.Contains ("Inf"))
						continue;

					if (string.IsNullOrEmpty (line.Trim ())) {
						continue;
					}

					String[] vals = line.Split ();
					NumberFormatInfo formatInfo = CultureInfo.GetCultureInfo ("NB-no").NumberFormat;
					decimal valx = decimal.Parse (vals [0], formatInfo);
					decimal valy = decimal.Parse (vals [1], formatInfo);

					dataPoints.Add (new DecPoint (valx, valy));

					if (valy < offsetVal)
						offsetVal = valy;
				}
			}

			// Check if file already exists. Prompt for overwrite.
//			if (File.Exists (this.Data.OutputFileName)) {
//				do {
//					Console.Write ("Output file exist. Overwrite? (Y/N): ");
//					StringBuilder inputStr = new StringBuilder();
//					int inpKey;
//					while ((inpKey = Console.Read()) != 10){
//						inputStr.Append((char)inpKey);
//					}
//
//					if (inputStr.ToString().Equals ("n", StringComparison.InvariantCultureIgnoreCase)) {
//						Console.WriteLine ("Aborting...");
//						return;
//					} else if (inputStr.ToString().Equals ("y", StringComparison.InvariantCultureIgnoreCase)) {
//						break;
//					}
//				} while(true);
//			}

			// Write new file
			using (StreamWriter writer = new StreamWriter(this.Data.OutputFileName, false, Encoding.UTF8)) {
				foreach (DecPoint point in dataPoints) {
					String val1 = point.X.ToString ("##0.000");
					String val2 = (point.Y - offsetVal).ToString ("##0.000");
					writer.Write (val1);
					for (int i = 0; i < (8 - val1.Length); i++) {
						writer.Write (" ");
					}
					writer.Write (val2);
					writer.Write (writer.NewLine);
				}
			}
		}
	}
}