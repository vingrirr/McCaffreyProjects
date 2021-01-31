using System;
namespace LogisticKernel
{
  class LogisticKernelProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin kernel logistic regression using C# demo \n");
      //github comment test
      //double[] v1 = new double[] { .5, .7 };
      //double[] v2 = new double[] { .8, .2 };
      //double k = Kernel(v1, v2, 0.25);
      //Console.WriteLine(k);
      //Console.ReadLine();


      Console.WriteLine("Loading training data into memory");
      Console.WriteLine("training [0] =  (0.2, 0.3) -> 0");
      Console.WriteLine("training [1] =  (0.1, 0.5) -> 0");
      Console.WriteLine(" . . . ");
      Console.WriteLine("training [20] = (0.5, 0.6) -> 1");

      // load training data - not lin. sep. logistic regression gives 12/21 = .57 accuracy
      double[][] trainX = new double[21][];
      trainX[0] = new double[] { 0.2, 0.3 };
      trainX[1] = new double[] { 0.1, 0.5 };
      trainX[2] = new double[] { 0.2, 0.7 };
      trainX[3] = new double[] { 0.3, 0.2 };
      trainX[4] = new double[] { 0.3, 0.8 };
      trainX[5] = new double[] { 0.4, 0.2 };
      trainX[6] = new double[] { 0.4, 0.8 };
      trainX[7] = new double[] { 0.5, 0.2 };
      trainX[8] = new double[] { 0.5, 0.8 };
      trainX[9] = new double[] { 0.6, 0.3 };
      trainX[10] = new double[] { 0.7, 0.5 };
      trainX[11] = new double[] { 0.6, 0.7 };
      trainX[12] = new double[] { 0.3, 0.4 };
      trainX[13] = new double[] { 0.3, 0.5 };
      trainX[14] = new double[] { 0.3, 0.6 };
      trainX[15] = new double[] { 0.4, 0.4 };
      trainX[16] = new double[] { 0.4, 0.5 };
      trainX[17] = new double[] { 0.4, 0.6 };
      trainX[18] = new double[] { 0.5, 0.4 };
      trainX[19] = new double[] { 0.5, 0.5 };
      trainX[20] = new double[] { 0.5, 0.6 };

      int[] trainY = new int[21] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 1, 1, 1, 1, 1, 1, 1, 1 };

      int maxIter = 1000;
      double lr = 0.001;
      double sigma = 0.2;  // small sigma for small values

      Console.WriteLine("\nUsing RBF kernel() with sigma = " + sigma.ToString("F1"));
      Console.WriteLine("Using SGD with lr = " + lr + " and maxIter = " + maxIter);
      Console.WriteLine("Starting training");
      double[] alphas = Train(trainX, trainY, lr, maxIter, sigma);
      Console.WriteLine("Training complete");

      ShowSomeAlphas(alphas, 3, 2);  // first 3, last 2 (last is bias)

      double accTrain = Accuracy(trainX, trainY, trainX, alphas, sigma);
      Console.WriteLine("Model accuracy on train data = " + accTrain.ToString("F4") + "\n");

      Console.WriteLine("Predicting class for (0.15, 0.45)");
      double[] unknown = new double[] { 0.15, 0.45 };
      double p = ComputeOutput(unknown, alphas, sigma, trainX);
      if (p < 0.5)
        Console.WriteLine("Computed p = " + p.ToString("F4") + " Predicted class = 0");
      else
        Console.WriteLine("Computed p = " + p.ToString("F4") + " Predicted class = 1");


      Console.WriteLine("\nEnd kernel logistic regression demo ");
      Console.ReadLine();

    } // Main

    static double[] Train(double[][] trainX, int[] trainY, double lr, int maxIter, double sigma, int seed = 0)
    {
      // Train == compute the alphas
      int n = trainX.Length;  // number train items
      double[] alphas = new double[n + 1];  // 1 per data item. extra cell for bias

      // 1. compute all item-item kernel values
      double[][] kernels = new double[n][]; // item-item similarity
      for (int i = 0; i < n; ++i)
        kernels[i] = new double[n];

      for (int i = 0; i < n; ++i)  // each train item
        for (int j = 0; j < n; ++j)  // other item
          kernels[i][j] = kernels[j][i] = Kernel(trainX[i], trainX[j], sigma);

      //// display kernel matrix
      //Console.WriteLine("\n-------------\n");
      //for (int i = 0; i < n; ++i)  // pre-compute all Kernel
      //{
      //  for (int j = 0; j < n; ++j)
      //  {
      //    Console.Write(kernels[i][j].ToString("F4") + " ");
      //  }
      //  Console.WriteLine("");
      //}
      //Console.WriteLine("\n-------------\n");

      int[] indices = new int[n];  // process data in random order
      for (int i = 0; i < n; ++i)
        indices[i] = i;

      Random rnd = new Random(seed);
            
      for (int iter = 0; iter < maxIter; ++iter)  // main loop
      {
        Shuffle(indices, rnd);
        foreach (int i in indices)  // each train item
        {
          double p = ComputeOutput(trainX[i], alphas, sigma, trainX);  // computed output
          int y = trainY[i];  // target 0 or 1 output
          for (int j = 0; j < alphas.Length - 1; ++j)  // update each alpha
            alphas[j] += lr * (y - p) * kernels[i][j];
            //alphas[j] += lr * (y - p) * p * (1-p) * kernels[i][j];

          alphas[alphas.Length - 1] += lr * (y - p) * 1;  // update bias (dummy input)
        } // each item

        if (iter % (maxIter/5) == 0) {
          double err = Error(trainX, trainY, trainX, alphas, sigma);
          Console.WriteLine(" iter = " + iter.ToString().PadLeft(4) +
            "  error = " + err.ToString("F4"));
        }
      } // main iteration loop

      return alphas;
    } // Train()

    static double Kernel(double[] v1, double[] v2, double sigma)
    {
      // RBF kernel
      double num = 0.0;
      for (int i = 0; i < v1.Length; ++i) 
        num += (v1[i] - v2[i]) * (v1[i] - v2[i]);
      double denom = 2.0 * sigma * sigma;
      double z = num / denom;
      return Math.Exp(-z);
    }

    static double ComputeOutput(double[] x, double[] alphas, double sigma, double[][] trainX)
    {
      // bias is last cell of alphas[]
      int n = trainX.Length;  // == number of training/reference items
      double sum = 0.0;
      for (int i = 0; i < n; ++i)
        sum += (alphas[i] * Kernel(x, trainX[i], sigma));
      sum += alphas[n];  // add the bias
      return LogSig(sum);  // result is [0.0, 1.0]
    }

    static double LogSig(double x)
    {
      if (x < -10.0)
        return 0.0;
      else if (x > 10.0)
        return 1.0;
      else
        return 1.0 / (1.0 + Math.Exp(-x));
    }

    static double Error(double[][] dataX, int[] dataY, double[][] trainX,
      double[] alphas, double sigma)
    {
      int n = dataX.Length;
      double sum = 0.0;  // sum of squarede error
      for (int i = 0; i < n; ++i)
      {
        double p = ComputeOutput(dataX[i], alphas, sigma, trainX);  // [0.0, 1.0]
        int y = dataY[i];  // target 0 or 1
        sum += (p - y) * (p - y);
      }
      return sum / n;
    }

    static double Accuracy(double[][] dataX, int[] dataY, double[][] trainX, 
      double[] alphas, double sigma)
    {
      // data can be train or test
      int numCorrect = 0; int numWrong = 0;
      int n = dataX.Length;
      for (int i = 0; i < n; ++i)
      {
        double p = ComputeOutput(dataX[i], alphas, sigma, trainX);  // [0.0, 1.0]
        int y = dataY[i];  // target 0 or 1

        if (p < 0.5 && y == 0 || p >= 0.5 && y == 1)
          ++numCorrect;
        else
          ++numWrong;
      }

      // Console.WriteLine("nc = " + numCorrect + " nw = " + numWrong);
      return (numCorrect * 1.0) / (numCorrect + numWrong);
    } // Accuracy()

    static void Shuffle(int[] indices, Random rnd)
    {
      for (int i = 0; i < indices.Length; ++i)
      {
        int ri = rnd.Next(i, indices.Length);
        int tmp = indices[i];
        indices[i] = indices[ri];
        indices[ri] = tmp;
      }
    }

    static void ShowSomeAlphas(double[] alphas, int first, int last)
    {
      Console.WriteLine("\nTrained model alpha values: \n");
      for (int i = 0; i < first; ++i)
        Console.WriteLine(" [" + i + "]  " + alphas[i].ToString("F4"));
      Console.WriteLine(" . . .");
      for (int i = alphas.Length - last; i < alphas.Length - 1; ++i)
        Console.WriteLine(" [" + i + "]  " + alphas[i].ToString("F4"));
      Console.WriteLine(" [" + (alphas.Length - 1) + "] (bias) " +
        alphas[alphas.Length - 1].ToString("F4"));
      Console.WriteLine("");
    }

  } // Program class

} // ns
