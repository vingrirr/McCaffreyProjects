using System;
namespace MultiBandit
{
  class MultiBanditProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin multi-armed bandit problem demo \n");

      Console.WriteLine("Creating 3 Gaussian machines");
      Console.WriteLine("Machine 0 mean =  0.0, sd = 1.0");
      Console.WriteLine("Machine 1 mean = -0.5, sd = 2.0");
      Console.WriteLine("Machine 2 mean =  0.1, sd = 0.5");
      Console.WriteLine("\nBest machine is [2] with mean pay = 0.1");

      int nMachines = 3;
      Machine[] machines = new Machine[nMachines];
      machines[0] = new Machine(0.0, 1.0, 0);
      machines[1] = new Machine(-0.5, 2.0, 1);
      machines[2] = new Machine(0.1, 0.5, 2);

      int nPulls = 20;
      double pctExplore = 0.40;
      Console.WriteLine("Setting nPulls = " + nPulls);

      Console.WriteLine("\nUsing explore-exploit with pctExplore = " + pctExplore.ToString("F2"));
      double avgPay = ExploreExploit(machines, pctExplore, nPulls);
      double totPay = avgPay * nPulls;
      Console.WriteLine("\nAverage pay per pull = " + avgPay.ToString("F2"));
      Console.WriteLine("Total payout         = " + totPay.ToString("F2"));

      double avgBase = machines[2].mean;
      double totBase = avgBase * nPulls;
      Console.WriteLine("\nBaseline average pay = " + avgBase.ToString("F2"));
      Console.WriteLine("Total baseline pay   = " + totBase.ToString("F2"));

      double regret = totBase - totPay;
      Console.WriteLine("\nTotal regret = " + regret.ToString("F2"));

      Console.WriteLine("\nEnd bandit demo \n");
      Console.ReadLine();

    } // Main

    static double ExploreExploit(Machine[] machines, double pctExplore, int nPulls)
    {
      // use basic explore-exploit algorithm
      // return the average pay per pull
      int nMachines = machines.Length;
      Random r = new Random(2); // which machine (2 gives representative demo)

      double[] explorePays = new double[nMachines]; // curr total payout (init 0.0 all)
      double totPay = 0.0; 

      int nExplore = (int)(nPulls * pctExplore); // n pulls to explore
      int nExploit = nPulls - nExplore; // n pulls to exploit

      Console.WriteLine("\nStart explore phase");
      for (int pull = 0; pull < nExplore; ++pull) // explore phase
      {
        int m = r.Next(0, nMachines); // pick a machine at random
        double pay = machines[m].Pay(); // play
        Console.Write("[" + pull.ToString().PadLeft(3) + "]  ");
        Console.WriteLine("selected machine " + m + ". pay = " +
          pay.ToString("F2").PadLeft(6));
        explorePays[m] += pay; // update
        totPay += pay;
      } // explore

      int bestMach = BestIdx(explorePays); // which machine paid most
      Console.WriteLine("\nBest machine found during explore = " + bestMach);

      Console.WriteLine("\nStart exploit phase");
      for (int pull = 0; pull < nExploit; ++pull) // exploit phase
      {
        double pay = machines[bestMach].Pay(); // play same machine always
        Console.Write("[" + pull.ToString().PadLeft(3) + "]  ");
        Console.WriteLine("pay = " + pay.ToString("F2").PadLeft(6));
        totPay += pay; // accumulate
      } // exploit

      return totPay / nPulls; // avg payout per pull

    } // ExploreExploit



    static int BestIdx(double[] pays)
    {
      // index of array with largest value
      int result = 0;
      double maxVal = pays[0];
      for (int i = 0; i < pays.Length; ++i)
      {
        if (pays[i] > maxVal)
        {
          result = i;
          maxVal = pays[i];
        }
      }
      return result;
    }

    //static void Display(int[] array)
    //{
    //  for (int i = 0; i < array.Length; ++i)
    //    Console.Write(array[i] + " ");
    //  Console.WriteLine("");
    //}

    //static void Display(double[] array)
    //{
    //  for (int i = 0; i < array.Length; ++i)
    //    Console.Write(array[i].ToString("F4") + " ");
    //  Console.WriteLine("");
    //}
  } // Program class 

  public class Machine
  {
    public double mean; // avg payout per pull
    public double sd; // variability about the mean
    private Gaussian g; // payout generator

    public Machine(double mean, double sd, int seed)
    {
      this.mean = mean;
      this.sd = sd;
      this.g = new Gaussian(mean, sd, seed);
    }

    public double Pay()
    {
      return this.g.Next();
    }

    // -----

    private class Gaussian
    {
      private Random r;
      private double mean;
      private double sd;

      public Gaussian(double mean, double sd, int seed)
      {
        this.r = new Random(seed);
        this.mean = mean;
        this.sd = sd;
      }

      public double Next()
      {
        double u1 = r.NextDouble();
        double u2 = r.NextDouble();
        double left = Math.Cos(2.0 * Math.PI * u1);
        double right = Math.Sqrt(-2.0 * Math.Log(u2));
        double z = left * right;
        return this.mean + (z * this.sd);
      }
    }

    // -----

  } // Machine

} // ns
