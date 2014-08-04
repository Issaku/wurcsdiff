using System;
using System.Collections;

//http://www.shise.net/data/DifferenceCheck.cs
namespace wurcsdif
{
	public class Difference
	{
		public static DiffResult check(Array a, Array b)
		{
			ArrayList add = new ArrayList();
			ArrayList delete = new ArrayList();

			ArrayList list1 = toArray(a.GetEnumerator());
			ArrayList list2 = toArray(b.GetEnumerator());

			for(int i=0; i<list2.Count; i++)
			{
				object target = list2[i];
				if(list1.Contains(target))
				{
					list1.Remove(target);
				}
				else
				{
					add.Add(target);
				}
			}
			delete.AddRange(list1);

			DiffResult result = new DiffResult();
			result.add = add.ToArray();
			result.delete = delete.ToArray();

			return result;
		}

		private static ArrayList toArray(IEnumerator e)
		{
			ArrayList list = new ArrayList();
			while(e.MoveNext())
			{
				list.Add(e.Current);
			}
			return list;
		}
	}

	public class DiffResult
	{
		public object[] add;
		public object[] delete;
	}
}

