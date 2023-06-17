namespace MakeArrayStrictlyIncreasing
{
	internal class Program
	{
		public class MakeArrayStrictlyIncreasing
		{
			private int BisectRight(int[] arr, int value)
			{
				int left = 0;
				int right = arr.Length;
				while(left < right)
				{
					int middle = (left + right) / 2;
					if (arr[middle] <= value)
					{
						left = middle + 1;
					}
					else
					{
						right = middle;
					}
				}
				return left;
			}
			public int MakeArrayIncreasing(int[] arr1, int[] arr2)
			{
				Array.Sort(arr2);
				Dictionary<int, int> dp = new()
				{
					{ -1, 0 }
				};
				for (int i = 0; i < arr1.Length; ++i)
				{
					Dictionary<int, int> newDp = new();
					foreach(KeyValuePair<int, int> pair in dp)
					{
						if (arr1[i] > pair.Key)
						{
							if (!newDp.ContainsKey(arr1[i]))
							{
								newDp.Add(arr1[i], int.MaxValue);
							}
							newDp[arr1[i]] = Math.Min(newDp[arr1[i]], dp[pair.Key]);
						}
						int rightIdx = BisectRight(arr2, pair.Key);
						if (rightIdx < arr2.Length)
						{
							if (!newDp.ContainsKey(arr2[rightIdx]))
							{
								newDp.Add(arr2[rightIdx], int.MaxValue);
							}
							newDp[arr2[rightIdx]] = Math.Min(newDp[arr2[rightIdx]], 1 + dp[pair.Key]);
						}
					}
					dp = newDp;
				}
				int makeArrayIncreasingSteps = int.MaxValue;
				foreach(KeyValuePair<int, int> pair in dp)
				{
					makeArrayIncreasingSteps = Math.Min(makeArrayIncreasingSteps, pair.Value);
				}
				return makeArrayIncreasingSteps == int.MaxValue ? -1 : makeArrayIncreasingSteps;
			}
		}

		static void Main(string[] args)
		{
			MakeArrayStrictlyIncreasing makeArrayStrictlyIncreasing = new();
            Console.WriteLine(makeArrayStrictlyIncreasing.MakeArrayIncreasing(new int[] { 1, 5, 3, 6, 7 }, new int[] { 1, 3, 2, 4 }));
			Console.WriteLine(makeArrayStrictlyIncreasing.MakeArrayIncreasing(new int[] { 1, 5, 3, 6, 7 }, new int[] { 4, 3, 1 }));
			Console.WriteLine(makeArrayStrictlyIncreasing.MakeArrayIncreasing(new int[] { 1, 5, 3, 6, 7 }, new int[] { 1, 6, 3, 3 }));
		}
	}
}