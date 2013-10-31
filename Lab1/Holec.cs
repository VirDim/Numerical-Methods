using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Holec
    {
        public double[,] MatrixA, MatrixAMod, MatrixL, MatrixLt, MatrixAT;
        public double[] VectorB, VectorY, VectorX, VectorBMod;
        int Size;

        public Holec(double[,] A, double[] B)
        {
            MatrixA = A;
            VectorB = B;
            Size = B.Length;
            MatrixL = new double[Size, Size];
            MatrixLt = new double[Size, Size];
            MatrixAMod = new double[Size, Size];
            VectorY = new double[Size];
            VectorX = new double[Size];
            VectorBMod = new double[Size];
            MatrixAT = new double[Size, Size];
            HolecSolve();

        }

        public double[] X_vector { get; private set; }

        public void HolecSolve()
        {
            MatrixTraspon();
            MatrixAct0();
            VectorAct0();
            FindMatrixL();
            SolveY();
            TranspMatrixL();
            SolveX();

        }

        //получаем транспонированную матрицу
        void MatrixTraspon()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    MatrixAT[i, j] = MatrixA[j, i];

        }

        //делаем симетричную матрицу
        void MatrixAct0()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    for (int k = 0; k < Size; k++)
                        MatrixAMod[j, i] += MatrixAT[i, k] * MatrixA[k, j];
                }
            }
        }




        //перемножаем вектор на матрицу
        void VectorAct0()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    VectorBMod[i] += VectorB[j] * MatrixAT[i, j];
                }
            }

        }


        //Находим матрицу L
        void FindMatrixL()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    double Sum = 0;
                    for (int k = 0; k < j; k++)
                    {
                        Sum += MatrixL[i, k] * MatrixL[j, k];
                    }
                    MatrixL[i, j] = (MatrixAMod[i, j] - Sum) / MatrixL[j, j];
                }
                double Sum1 = 0;
                for (int k = 0; k < i; k++)
                    Sum1 += MatrixL[i, k] * MatrixL[i, k];
                MatrixL[i, i] = Math.Sqrt(MatrixAMod[i, i] - Sum1);
            }
        }

        void TranspMatrixL()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    MatrixLt[i, j] = MatrixL[j, i];
        }

        //вычисляем Ly=b
        void SolveY()
        {
            for (int i = 0; i < Size; i++)
            {
                double Sum = VectorBMod[i];
                for (int j = 0; j < i; j++)
                {
                    Sum -= MatrixL[i, j] * VectorY[j];
                }
                VectorY[i] = Sum / MatrixL[i, i];
            }
        }

        //вычисляем lt*x=y
        void SolveX()
        {
            for (int i = Size - 1; i >= 0; i--)
            {
                double Sum = VectorY[i];
                for (int j = Size - 1; j > i; j--)
                {
                    Sum -= MatrixLt[i, j] * VectorX[j];
                }
                VectorX[i] = Sum / MatrixLt[i, i];
            }
        }

    }
}
