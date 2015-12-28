using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiroNet1
{
    class Neiron
    {
        public  string name;
        public  double[,] veight;
        public  int countTrainig;

         public Neiron() {}
         public string GetName() { return name; }

         public void Clear(string name, int x, int y)
         {
             this.name = name;
             veight = new double[x,y];
             for (int n = 0; n < veight.GetLength(0); n++)
                 for (int m = 0; m < veight.GetLength(1); m++) veight[n, m] = 0;
             countTrainig = 0;
         }

         public double GetRes(int[,] data){
             if (veight.GetLength(0) != data.GetLength(0) || veight.GetLength(1) != data.GetLength(1)) return -1;
             double res = 0;
             for (int n = 0; n < veight.GetLength(0); n++)
                 for (int m = 0; m < veight.GetLength(1); m++) 
                     res += 1 - Math.Abs(veight[n, m] - data[n, m]);
             return res / (veight.GetLength(0) * veight.GetLength(1));
         }

         public int Training(int[,] data)
         {
             if (data == null || veight.GetLength(0) != data.GetLength(0) || veight.GetLength(1) != data.GetLength(1)) return countTrainig;
             countTrainig++;
             for (int n = 0; n < veight.GetLength(0); n++)
                 for (int m = 0; m < veight.GetLength(1); m++)
                 {
                     double v = data[n, m] == 0 ? 0 : 1;
                     veight[n, m] += 2 * (v - 0.5f) / countTrainig;
                     if (veight[n, m] > 1) veight[n, m] = 1;
                     if (veight[n, m] < 0) veight[n, m] = 0;
                 }
             return countTrainig;
         }
       
    }


}
