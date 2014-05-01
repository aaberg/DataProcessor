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

			List<decimal[]> dataPoints = new List<decimal[]> ();
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

					decimal[] decVals = new decimal[vals.Length];
					for (int idx = 0; idx < vals.Length; idx ++) {
						decVals [idx] = decimal.Parse (vals [idx], formatInfo);
					}


					dataPoints.Add (decVals);

					if (decVals[decVals.Length-1] < offsetVal)
						offsetVal = decVals[decVals.Length-1];
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
				foreach (decimal[] row in dataPoints) {
					for (int idx = 0; idx < row.Length; idx++) {
						decimal decVal = row [idx];
						String tab = "\t";
						if (idx == row.Length - 1) {
							decVal = decVal - offsetVal;
							tab = "";
						}
						String strVal = decVal.ToString ("##0.000");

						writer.Write (strVal);
						writer.Write (tab);
					}
					writer.Write (writer.NewLine);
				}
			}
		}
	}
}