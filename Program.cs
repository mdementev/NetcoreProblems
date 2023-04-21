using System.Diagnostics;
using System.Text;

using Xunit;

namespace NetcoreProblems
{
    public static class Program
    {
        private static void Main()
        {
            // two sum using dictionary (hash), O(n) time, O(n) space
            Assert.Equal(TwoSumDict(new int[] { 2, 7, 11, 15 }, 9), new int[] { 0, 1 });
            // two sum using left and right pointers (sorted array only), O(n) time, O(n) space
            Assert.Equal(TwoSumPointers(new int[] { 2, 4, 5 }, 6), new int[] { 0, 1 });
            // Palindrome problem (left to right equals right to left),
            Assert.True(IsPalindrome(1321231));
            Assert.False(IsPalindrome(13251231));
            // Is mirrored
            Assert.True(IsMirror("Abc", "cbA"));
            Assert.False(IsMirror("Abc", "CbA"));
            // Is anagram
            Assert.True(IsAnagram("abc123", "321acb"));
            Assert.False(IsAnagram("abc1234", "321acb"));
            Assert.False(IsAnagram("abc123", "321acb4"));
            // Is following sequence
            Assert.False(IsFollowingSequence(new int[] { 2, 2, 2, 5 }));
            Assert.True(IsFollowingSequence(new int[] { 2, 3, 4, 5 }));
            // Roman to integer
            Assert.Equal(1994, RomanToInteger("MCMXCIV"));
            // AddTwoNumbers (not passing, probably need to implement comparison logic)
            // Assert.Equal(expectedNode, AddTwoNumbers(node1, node2));
            // Find symbol in string
            Assert.True(StringContainsSymbol('a', "defa"));
            // Longest Common Prefix
            Assert.Equal("fl", LongestCommonPrefix(new string[3] { "flower", "flow", "flight" }));

            // TestingPerformance();
        }

        public static string LongestCommonPrefix(string[] strs)
        {
            int pointer = 0;
            string result = "";
            while (true)
            {
                try
                {
                    var tempResult = strs[0][pointer];
                    for (var i = 0; i < strs.Length; i++)
                    {
                        if (strs[i][pointer] != tempResult)
                        {
                            return result;
                        }
                    }
                    result += tempResult;
                    pointer++;
                }
                catch
                {
                    break;
                }
            }

            return result;
        }

        public static bool StringContainsSymbol(char a, string b)
        {
            foreach (var item in b)
            {
                if (item == a)
                {
                    return true;
                }
            }

            return false;
        }

        public static ListNode AddTwoNumbers(ListNode a, ListNode b)
        {
            var carry = 0;
            var result = new ListNode();
            ListNode currentNode = null;

            while (a?.val != null || b?.val != null)
            {
                if (currentNode == null)
                {
                    currentNode = result;
                }
                else
                {
                    currentNode.next = new ListNode();
                    currentNode = currentNode.next;
                }

                int val = ((a?.val ?? 0) + (b?.val ?? 0) + carry) % 10;
                carry = ((a?.val ?? 0) + (b?.val ?? 0) + carry) / 10;

                currentNode.val = val;

                if (a != null)
                {
                    a = a.next;
                }

                if (b != null)
                {
                    b = b.next;
                }
            }

            if (carry > 0)
            {
                currentNode.next = new ListNode(carry);
                currentNode = currentNode.next;
            }

            return result;
        }

        public static void TestingPerformance()
        {
            int[] arr = Enumerable.Range(1, 100000000).ToArray();
            Stopwatch watch = Stopwatch.StartNew();

            // new SortedSet<int>(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i]++;
            }

            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Took: " + elapsedMs);
        }

        public static int? RomanToInteger(string s)
        {
            Dictionary<char, int> dict = new() { { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 }, { 'C', 100 }, { 'D', 500 }, { 'M', 1000 } };

            // tricky cases
            s = s.Replace("IV", "IIII")
                .Replace("IX", "VIIII")
                .Replace("XL", "XXXX")
                .Replace("XC", "LXXXX")
                .Replace("CD", "CCCC")
                .Replace("CM", "DCCCC");

            int result = 0;

            foreach (char ch in s)
            {
                result += dict[ch];
            }

            return result;
        }

        public static bool? IsFollowingSequence(int[] sequence)
        {
            SortedSet<int> set = new(sequence);

            return set.Count == sequence.Length && set.Max - set.Min == sequence.Length - 1;
        }

        public static bool? IsAnagram(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            Dictionary<char, int> dict = new();

            for (int i = 0; i < a.Length; i++)
            {
                _ = dict.TryAdd(a[i], 0);
                _ = dict.TryAdd(b[i], 0);

                dict[a[i]]--;
                dict[b[i]]++;
            }

            return dict.Values.All(a => a == 0);
        }

        public static bool? IsMirror(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            int i = 0;
            int j = b.Length - 1;

            for (; j >= 0; j--, i++)
            {
                if (a[j] != b[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool? IsPalindrome(int target)
        {
            string str = target.ToString();
            int a = 0, b = str.Length - 1;
            while (a < b)
            {
                if (str[a] != str[b])
                {
                    return false;
                }

                a++;
                b--;
            }

            return true;
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
            int left = 0;
            int right = nums.Length - 1;
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