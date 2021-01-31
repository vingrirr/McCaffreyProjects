using System;
namespace LogisticMulti
{
  class LogisticMultiProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin multiclass logistic regression using C# \n");

      Console.WriteLine("Artificial training data with 2-features, 3-classes: ");
      Console.WriteLine("[0.1, 0.2] -> 0");
      Console.WriteLine("[0.4, 0.1] -> 1");
      Console.WriteLine("[0.1, 0.4] -> 2");
      Console.WriteLine(" . . . ");

      Console.WriteLine("\nLoading 27-item training data into memory");
      // dummy data
      double[][] trainX = new double[27][];

      trainX[0] = new double[] { 0.1, 0.1 };  // lower left
      trainX[1] = new double[] { 0.1, 0.2 };
      trainX[2] = new double[] { 0.1, 0.3 };
      trainX[3] = new double[] { 0.2, 0.1 };
      trainX[4] = new double[] { 0.2, 0.2 };
      trainX[5] = new double[] { 0.2, 0.3 };
      trainX[6] = new double[] { 0.3, 0.1 };
      trainX[7] = new double[] { 0.3, 0.2 };
      trainX[8] = new double[] { 0.3, 0.3 };

      trainX[9] = new double[] { 0.4, 0.1 };  // lower right
      trainX[10] = new double[] { 0.4, 0.2 };
      trainX[11] = new double[] { 0.4, 0.3 };
      trainX[12] = new double[] { 0.5, 0.1 };
      trainX[13] = new double[] { 0.5, 0.2 };
      trainX[14] = new double[] { 0.5, 0.3 };
      trainX[15] = new double[] { 0.6, 0.1 };
      trainX[16] = new double[] { 0.6, 0.2 };
      trainX[17] = new double[] { 0.6, 0.3 };

      trainX[18] = new double[] { 0.1, 0.4 };  // upper left
      trainX[19] = new double[] { 0.1, 0.5 };
      trainX[20] = new double[] { 0.1, 0.6 };
      trainX[21] = new double[] { 0.2, 0.4 };
      trainX[22] = new double[] { 0.2, 0.5 };
      trainX[23] = new double[] { 0.2, 0.6 };
      trainX[24] = new double[] { 0.3, 0.4 };
      trainX[25] = new double[] { 0.3, 0.5 };
      trainX[26] = new double[] { 0.3, 0.6 };

      int[][] trainY = new int[27][];
      trainY[0] = new int[] { 1, 0, 0 };  // class 0
      trainY[1] = new int[] { 1, 0, 0 };
      trainY[2] = new int[] { 1, 0, 0 };
      trainY[3] = new int[] { 1, 0, 0 };
      trainY[4] = new int[] { 1, 0, 0 };
      trainY[5] = new int[] { 1, 0, 0 };
      trainY[6] = new int[] { 1, 0, 0 };
      trainY[7] = new int[] { 1, 0, 0 };
      trainY[8] = new int[] { 1, 0, 0 };

      trainY[9] = new int[] { 0, 1, 0 };  // class 1
      trainY[10] = new int[] { 0, 1, 0 };
      trainY[11] = new int[] { 0, 1, 0 };
      trainY[12] = new int[] { 0, 1, 0 };
      trainY[13] = new int[] { 0, 1, 0 };
      trainY[14] = new int[] { 0, 1, 0 };
      trainY[15] = new int[] { 0, 1, 0 };
      trainY[16] = new int[] { 0, 1, 0 };
      trainY[17] = new int[] { 0, 1, 0 };

      trainY[18] = new int[] { 0, 0, 1 };  // class 2
      trainY[19] = new int[] { 0, 0, 1 };
      trainY[20] = new int[] { 0, 0, 1 };
      trainY[21] = new int[] { 0, 0, 1 };
      trainY[22] = new int[] { 0, 0, 1 };
      trainY[23] = new int[] { 0, 0, 1 };
      trainY[24] = new int[] { 0, 0, 1 };
      trainY[25] = new int[] { 0, 0, 1 };
      trainY[26] = new int[] { 0, 0, 1 };

      double lr = 0.01;
      int maxEpoch = 1000;
      Console.WriteLine("\nStart online SGD train lr = " +
        lr.ToString("F3") + " maxEpoch = " + maxEpoch);
      double[][] wts = Train(trainX, trainY, lr, maxEpoch);
      Console.WriteLine("Done");

      Console.WriteLine("\nModel weights and biases:");
      ShowMatrix(wts);

      Console.WriteLine("\nPredicting class for [0.45, 0.25] ");
      double[] x = new double[] { 0.45, 0.25 };
      double[] oupts = ComputeOutput(x, wts, true);  // true: show pre-softmax
      ShowVector(oupts);

      Console.WriteLine("\nEnd demo");
      Console.ReadLine();
    } // Main

    static double[] ComputeOutput(double[] x, double[][] wts, bool verbose = false)
    {
      // wts[feature][class]
      // biases are in last row of wts
      int nc = wts[0].Length;  // number classes
      int nf = x.Length;  // number features
      double[] outputs = new double[nc];
      for (int j = 0; j < nc; ++j)
      {
        for (int i = 0; i < nf; ++i)
          outputs[j] += x[i] * wts[i][j];
        outputs[j] += wts[nf][j];  // add bias 
      }

      if (verbose == true)
        ShowVector(outputs);  // pre-softmax

      return Softmax(outputs);
    }

    static double[] Softmax(double[] vec)
    {
      // naive. consider max trick
      double[] result = new double[vec.Length];
      double sum = 0.0;
      for (int i = 0; i < result.Length; ++i)
      {
        result[i] = Math.Exp(vec[i]);
        sum += result[i];
      }
      for (int i = 0; i < result.Length; ++i)
        result[i] /= sum;
      return result;
    }

    //static double[] Softmax(double[] vec)
    //{
    //  // using max trick
    //  // see https://jamesmccaffrey.wordpress.com/2018/05/18/avoiding-an-exception-when-calculating-softmax/
    //  double m = Max(vec);
    //  double[] vals = new double[vec.Length];
    //  double sum = 0.0;
    //  for (int i = 0; i < vec.Length; ++i)
    //  {
    //    vals[i] = Math.Exp(vec[i] - m);
    //    sum += vals[i];
    //  }
    //  for (int i = 0; i < vec.Length; ++i)
    //    vals[i] /= sum;
    //  return vals;
    //}

    //static double Max(double[] vec)
    //{
    //  double result = vec[0];
    //  for (int i = 0; i < vec.Length; ++i)
    //    if (vec[i] > result) result = vec[i];
    //  return result;
    //}


    static double[][] Train(double[][] trainX, int[][] trainY, double lr, int maxEpoch, int seed = 0)
    {
      int N = trainX.Length;  // number train items
      int nf = trainX[0].Length;  // number predictors/features
      //int nw = nf + 1;  // plus 1 for the bias
      int nc = trainY[0].Length;  // number classes
      Random rnd = new Random(seed);

      double[][] wts = new double[nf + 1][];  // 1 extra row for biases
      for (int i = 0; i < wts.Length; ++i)
        wts[i] = new double[nc];  // wts[i][j] - j is the class, i is the wt (b last cell)

      double lo = -0.01; double hi = 0.01;
      for (int i = 0; i < wts.Length; ++i)
        for (int j = 0; j < wts[0].Length; ++j)
          wts[i][j] = (hi - lo) * rnd.NextDouble() + lo;

      int[] indices = new int[N];  // process in random order
      for (int i = 0; i < N; ++i)
        indices[i] = i;

      int numMessages = 10;
      int intervals = (int)(maxEpoch / numMessages);
      for (int epoch = 0; epoch < maxEpoch; ++epoch)
      {
        Shuffle(indices, rnd);
        foreach (int idx in indices)  // each train item
        {
          double[] oupts = ComputeOutput(trainX[idx], wts);  // computed like (0.20, 0.50, 0.30)
          int[] targets = trainY[idx];                       // targets like  ( 0,     1,    0)
          for (int j = 0; j < nc; ++j) {  // each class
            for (int i = 0; i < nf; ++i) {  // each feature
               wts[i][j] += -1 * lr * trainX[idx][i] * (oupts[j] - targets[j]) * oupts[j] * (1 - oupts[j]);
            }
            wts[nf][j] += -1 * lr * 1 * (oupts[j] - targets[j]) * oupts[j] * (1 - oupts[j]);
          } // j
        } // each train item

        if (epoch % intervals == 0)
        {
          double err = Error(trainX, trainY, wts);
          double acc = Accuracy(trainX, trainY, wts);
          Console.Write("epoch = " + epoch.ToString().PadLeft(5));
          Console.Write("  acc = " + acc.ToString("F4"));
          Console.Write("  err = " + err.ToString("F4"));
          Console.WriteLine("");
        }
      } // epoch

      return wts;
    } // Train

    static void Shuffle(int[] vec, Random rnd)
    {
      int n = vec.Length;
      for (int i = 0; i < n; ++i)
      {
        int ri = rnd.Next(i, n);
        int tmp = vec[ri];
        vec[ri] = vec[i];
        vec[i] = tmp;
      }
    }

    static double Accuracy(double[][] dataX, int[][] dataY, double[][] wts)
    {
      int N = dataX.Length;
      int numCorrect = 0; int numWrong = 0;

      for (int i = 0; i < N; ++i)
      {
        double[] oupts = ComputeOutput(dataX[i], wts);
        int[] targets = dataY[i];

        int mi = ArgMax(oupts);
        if (targets[mi] == 1)
          ++numCorrect;
        else
          ++numWrong;
      }
      return (numCorrect * 1.0) / (numCorrect + numWrong);
    }

    static int ArgMax(double[] vec)
    {
      double maxVal = vec[0];
      int maxIdx = 0;
      for (int i = 0; i < vec.Length; ++i)
      {
        if (vec[i] > maxVal)  {
          maxVal = vec[i]; maxIdx = i;
        }
      }
      return maxIdx;
    }

    static double Error(double[][] dataX, int[][] dataY, double[][] wts)
    {
      // mean squared error
      int N = dataX.Length;
      int nc = dataY[0].Length;  // number classes

      double sumSqErr = 0.0;
      for (int i = 0; i < N; ++i)
      {
        double[] oupts = ComputeOutput(dataX[i], wts);
        int[] targets = dataY[i];

        for (int j = 0; j < nc; ++j)
          sumSqErr += (oupts[j] - targets[j]) * (oupts[j] - targets[j]);
      }
      return sumSqErr / N;
    }

    static void ShowVector(double[] vec)
    {
      for (int i = 0; i < vec.Length; ++i)
        Console.Write(vec[i].ToString("F4") + "  ");
      Console.WriteLine("");
    }

    static void ShowVector(int[] vec)
    {
      for (int i = 0; i < vec.Length; ++i)
        Console.Write(vec[i] + "  ");
      Console.WriteLine("");
    }

    static void ShowMatrix(double[][] wts)
    {
      Console.WriteLine("        Class 0   Class 1   Class 2");
      for (int i = 0; i < wts.Length; ++i)
      {
        if (i == wts.Length - 1)
          Console.Write("b   ");
        else
          Console.Write("w[" + i + "]");
        for (int j = 0; j < wts[0].Length; ++j)
        {
          Console.Write(wts[i][j].ToString("F4").PadLeft(10));
        }
        Console.WriteLine("");
      }
    }

  } // Program class

} // ns