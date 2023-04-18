namespace NetcoreProblems
{
    class Program
    {
        static void Main(string[] args)
        {
            // two sum using dictionary (hash)
            Xunit.Assert.Equal(TwoSum(new int[] { 5, 2, 3, 4, 1 }, 7), new int[] { 0, 1 });
        }

        public static int[]? TwoSum(int[] nums, int target)
        {
            var hasmap = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (hasmap.TryGetValue(target - nums[i], out int index))
                {
                    return new int[] { index, i };
                }
                hasmap[nums[i]] = i;
            }

            return null;
        }
    }
}