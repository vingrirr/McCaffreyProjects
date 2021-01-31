using System;
namespace Perceptron
{
  class PerceptronProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin perceptron classification demo \n");
      Console.WriteLine("Goal is to predict authentic (-1) or forgery (+1)");
      Console.WriteLine("from numeric features of an image of a banknote \n");
      Console.WriteLine("Data looks like: ");
      Console.WriteLine(" 3.6216,  8.6661," +
        " -2.8073, -0.44699, -1");
      Console.WriteLine("-2.0631, -1.5147," +
        "  1.219,   0.44524, +1");
      Console.WriteLine(" . . . \n");
      // https://archive.ics.uci.edu/ml/datasets/banknote+authentication
      // features: image variance, skewness, curtosis, entropy
      // 1372 items, 762 authentic (0), 610 fake (1)

      Console.WriteLine("Loading banknote subset training data into memory");
      double[][] xTrain = new double[10][];
      xTrain[0] = new double[] { 3.6216, 8.6661, -2.8073, -0.44699 }; // auth
      xTrain[1] = new double[] { 4.5459, 8.1674, -2.4586, -1.4621 };
      xTrain[2] = new double[] { 3.866, -2.6383, 1.9242, 0.10645 };
      xTrain[3] = new double[] { 2.0922, -6.81, 8.4636, -0.60216 };
      xTrain[4] = new double[] { 4.3684, 9.6718, -3.9606, -3.1625 };

      xTrain[5] = new double[] { -2.0631, -1.5147, 1.219, 0.44524 }; // forgeries
      xTrain[6] = new double[] { -4.4779, 7.3708, -0.31218, -6.7754 };
      xTrain[7] = new double[] { -3.8483, -12.8047, 15.6824, -1.281 };
      xTrain[8] = new double[] { -2.2804, -0.30626, 1.3347, 1.3763 };
      xTrain[9] = new double[] { -1.7582, 2.7397, -2.5323, -2.234 };

      int[] yTrain = new int[] { -1, -1, -1, -1, -1, 1, 1, 1, 1, 1 }; // -1 = auth, 1 = forgery

      int maxIter = 100;
      double lr = 0.01;
      Console.WriteLine("Starting training");
      double[] wts = Train(xTrain, yTrain, lr, maxIter, 0);  // averaged-wts perceptron
      Console.WriteLine("Training complete");

      double acc = Accuracy(xTrain, yTrain, wts);
      Console.WriteLine("\nAccuracy of averaged perceptron model on data = ");
      Console.WriteLine(acc.ToString("F4"));

      Console.WriteLine("\nModel weights and bias: ");
      for (int i = 0; i < wts.Length; ++i)
        Console.Write(wts[i].ToString("F4").PadLeft(8));
      Console.WriteLine("");

      Console.WriteLine("\nPredicting authenticity for note (0.00 2.00 -1.00 1.00)");
      double[] unknown = new double[] { 0.00, 2.00, -1.00, 1.00 };
      double z = ComputeOutput(unknown, wts);
      Console.WriteLine("Computed output = ");
      Console.WriteLine(z);  // -1 or +1

      Console.WriteLine("\nEnd perceptron demo ");
      Console.ReadLine();
    } // Main

    static int ComputeOutput(double[] x, double[] wts)
    {
      // bias is last cell of wts
      double z = 0.0;
      for (int i = 0; i < x.Length; ++i)
        z += x[i] * wts[i];
      z += wts[wts.Length - 1];  // add the bias

      if (z < 0.0) return -1;
      else return +1;
    }

    static double[] Train(double[][] xData, int[] yData, double lr,
      int maxIter, int seed)
    {
      int N = xData.Length;  // number training items
      int n = xData[0].Length;  // number predictors / weights (not including bias)
      double[] wts = new double[n + 1];  // curr wts (1 extra cell for bias)
      double[] accWts = new double[n + 1];  // accumulated weights / avg is result 
      double[] avgWts = new double[n + 1];  // result to return

      int[] indices = new int[N];
      Random rnd = new Random(seed);  // to visit train data random order
      int iter = 0;
      int numAccums = 0;  // number of weight accumulations performed

      while (iter < maxIter)
      {
        Shuffle(indices, rnd);
        foreach (int i in indices)
        {
          int output = ComputeOutput(xData[i], wts);  // -1, or +1
          int target = yData[i];  // -1 or +1

          if (output != target)  // wrong so update wts and bias
          {
            double delta = target - output;  // can only be +2 or -2
            for (int j = 0; j < n; ++j)  // update wts
              wts[j] = wts[j] + (lr * delta * xData[i][j]);
            wts[n] = wts[n] + (lr * delta * 1);  // bias 
          } 

          // averaging gives more emphasis to unchangng wts
          for (int j = 0; j < wts.Length; ++j)
            accWts[j] += wts[j];
          ++numAccums;
        } // for each item
       
        ++iter;
      } // while epoch

      for (int j = 0; j < wts.Length; ++j)
        avgWts[j] = accWts[j] / numAccums;

      return avgWts;
    } // Train

    static void Shuffle(int[] indices, Random rnd)
    {
      int n = indices.Length;
      for (int i = 0; i < n; ++i)
      {
        int ri = rnd.Next(i, n);
        int tmp = indices[ri];
        indices[ri] = indices[i];
        indices[i] = tmp;
      }
    }

    static double Accuracy(double[][] xData, int[] yData,
      double[] wts)
    {
      int numCorrect = 0; int numWrong = 0;
      int N = xData.Length;
      for (int i = 0; i < N; ++i)  // each item
      {
        double[] x = xData[i];
        int target = yData[i];  // actual, -1 or +1
        int computed = ComputeOutput(x, wts);  // -1 or +1
        if (target == 1 && computed == 1 ||
          target == -1 && computed == -1)
        {
          //Console.WriteLine("correct");
          ++numCorrect;
        }
        else
        {
          //Console.WriteLine("wrong");
          ++numWrong;
        }
      }
      return (1.0 * numCorrect) /
        (numCorrect + numWrong);
    }

  } // Program class
} // ns
