using System;

namespace TonjeConverter
{
	class MainClass
	{
		public static int Main (string[] args)
		{
			try {
				if (args.Length > 0 && args [0] == "offset") {
						Offset offsetFunction = new Offset(args);
						offsetFunction.PerformOffset();

						Console.WriteLine("Success :)");
						return 0;
				}

			} catch(Exception ex){
				Console.WriteLine (ex.Message);
			}
			Console.WriteLine ("Don't know what to do!");


			return 0;
		}
	}
}
