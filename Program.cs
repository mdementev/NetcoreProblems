using Xunit;

namespace NetcoreProblems
{
    internal static class Program
    {
        private static void Main()
        {
            // two sum using dictionary (hash), O(n) time, O(n) space
            Assert.Equal(TwoSumDict(new int[] { 2, 7, 11, 15 }, 9), new int[] { 0, 1 });
            // two sum using left and right pointers (sorted array only)
            Assert.Equal(TwoSumPointers(new int[] { 2, 4, 5 }, 6), new int[] { 0, 1 });
        }

        public static int[]? TwoSumDict(int[] nums, int target)
        {
            Dictionary<int, int> hasmap = new();
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

        public static int[]? TwoSumPointers(int[] nums, int target)
        {
            var left = 0;
            var right = nums.Length - 1;
            while (left < right)
            {
                if (nums[left] + nums[right] == target)
                {
                    return new int[] { left, right };
                }
                else if (nums[left] + nums[right] < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return null;
        }
    }
}