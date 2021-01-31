using System;
namespace PredictionMarket
{
  class PredictionMarketProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin prediction market demo \n");
      Console.WriteLine("Goal is to predict winner of Xrays (team [0]) ");
      Console.WriteLine("vs. Yanks (team [1]) using expert opinions ");

      double liq = 100.0; // liquidity
      Console.WriteLine("\nSetting liquidity parameter = " + liq.ToString("F1"));

      int[] outstanding = new int[] { 0, 0 };
      Console.WriteLine("\nInitial number of shares owned of teams [0] and [1] are: ");
      ShowVector(outstanding);

      double[] probs = Probabilities(outstanding, liq);
      Console.WriteLine("Initial inferred probabilities of winning are: ");
      ShowVector(probs, 4, " ");

      Console.WriteLine("=================================");

      double[] costPerShare = CostForOneShare(outstanding, liq);
      Console.WriteLine("Current costs for one share of each team are: ");
      ShowVector(costPerShare, 4, " $");

      Console.WriteLine("Update: expert [01] buys 20 shares of team [0]");
      double costTrans = CostOfTrans(outstanding, 0, 20, liq);
      Console.WriteLine("Cost of transaction to expert was: $" + costTrans.ToString("F2"));

      outstanding = new int[] { 20, 0 };
      Console.WriteLine("\nNew number of shares owned of teams [0] and [1] are: ");
      ShowVector(outstanding);

      probs = Probabilities(outstanding, liq);
      Console.WriteLine("New inferred probabilities of winning are: ");
      ShowVector(probs, 4, " ");

      Console.WriteLine("=================================");

      costPerShare = CostForOneShare(outstanding, liq);
      Console.WriteLine("Current costs for one share of each team are: ");
      ShowVector(costPerShare, 4, " $");

      Console.WriteLine("Update: expert [02] buys 20 shares of team [1]");
      costTrans = CostOfTrans(outstanding, 1, 20, liq);
      Console.WriteLine("Cost of transaction to expert was: $" + costTrans.ToString("F2"));

      outstanding = new int[] { 20, 20 };
      Console.WriteLine("\nNew number of shares owned of teams [0] and [1] are: ");
      ShowVector(outstanding);

      probs = Probabilities(outstanding, liq);
      Console.WriteLine("New inferred probabilities of winning are: ");
      ShowVector(probs, 4, " ");

      Console.WriteLine("=================================");

      costPerShare = CostForOneShare(outstanding, liq);
      Console.WriteLine("Current costs for one share of each team are: ");
      ShowVector(costPerShare, 4, " $");

      Console.WriteLine("Update: expert [03] buys 60 shares of team [0]");
      costTrans = CostOfTrans(outstanding, 0, 60, liq);
      Console.WriteLine("Cost of transaction to expert was: $" + costTrans.ToString("F2"));

      outstanding = new int[] { 80, 20 };
      Console.WriteLine("\nNew number of shares owned of teams [0] and [1] are: ");
      ShowVector(outstanding);

      probs = Probabilities(outstanding, liq);
      Console.WriteLine("New inferred probabilities of winning are: ");
      ShowVector(probs, 4, " ");

      Console.WriteLine("=================================");

      costPerShare = CostForOneShare(outstanding, liq);
      Console.WriteLine("Current costs for one share of each team are: ");
      ShowVector(costPerShare, 4, " $");

      Console.WriteLine("Update: expert [01] sells 10 shares of team [0]");
      costTrans = CostOfTrans(outstanding, 0, -10, liq);
      Console.WriteLine("Cost of transaction to expert was: $" + costTrans.ToString("F2"));

      outstanding = new int[] { 70, 20 };
      Console.WriteLine("\nNew number of shares owned of teams [0] and [1] are: ");
      ShowVector(outstanding);

      probs = Probabilities(outstanding, liq);
      Console.WriteLine("New inferred probabilities of winning are: ");
      ShowVector(probs, 4, " ");

      Console.WriteLine("=================================");

      Console.WriteLine("Update: Market Closed");
      Console.WriteLine("\nEnd prediction market demo \n");
      Console.ReadLine();

    } // Main()

    static double[] Probabilities(int[] outstanding, double liq)
    {
      // aka marginal price
      // return the inferred probabilities of winning
      // from the outstanding shares owned on each team/option
      // b = liquidity. smaller values = faster change
      double[] result = new double[2];
      double denom = 0.0;
      for (int i = 0; i < 2; ++i)
        denom += Math.Exp(outstanding[i] / liq);
      for (int i = 0; i < 2; ++i)
        result[i] = Math.Exp(outstanding[i] / liq) / denom;
      return result;
    } // Probs

    //static int[] OutstandingFromProbs(double[] probs, double liq)
    //{
    //  // return a set of outstanding[] shares
    //  // that correspond to a set of initial probabilities
    //  int[] result = new int[2];
    //  // set outstanding points assoc. with smaller prob to 0
    //  int smallIdx = 0; // take a guess
    //  int largeIdx = 1;
    //  if (probs[1] < probs[0]) // we were wrong
    //  {
    //    smallIdx = 1; largeIdx = 0;
    //  }
    //  result[smallIdx] = 0; // 0 points outstanding
    //  double p = probs[largeIdx];  // the larger prob has outstanding points
    //  result[largeIdx] = (int)Math.Round(liq * Math.Log(p / (1 - p))); // algebra
    //  return result;
    //} // OutstandingFromProbs

    //static int SharesEarned(int[] outstanding, int idx, int shares, double liq)
    //{
    //  // shares earned if option idx wins
    //  // the return includes the points-bet
    //  int s = 0;
    //  double costOfTrans = 0.0;
    //  while (costOfTrans < shares) // subtle
    //  {
    //    costOfTrans = CostOfTrans(outstanding, idx, s, liq);
    //    ++s;
    //  }
    //  return s;
    //} // PointsEarned

    static double Cost(int[] outstanding, double liq)
    {
      // cost asociated with a set of outstanding shares
      double sum = 0.0;
      for (int i = 0; i < 2; ++i)
        sum += Math.Exp(outstanding[i] / liq);
      return liq * Math.Log(sum);
    }

    static double CostOfTrans(int[] outstanding, int idx, int nShares, double liq)
    {
      // cost of a transaction that buys nShares on team idx
      // given current outstanding points
      int[] after = new int[2];
      Array.Copy(outstanding, after, 2);
      after[idx] += nShares;
      return Cost(after, liq) - Cost(outstanding, liq);
    }

    static double[] CostForOneShare(int[] outstanding, double liq)
    {
      double[] result = new double[2];
      result[0] = CostOfTrans(outstanding, 0, 1, liq);
      result[1] = CostOfTrans(outstanding, 1, 1, liq);
      return result;
    }

    static void ShowVector(double[] vector, int dec, string pre)
    {
      for (int i = 0; i < vector.Length; ++i)
        Console.Write(pre + vector[i].ToString("F" + dec) + " ");
      Console.WriteLine("\n");
    } // ShowVector

    static void ShowVector(int[] vector)
    {
      for (int i = 0; i < vector.Length; ++i)
        Console.Write(vector[i] + " ");
      Console.WriteLine("\n");
    } // ShowVector

  } // Program class

} // ns

