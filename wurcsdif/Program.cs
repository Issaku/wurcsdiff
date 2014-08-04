// Xamarin Studio on MAcOSX 10.9

using System;
using System.IO;
using System.Text ;  // for Encoding
using System.Collections.Generic;
using System.Collections;

namespace wurcsdif
{
	class MainClass
	{
		public static void Main(string[] s)
		{
			StringBuilder sb = new StringBuilder ();

			string file1 = String.Empty;
			string file2 = String.Empty;

			List<string> wurcs1 = new List<string> ();
			List<string> wurcs2 = new List<string> ();

			for (int N = 1; N < Environment.GetCommandLineArgs ().Length; N++) {
				string file = Environment.GetCommandLineArgs () [N];

				if (File.Exists (file)) {
					StreamReader reader = new StreamReader (file, Encoding.Default);
					string A;
					while ((A = reader.ReadLine ()) != null) {
						if (N == 1) {
							wurcs1.Add (A);
							file1 = file;
						} else if (N == 2) {
							wurcs2.Add (A);
							file2 = file;
						}
					}
					reader.Close ();
					//Console.WriteLine (file);
				} 
				else {
					sb.AppendLine ("file not found: " + file);
				}
			}
				
			foreach (var w1 in wurcs1) {
				string[] id1 = w1.Split ('\t');

				foreach (var w2 in wurcs2) {
					string[] id2 = w2.Split ('\t');

					try {
						if (id1 [0] == id2 [0]) {
							if (id1[1] != id2[1] & (id1[1] != "error" && id2[1] != "error")) {
								sb.AppendLine("ID: " + id1[0]);
									
								//char[] ch1 = id1[1].ToCharArray();
								//char[] ch2 = id2[1].ToCharArray();

								string[] ch1 = id1[1].Split(new char[] {'[', ']', '|' }) ;
								string[] ch2 = id2[1].Split(new char[] {'[', ']', '|' }) ;

								DiffResult result = Difference.check(ch1, ch2);

								//sb.AppendLine("#mol");
								foreach(object obj in result.add)
								{
									sb.AppendLine("#mol: " + (string)obj);
								}
								//sb.AppendLine("");
								//sb.AppendLine("#gly");
								foreach(object obj in result.delete)
								{
									sb.AppendLine("#gly: " + (string)obj);
								}
								//sb.AppendLine("");
								Console.WriteLine (id1[0]);
							}
						}
					}
					catch {
					}
				}
			}

			DateTime dt = DateTime.Now;
			string dtString = dt.ToString ("yyyyMMddHHmmss");

			StreamWriter writer = new StreamWriter(dtString + "_dif_" + file1 + "-" + file2,
				false,  // 上書き （ true = 追加 ）
				Encoding.Default) ;

			writer.Write(sb.ToString()) ;
			writer.Close() ;
			Console.WriteLine ("finished");
		}
	}
}
