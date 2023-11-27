using System;

// "Roach Infestation Optimization"
// 2008 IEEE Swarm Intelligence Symposium
// Timothy C. Havens, Christopher J. Spain, Nathan G. Salmon, James M. Keller
// 
// http://www.ece.mtu.edu/~thavens/papers/SIS_2008_Havens.pdf

namespace RoachOptimization
{
  class RoachOptimizationProgram
  {
    static void Main(string[] args)
    {
      Console.WriteLine("\nBegin roach infestation optimization demo\n");

      //int dim = 5;
      //int numRoaches = 20;
      //int tMax = 5000;
      //int rndSeed = 2;

      int dim = 8;
      int numRoaches = 20;
      int tMax = 10000;
      int rndSeed = 6;

      Console.WriteLine("Goal is to minimize Rastrigin's " +
        "function in " + dim + " dimensions");
      Console.WriteLine("Problem has known min value = 0.0 " +
        "at (0, 0, .. 0) \n");
      Console.WriteLine("Setting number of roaches  = " +
        numRoaches);
      Console.WriteLine("Setting maximum iterations = " +
        tMax);
      Console.WriteLine("Setting random seed        = " +
        rndSeed);

      Console.WriteLine("\nStarting roach optimization \n");
      double[] answer = SolveRastrigin(dim, numRoaches, tMax, rndSeed);
      Console.WriteLine("\nRoach algorithm completed\n");

      double err = Error(answer);
      Console.WriteLine("Best error found = " + err.ToString("F6") + " at: \n");

      for (int i = 0; i < dim; ++i)
        Console.Write(answer[i].ToString("F4") + " ");
      Console.WriteLine("");

      Console.WriteLine("\nEnd roach infestation optimization demo\n");
      Console.ReadLine();
    } // Main

    static double[] SolveRastrigin(int dim, int numRoaches, int tMax, int rndSeed)
    {
      // estimate solution to Rastrigin's function using roach infestation optimization
      double C0 = 0.7;
      double C1 = 1.43;
      //double[] A = new double[] { 0.49, 0.63, 0.65 }; // probs of exchanging info - actual roach behavior!
      double[] A = new double[] { 0.2, 0.3, 0.4 }; // probs of exchanging info 
      int tHunger = tMax / 10;  // smaller divisor (e.g., 5 instead of 10) lets roaches stay longer
      double minX = -10.0;
      double maxX = 10.0;
      int extinction = tMax / 4;  // mass extinction
      Random rnd = new Random(rndSeed);  // for random process ordering

      Roach[] herd = new Roach[numRoaches];  // actually called an 'intrusion' of roaches!
      for (int i = 0; i < numRoaches; ++i)
        herd[i] = new Roach(dim, minX, maxX, tHunger, i); // random position

      int t = 0;  // loop counter (time)
      int[] indices = new int[numRoaches];
      for (int i = 0; i < numRoaches; ++i)
        indices[i] = i;

      double bestError = double.MaxValue;  // by any roach
      double[] bestPosition = new double[numRoaches];

      int displayInterval = tMax / 20;

      // distances between all pairs
      double[][] distances = new double[numRoaches][];
      for (int i = 0; i < numRoaches; ++i)
        distances[i] = new double[numRoaches];

      while (t < tMax)
      {
        if (t > 0 && t % displayInterval == 0)
        {
          Console.Write("time = " + t.ToString().PadLeft(6));
          Console.WriteLine(" best error = " + bestError.ToString("F5"));
        }

        // compute distances 
        for (int i = 0; i < numRoaches - 1; ++i)
        {
          for (int j = i + 1; j < numRoaches; ++j)
          {
            double d = Distance(herd[i].position, herd[j].position);
            distances[i][j] = distances[j][i] = d;
          }
        }

        // find median distance
        double[] sortedDists = new double[numRoaches * (numRoaches - 1) / 2];
        int k = 0;
        for (int i = 0; i < numRoaches - 1; ++i)
        {
          for (int j = i + 1; j < numRoaches; ++j)
          {
            sortedDists[k++] = distances[i][j];
            //++k;
          }
        }

        Array.Sort(sortedDists);
        double medianDist = sortedDists[sortedDists.Length / 4];  // changed to 'quarter median'

        Shuffle(indices, t); // t is used as a random seed

        for (int i = 0; i < numRoaches; ++i)  // each roach
        {
          int idx = indices[i]; // roach index

          Roach curr = herd[idx]; // ref to current roach
          int numNeighbors = 0;

          for (int j = 0; j < numRoaches; ++j)  // calculate number of neighbors of curr roach
          {
            if (j == idx) continue; // a roach is not a neighbor of itself
            double d = distances[idx][j];
            if (d < medianDist) // curr roach[idx] is a neighbor to roach[j]
              ++numNeighbors;
          }

          int effectiveNeighbors = numNeighbors;
          if (effectiveNeighbors >= 3)  // at most 3 neighbors are relevant
            effectiveNeighbors = 3;

          // possibly swap group best information
          for (int j = 0; j < numRoaches; ++j)
          {
            if (j == idx) continue; // a roach is not a neighbor of itself
            if (effectiveNeighbors == 0) continue; // no neighbor
            double prob = rnd.NextDouble();
            if (prob > A[effectiveNeighbors - 1]) continue; // don't always swap

            double d = distances[idx][j];
            if (d < medianDist) // curr roach[idx] is a neighbor to roach[j]
            {
              // exchange information about group best position
              if (curr.error < herd[j].error) // curr roach is better than roach[j]
              {
                for (int p = 0; p < dim; ++p)
                {
                  herd[j].groupBestPos[p] = curr.personalBestPos[p];  // use curr roach's personal best as the group best for both roachs
                  curr.groupBestPos[p] = curr.personalBestPos[p];
                }
              }
              else // roach[j] is better than curr
              {
                for (int p = 0; p < dim; ++p)
                {
                  curr.groupBestPos[p] = herd[j].personalBestPos[p];  // use roach[j]'s personal best as the group best for both roachs
                  herd[j].groupBestPos[p] = herd[j].personalBestPos[p];
                }

               }

            } // if a neighbor
          } // j, each neighbor

          if (curr.hunger < tHunger) // move roach
          {
            // new velocity
            for (int p = 0; p < dim; ++p)
              curr.velocity[p] = (C0 * curr.velocity[p]) + (C1 * rnd.NextDouble() * (curr.personalBestPos[p] - curr.position[p])) + (C1 * rnd.NextDouble() * (curr.groupBestPos[p] - curr.position[p]));

            // update position
            for (int p = 0; p < dim; ++p)
              curr.position[p] = curr.position[p] + curr.velocity[p];

            // update error
            //double e = RoachOptimizationProgram.Error(curr.position);
            double e = Error(curr.position);
            curr.error = e;

            // new personal best position?
            if (curr.error < curr.personalBestErr)
            {
              curr.personalBestErr = curr.error;
              for (int p = 0; p < dim; ++p)
                curr.personalBestPos[p] = curr.position[p];
            }

            // new global best position?
            if (curr.error < bestError)
            {
              bestError = curr.error;
              for (int p = 0; p < dim; ++p)
                bestPosition[p] = curr.position[p];
            }

            ++curr.hunger;
          }
          else // hungry. leave group, move to random position 
          {
            herd[idx] = new Roach(dim, minX, maxX, tHunger, t);
          }

        } // each roach

        if (t > 0 && t % extinction == 0)  // mass extinction?
        {
          Console.WriteLine("Mass extinction at t = " + t.ToString().PadLeft(6));
          for (int i = 0; i < numRoaches; ++i)
            herd[i] = new Roach(dim, minX, maxX, tHunger, i);
        }

        ++t;
      } // main while loop

      return bestPosition;

    } // SolveRastrigin

    public static double Error(double[] x)
    {
      // Rastrigin function has min value of 0.0
      double trueMin = 0.0;

      double rastrigin = 0.0;
      for (int i = 0; i < x.Length; ++i)
      {
        double xi = x[i];
        rastrigin += (xi * xi) - (10 * Math.Cos(2 * Math.PI * xi)) + 10;
      }

      return (rastrigin - trueMin) * (rastrigin - trueMin);
    }

    static double Distance(double[] pos1, double[] pos2)
    {
      double sum = 0.0;
      for (int i = 0; i < pos1.Length; ++i)
        sum += (pos1[i] - pos2[i]) * (pos1[i] - pos2[i]);
      return Math.Sqrt(sum);
    }

    static void Shuffle(int[] indices, int seed)
    {
      Random rnd = new Random(seed);
      for (int i = 0; i < indices.Length; ++i)
      {
        int r = rnd.Next(i, indices.Length);
        int tmp = indices[r];
        indices[r] = indices[i];
        indices[i] = tmp;
      }
    }

  } // Program

  public class Roach
  {
    public int dim;
    public double[] position;
    public double[] velocity;
    public double error; // at curr position

    public double[] personalBestPos; // best position found
    public double personalBestErr; // 

    public double[] groupBestPos; // best position by any group member
 
    public int hunger;
    private Random rnd;

    public Roach(int dim, double minX, double maxX, int tHunger, int rndSeed)
    {
      this.dim = dim;
      this.position = new double[dim];
      this.velocity = new double[dim];
      this.personalBestPos = new double[dim];
      this.groupBestPos = new double[dim];

      this.rnd = new Random(rndSeed);
      this.hunger = this.rnd.Next(0, tHunger);

      for (int i = 0; i < dim; ++i)
      {
        this.position[i] = (maxX - minX) * rnd.NextDouble() + minX;
        this.velocity[i] = (maxX - minX) * rnd.NextDouble() + minX;
        this.personalBestPos[i] = this.position[i];
        this.groupBestPos[i] = this.position[i];
      }

      this.error = RoachOptimizationProgram.Error(this.position);
      this.personalBestErr = this.error;
    }

  } // Roach

} // ns
