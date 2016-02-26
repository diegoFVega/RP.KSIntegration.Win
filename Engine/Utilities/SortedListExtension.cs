using System.Collections.Generic;
using System.Linq;

namespace Engine.Utilities
{
	public static class SortedListExtension
	{
		public static SortedList<TKey, TValue> MergeSortedLists<TKey, TValue>(this SortedList<TKey, TValue> sortedList, SortedList<TKey, TValue> mergedSortedList) => new SortedList<TKey, TValue>(sortedList.Union<KeyValuePair<TKey, TValue>>(mergedSortedList).ToLookup<KeyValuePair<TKey, TValue>, TKey, TValue>(k => k.Key, v => v.Value).ToDictionary<IGrouping<TKey, TValue>, TKey, TValue>(k => k.Key, v => v.First<TValue>()));
	}
}