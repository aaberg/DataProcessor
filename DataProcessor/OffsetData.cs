using System;
using System.Windows.Forms;

namespace TonjeConverter
{
	public class OffsetData
	{
		public OffsetData (String[] args)
		{
			for (int i = 1; i < args.Length; i++) {
				String[] arg = args [i].Split (new char[]{'='});
				if (arg.Length != 2) {
					throw new Exception ("Wrong argument to Offset method");
				}

				if (arg [0] == "i") {
					this.InputFileName = arg [1];
				} else if (arg [0] == "o") {
					this.OutputFileName = arg [1];
				}
			}

			if (InputFileName == null) {
				OpenFileDialog dialog = new OpenFileDialog ();
				dialog.Title = "Input file name";
				dialog.Multiselect = false;
				if (dialog.ShowDialog () == DialogResult.OK) {
					this.InputFileName = dialog.FileName;
				} else {
					throw new Exception ("Missing input file argument");
				}
			}

			if (OutputFileName == null) {
				FileDialog outputFileDialog = new SaveFileDialog ();
				outputFileDialog.Title = "Output file name";
				if (outputFileDialog.ShowDialog () == DialogResult.OK) {
					this.OutputFileName = outputFileDialog.FileName;
				} else {
					throw new Exception ("Missing output file argument");
				}
			}
				
		}

		public String InputFileName {
			get;
			set;
		}

		public String OutputFileName {
			get;
			set;
		}
	}
}

