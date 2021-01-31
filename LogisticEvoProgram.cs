using System;
namespace LogisticEvo
{
  class LogisticEvoProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nEvolutionary optimization for logistic regression \n");
      Console.WriteLine("Banknote authenticity using four predictors");

      // Banknote Authentication subset
      // variance, skewness, kurtosis, entropy
      double[][] trainX = new double[40][];
      trainX[0] = new double[] { 3.6216, 8.6661, -2.8073, -0.44699 };  // auth = 0
      trainX[1] = new double[] { 4.5459, 8.1674, -2.4586, -1.4621 };
      trainX[2] = new double[] { 3.866, -2.6383, 1.9242, 0.10645 };
      trainX[3] = new double[] { 3.4566, 9.5228, -4.0112, -3.5944 };
      trainX[4] = new double[] { 0.32924, -4.4552, 4.5718, -0.9888 };
      trainX[5] = new double[] { 4.3684, 9.6718, -3.9606, -3.1625 };
      trainX[6] = new double[] { 3.5912, 3.0129, 0.72888, 0.56421 };
      trainX[7] = new double[] { 2.0922, -6.81, 8.4636, -0.60216 };
      trainX[8] = new double[] { 3.2032, 5.7588, -0.75345, -0.61251 };
      trainX[9] = new double[] { 1.5356, 9.1772, -2.2718, -0.73535 };
      trainX[10] = new double[] { 1.2247, 8.7779, -2.2135, -0.80647 };
      trainX[11] = new double[] { 3.9899, -2.7066, 2.3946, 0.86291 };
      trainX[12] = new double[] { 1.8993, 7.6625, 0.15394, -3.1108 };
      trainX[13] = new double[] { -1.5768, 10.843, 2.5462, -2.9362 };
      trainX[14] = new double[] { 3.404, 8.7261, -2.9915, -0.57242 };
      trainX[15] = new double[] { 4.6765, -3.3895, 3.4896, 1.4771 };
      trainX[16] = new double[] { 2.6719, 3.0646, 0.37158, 0.58619 };
      trainX[17] = new double[] { 0.80355, 2.8473, 4.3439, 0.6017 };
      trainX[18] = new double[] { 1.4479, -4.8794, 8.3428, -2.1086 };
      trainX[19] = new double[] { 5.2423, 11.0272, -4.353, -4.1013 };

      trainX[20] = new double[] { -1.3971, 3.3191, -1.3927, -1.9948 };  // forgery = 1
      trainX[21] = new double[] { 0.39012, -0.14279, -0.031994, 0.35084 };
      trainX[22] = new double[] { -1.6677, -7.1535, 7.8929, 0.96765 };
      trainX[23] = new double[] { -3.8483, -12.8047, 15.6824, -1.281 };
      trainX[24] = new double[] { -3.5681, -8.213, 10.083, 0.96765 };
      trainX[25] = new double[] { -2.2804, -0.30626, 1.3347, 1.3763 };
      trainX[26] = new double[] { -1.7582, 2.7397, -2.5323, -2.234 };
      trainX[27] = new double[] { -0.89409, 3.1991, -1.8219, -2.9452 };
      trainX[28] = new double[] { 0.3434, 0.12415, -0.28733, 0.14654 };
      trainX[29] = new double[] { -0.9854, -6.661, 5.8245, 0.5461 };
      trainX[30] = new double[] { -2.4115, -9.1359, 9.3444, -0.65259 };
      trainX[31] = new double[] { -1.5252, -6.2534, 5.3524, 0.59912 };
      trainX[32] = new double[] { -0.61442, -0.091058, -0.31818, 0.50214 };
      trainX[33] = new double[] { -0.36506, 2.8928, -3.6461, -3.0603 };
      trainX[34] = new double[] { -5.9034, 6.5679, 0.67661, -6.6797 };
      trainX[35] = new double[] { -1.8215, 2.7521, -0.72261, -2.353 };
      trainX[36] = new double[] { -0.77461, -1.8768, 2.4023, 1.1319 };
      trainX[37] = new double[] { -1.8187, -9.0366, 9.0162, -0.12243 };
      trainX[38] = new double[] { -3.5801, -12.9309, 13.1779, -2.5677 };
      trainX[39] = new double[] { -1.8219, -6.8824, 5.4681, 0.057313 };

      int[] trainY = new int[40] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                                 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

      int popSize = 6;
      int maxGen = 8;
      double alpha = 0.40;    // selection pressure (parents)
      double sigma = 0.80;  // selection (child)
      double omega = 0.50;  // selection (new soln)
      double mRate = 0.05;  // mutation rate

      Console.WriteLine("\nSetting pop size = " + popSize);
      Console.WriteLine("Setting max gen = " + maxGen);
      double[] wts = Train(trainX, trainY, popSize, maxGen, alpha, sigma, omega, mRate, seed: 3);

      Console.WriteLine("\nBest weights and bias found:");
      ShowVector(wts);

      Console.WriteLine("\nEnd demo ");
      Console.ReadLine();
    } // Main

    static double[] Train(double[][] trainX, int[] trainY, int popSize, int maxGen,
      double alpha, double sigma, double omega, double mRate, int seed)
    {
      Random rnd = new Random(seed);

      // create a population of possible solutions
      int nf = trainX[0].Length;  // num features (4 for Banknote)
      int nw = nf + 1;  // num wts = add 1 for the bias
      double[][] population = MakePopulation(popSize, nw, rnd);

      // compute error for each possible solution
      double[] errors = new double[popSize];
      for (int i = 0; i < popSize; ++i)
        errors[i] = Error(trainX, trainY, population[i]);

      // goal: find idx of best solution
      int bestSoln = 0;
      double bestErr = double.MaxValue;

      Console.WriteLine("\n==================");
      for (int gen = 0; gen < maxGen; ++gen)  // each generation
      {
        int[] parents = SelectTwo(errors, alpha, rnd);  // pick indices of two "good" solutions
        
        double[] child = MakeChild(population, parents, rnd);   // use good solutions to create a child solution
        Mutate(child, mRate, rnd);  // mutate child
        int bad = SelectBad(errors, sigma, rnd);   // pick index of a "bad" solution
        population[bad] = child;   // replace bad solution with the new child
        errors[bad] = Error(trainX, trainY, child);  // compute child error

        double[] rndSoln = MakeSolution(nw, rnd);  // inject a new random solution
        bad = SelectBad(errors, omega, rnd);   // pick index of a "bad" solution
        population[bad] = rndSoln;   // replace bad solution with the new soln
        errors[bad] = Error(trainX, trainY, rndSoln);  // compute the error

        // update best solution in population
        bestSoln = BestSolution(errors);
        bestErr = errors[bestSoln];
        double acc = Accuracy(trainX, trainY, population[bestSoln]);
        
        Console.WriteLine("generation = " + gen);
        Console.WriteLine("Best solution = [" + bestSoln + "]");
        Console.WriteLine("Best error = " + bestErr.ToString("F4"));
        Console.WriteLine("Accuracy = " + acc.ToString("F4"));
        Console.WriteLine("==================");
      } // generation

      return population[bestSoln];
    } // Train

    static double[] MakeSolution(int nw, Random rnd)
    {
      double lo = -1.0; double hi = 1.0;
      double[] soln = new double[nw];
      for (int i = 0; i < nw; ++i)
        soln[i] = (hi - lo) * rnd.NextDouble() + lo;
      return soln;
    }

    static double[][] MakePopulation(int popSize, int nw, Random rnd)
    {
      double[][] pop = new double[popSize][];
      for (int i = 0; i < popSize; ++i)
        pop[i] = MakeSolution(nw, rnd);
      return pop;
    }

    static double[] MakeChild(double[][] pop, int[] parents, Random rnd)
    {
      int nw = pop[0].Length;  // num wts including bias
      int idx = rnd.Next(1, nw);  // crossover (0 permits no change)
      double[] child = new double[nw];
      for (int j = 0; j < idx; ++j)
        child[j] = pop[parents[0]][j];  // left part of parent 1
      for (int j = idx; j < nw; ++j)
        child[j] = pop[parents[1]][j];  // right part of parent 2
      return child;
    }

    static void Mutate(double[] child, double mRate, Random rnd)
    {
      double lo = -1.0; double hi = 1.0;
      for (int i = 0; i < child.Length; ++i)
      {
        double p = rnd.NextDouble();
        if (p < mRate)  // rarely
          child[i] = (hi - lo) * rnd.NextDouble() + lo;
      }
      return;
    }

    static int BestSolution(double[] errors)
    {
      // find current best solution
      int bestSoln = 0;  // index of best wts
      double bestErr = errors[0];
      for (int i = 0; i < errors.Length; ++i)
      {
        if (errors[i] < bestErr)  {
          bestSoln = i; bestErr = errors[i];
        }
      }
      return bestSoln;
    }

    static int SelectGood(double[] errors, double pct, Random rnd)
    {
      // pick best one from a random pct-percent of population
      int popSize = errors.Length;
      int numItems = (int)(popSize * pct);
      int[] allIndices = new int[popSize];
      for (int i = 0; i < popSize; ++i)
        allIndices[i] = i;
      Shuffle(allIndices, rnd);

      int bestIdx = allIndices[0];
      double bestErr = errors[allIndices[0]];
      for (int i = 0; i < numItems; ++i)
      {
        int idx = allIndices[i];
        if (errors[idx] < bestErr) {
          bestIdx = idx;  bestErr = errors[idx];
        }
      }
      return bestIdx;
    }

    static int[] SelectTwo(double[] errors, double pct, Random rnd)
    {
      // two different good ones (highly unlikely but possibly the same)
      int[] result = new int[2];
      int ct = 0;
      result[0] = SelectGood(errors, pct, rnd);
      result[1] = SelectGood(errors, pct, rnd);
      while (result[1] == result[0] && ct < 100) {
        result[1] = SelectGood(errors, pct, rnd);  // try again
        ++ct;
      }
      return result;
    }

    static int SelectBad(double[] errors, double pct, Random rnd)
    {
      // pick worst one from a random pct-percent of population
      int popSize = errors.Length;
      int numItems = (int)(popSize * pct);
      int[] allIndices = new int[popSize];
      for (int i = 0; i < popSize; ++i)
        allIndices[i] = i;
      Shuffle(allIndices, rnd);

      int worstIdx = allIndices[0];
      double largestErr = errors[allIndices[0]];
      for (int i = 0; i < numItems; ++i)
      {
        int idx = allIndices[i];
        if (errors[idx] > largestErr)
        {
          worstIdx = idx; largestErr = errors[idx];
        }
      }
      return worstIdx;
    }

    static void Shuffle(int[] vec, Random rnd)
    {
      int n = vec.Length;
      for (int i = 0; i < n; ++i) {
        int ri = rnd.Next(i, n);
        int tmp = vec[ri];
        vec[ri] = vec[i];
        vec[i] = tmp;
      }
    }

    static double ComputeOutput(double[] x, double[] wts)
    {
      // bias is last cell of w
      double z = 0.0;
      for (int i = 0; i < x.Length; ++i)
        z += x[i] * wts[i];
      z += wts[wts.Length - 1];
      return LogSig(z);
    }

    static double LogSig(double x)
    {
      if (x < -20.0)
        return 0.0;
      else if (x > 20.0)
        return 1.0;
      else
        return 1.0 / (1.0 + Math.Exp(-x));
    }

    static double Accuracy(double[][] dataX, int[] dataY, double[] wts)
    {
      int numCorrect = 0; int numWrong = 0;
      int N = dataX.Length;
      for (int i = 0; i < N; ++i)
      {
        double[] x = dataX[i];
        int y = dataY[i];  // actual, 0 or 1
        double p = ComputeOutput(x, wts);
        if (y == 0 && p < 0.5 || y == 1 && p >= 0.5)
          ++numCorrect;
        else
          ++numWrong;
      }
      return (1.0 * numCorrect) / (numCorrect + numWrong);
    }

    static double Error(double[][] dataX, int[] dataY, double[] wts)
    {
      double sum = 0.0;
      int N = dataX.Length;
      for (int i = 0; i < N; ++i)
      {
        double[] x = dataX[i];
        int y = dataY[i];  // target, 0 or 1
        double p = ComputeOutput(x, wts);
        sum += (p - y) * (p - y); // E = (o-t)^2 form
      }
      return sum / N; ;
    }

    static void ShowVector(double[] v)
    {
      for (int i = 0; i < v.Length; ++i)
        Console.Write(v[i].ToString("F4") + " ");
      Console.WriteLine("");
    }

  } // Program

} // ns
