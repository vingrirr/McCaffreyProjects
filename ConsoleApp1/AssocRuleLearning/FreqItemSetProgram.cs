using System;
using System.Collections.Generic;
// Extracting frequent item-sets for Association Rule Learning.

// Demo of extracting frequent item-sets from a set of transactions.
// Extracting frequent item-sets is useful by itself, but also is
// the first phase of 'assocation rules learning' (not covered in this demo).
// Demo uses a variation of the 'Apriori' algorithm.

namespace FreqItemSets
{
  class FreqItemSetProgram
  {
    static void Main(string[] args)
    {
      try
      {
        Console.WriteLine("\nBegin frequent item-set extraction demo\n");

        string[] rawItems = new string[] { "apples", "bread ", "celery", "donuts", "eggs  ", "flour ",
          "grapes", "honey ", "icing ", "jelly ", "kale  ", "lettuce" };

        int N = rawItems.Length; // total number of items to deal with ( [0..11] )

        string[][] rawTransactions = new string[10][];
        rawTransactions[0] = new string[] { "apples", "bread ", "celery", "flour " };   // 0 1 2 5
        rawTransactions[1] = new string[] { "bread ", "eggs  ", "flour " };             // 1 4 5
        rawTransactions[2] = new string[] { "apples", "bread ", "donuts", "eggs  " };             // 0 1 3 4 
        rawTransactions[3] = new string[] { "celery", "donuts", "flour ", "grapes" };   // 2 3 5 6
        rawTransactions[4] = new string[] { "donuts", "eggs  " };                       // 3 4
        rawTransactions[5] = new string[] { "donuts", "eggs  ", "jelly " };             // 3 4 9
        rawTransactions[6] = new string[] { "apples", "bread ", "donuts", "icing " };   // 0 1 3 8
        rawTransactions[7] = new string[] { "bread ", "grapes", "honey " };                     // 1 6 7
        rawTransactions[8] = new string[] { "apples", "bread ", "celery", "flour ", "kale  " }; // 0 1 2 5 10
        rawTransactions[9] = new string[] { "apples", "bread ", "celery", "flour " };           // 0 1 2 5

        Console.WriteLine("Raw transactions are:");
        Console.WriteLine("-----------------------------------------------");
        for (int i = 0; i < rawTransactions.Length; ++i)
        {
          Console.Write("[" + i + "] : ");
          for (int j = 0; j < rawTransactions[i].Length; ++j)
            Console.Write(rawTransactions[i][j] + "   ");
          Console.WriteLine("");
        }
     
        // could do this programmatically
        List<int[]> transactions = new List<int[]>();
        transactions.Add(new int[] { 0, 1, 2, 5 });
        transactions.Add(new int[] { 1, 4, 5 });
        transactions.Add(new int[] { 0, 1, 3, 4 });
        transactions.Add(new int[] { 2, 3, 5, 6 });
        transactions.Add(new int[] { 3, 4 });
        transactions.Add(new int[] { 3, 4, 9 });
        transactions.Add(new int[] { 0, 1, 3, 8 });
        transactions.Add(new int[] { 1, 6, 7 });
        transactions.Add(new int[] { 0, 1, 2, 5, 10 });
        transactions.Add(new int[] { 0, 1, 2, 5 });
 
        Console.WriteLine("\n\nEncoded transactions are:");
        Console.WriteLine("-----------------------------------------------");
        for (int i = 0; i < transactions.Count; ++i)
        {
          Console.Write("[" + i + "] : ");
          for (int j = 0; j < transactions[i].Length; ++j)
            Console.Write(transactions[i][j].ToString() + " ") ;
          Console.WriteLine("");
        }
        Console.WriteLine("");


        double minSupportPct = 0.30; // minimum pct of transactions for an item-set to be 'frequent'
        int minItemSetLength = 2;
        int maxItemSetLength = 4;
        Console.WriteLine("\nSetting minimum frequent support percent = " +
          minSupportPct.ToString("F2"));
        Console.WriteLine("Setting minimum frequent item-set length = " +
          minItemSetLength);
        Console.WriteLine("Setting maximum frequent item-set length = " +
          maxItemSetLength);
        Console.WriteLine("Using Apriori algorithm to construct frequent item-sets");

        // everything happens here
        List<ItemSet> frequentItemSets =
          GetFrequentItemSets(N, transactions, minSupportPct, minItemSetLength, maxItemSetLength);

        Console.WriteLine("\nFrequent item-sets in numeric form are:");
        Console.WriteLine("");
        for (int i = 0; i < frequentItemSets.Count; ++i)
          Console.WriteLine(frequentItemSets[i].ToString());

        Console.WriteLine("\nFrequent item-sets in string form are:");
        Console.WriteLine("");
        for (int i = 0; i < frequentItemSets.Count; ++i)
        {
          for (int j = 0; j < frequentItemSets[i].data.Length; ++j)
          {
            int v = frequentItemSets[i].data[j];
            Console.Write(rawItems[v] + "   ");
          }
          Console.WriteLine("");
        }

        Console.WriteLine("\nEnd frequent item-set extraction demo\n");
        Console.ReadLine();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Console.ReadLine();
      }
    } // Main

    static List<ItemSet> GetFrequentItemSets(int N, List<int[]> transactions, double minSupportPct,
      int minItemSetLength, int maxItemSetLength)
    {
      // create a List of frequent ItemSet objects that are in transactions
      // frequent means occurs in minSupportPct percent of transactions
      // N is total number of items
      // uses a variation of the Apriori algorithm

      int minSupportCount = (int)(transactions.Count * minSupportPct);

      Dictionary<int, bool> frequentDict = new Dictionary<int, bool>(); // key = int representation of an ItemSet, val = is in List of frequent ItemSet objects
      List<ItemSet> frequentList = new List<ItemSet>(); // item set objects that meet minimum count (in transactions) requirement 
      List<int> validItems = new List<int>(); // inidividual items/values at any given point in time to be used to construct new ItemSet (which may or may not meet threshhold count)

      // get counts of all individual items
      int[] counts = new int[N]; // index is the item/value, cell content is the count
      for (int i = 0; i < transactions.Count; ++i)
      {
        for (int j = 0; j < transactions[i].Length; ++j)
        {
          int v = transactions[i][j];
          ++counts[v];
        }
      }
      // for those items that meet support threshold, add to valid list, frequent list, frequent dict
      for (int i = 0; i < counts.Length; ++i)
      {
        if (counts[i] >= minSupportCount) // frequent item
        {
          validItems.Add(i); // i is the item/value
          int[] d = new int[1]; // the ItemSet ctor wants an array
          d[0] = i;
          ItemSet ci = new ItemSet(N, d, 1); // an ItemSet with size 1, ct 1
          frequentList.Add(ci); // it's frequent
          frequentDict.Add(ci.hashValue, true); // 
        } // else skip this item
      }

      bool done = false; // done if no new frequent item-sets found
      for (int k = 2; k <= maxItemSetLength && done == false; ++k) // construct all size  k = 2, 3, 4, . .  frequent item-sets
      {
        done = true; // assume no new item-sets will be created
        int numFreq = frequentList.Count; // List size modified so store first

        for (int i = 0; i < numFreq; ++i) // use existing frequent item-sets to create new freq item-sets with size+1
        {
          if (frequentList[i].k != k - 1) continue; // only use those ItemSet objects with size 1 less than new ones being created

          for (int j = 0; j < validItems.Count; ++j)
          {
            int[] newData = new int[k]; // data for a new item-set

            for (int p = 0; p < k - 1; ++p)
              newData[p] = frequentList[i].data[p]; // old data in

            if (validItems[j] <= newData[k - 2]) continue; // because item-values are in order we can skip sometimes

            newData[k - 1] = validItems[j]; // new item-value
            ItemSet ci = new ItemSet(N, newData, -1); // ct to be determined

            if (frequentDict.ContainsKey(ci.hashValue) == true) // this new ItemSet has already been added
              continue;
            int ct = CountTimesInTransactions(ci, transactions); // how many times is the new ItemSet in the transactuions?
            if (ct >= minSupportCount) // we have a winner!
            {
              ci.ct = ct; // now we know the ct
              frequentList.Add(ci);
              frequentDict.Add(ci.hashValue, true);
              done = false; // a new item-set was created, so we're not done
            }
          } // j
        } // i

        // update valid items -- quite subtle
        validItems.Clear();
        Dictionary<int, bool> validDict = new Dictionary<int, bool>(); // track new list of valid items
        for (int idx = 0; idx < frequentList.Count; ++idx)
        {
          if (frequentList[idx].k != k) continue; // only looking at the just-created item-sets
          for (int j = 0; j < frequentList[idx].data.Length; ++j)
          {
            int v = frequentList[idx].data[j]; // item
            if (validDict.ContainsKey(v) == false)
            {
              //Console.WriteLine("adding " + v + " to valid items list");
              validItems.Add(v);
              validDict.Add(v, true);
            }
          }
        }
        validItems.Sort(); // keep valid item-values ordered so item-sets will always be ordered
      } // next k

      // transfer to return result, filtering by minItemSetCount
      List<ItemSet> result = new List<ItemSet>();
      for (int i = 0; i < frequentList.Count; ++i)
      {
        if (frequentList[i].k >= minItemSetLength)
          result.Add(new ItemSet(frequentList[i].N, frequentList[i].data, frequentList[i].ct));
      }

      return result;
    }

    static int CountTimesInTransactions(ItemSet itemSet, List<int[]> transactions)
    {
      // number of times itemSet occurs in transactions
      int ct = 0;
      for (int i = 0; i < transactions.Count; ++i)
      {
        if (itemSet.IsSubsetOf(transactions[i]) == true)
          ++ct;
      }
      return ct;
    }
  } // program

  public class ItemSet
  {
    public int N; // data items are [0..N-1]
    public int k; // number of items
    public int[] data; // ex: [0 2 5]
    public int hashValue; // "0 2 5" -> 520 (for hashing)
    public int ct; // num times this occurs in transactions

    public ItemSet(int N, int[] items, int ct)
    {
      this.N = N;
      this.k = items.Length;
      this.data = new int[this.k];
      Array.Copy(items, this.data, items.Length);
      this.hashValue = ComputeHashValue(items);
      this.ct = ct;
    }

    private static int ComputeHashValue(int[] data)
    {
      int value = 0;
      int multiplier = 1;
      for (int i = 0; i < data.Length; ++i) // actually working backward
      {
        value = value + (data[i] * multiplier);
        multiplier = multiplier * 10;
      }
      return value;
    }

    public override string ToString()
    {
      string s = "{ ";
      for (int i = 0; i < data.Length; ++i)
        s += data[i] + " ";
      return s + "}" + "   ct = " + ct; ;
    }

    public bool IsSubsetOf(int[] trans)
    {
      // 'trans' is an ordered transaction like [0 1 4 5 8]
      int foundIdx = -1;
      for (int j = 0; j < this.data.Length; ++j)
      {
        foundIdx = IndexOf(trans, this.data[j], foundIdx + 1);
        if (foundIdx == -1) return false;
      }
      return true;
    }
    private static int IndexOf(int[] array, int item, int startIdx)
    {
      for (int i = startIdx; i < array.Length; ++i)
      {
        if (i > item) return -1; // i is past where the target could possibly be
        if (array[i] == item) return i;
      }
      return -1;
    }


  } // ItemSet

} // ns
