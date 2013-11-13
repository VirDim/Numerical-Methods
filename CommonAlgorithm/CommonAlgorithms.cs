using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonAlgorithm
{
    public class CommonAlgorithmException : Exception
    {
        public CommonAlgorithmException(string msg)
            : base("Error:\r\n" + msg) { }
    }

    public static class CommonAlgorithms
    {
        /// <summary>
        /// Transpose squre matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>Transponse matrix</returns>
        public static double[,] MatrixTranspose(double[,] matrix)
        {
            int size0 = matrix.GetLength(0);
            int size1 = matrix.GetLength(1);
            if (size0 == size1)
            {
                double[,] matrixTranspone = new double[size0, size0];
                for (int i = 0; i < size0; i++)
                    for (int j = 0; j < size0; j++)
                        matrixTranspone[i, j] = matrix[j, i];
                return matrixTranspone;
            }
            else
                throw new CommonAlgorithmException("matrix is not square");
        }

        /// <summary>
        /// Multiplication matrix
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>Multiplicated matrix</returns>
        public static double[,] MatrixMultiplication(double[,] matrix1, double[,] matrix2)
        {
            int sizeACollumn = matrix1.GetLength(1);
            int sizeARow = matrix1.GetLength(0);
            int sizeBRow = matrix2.GetLength(0);
            int sizeBCollumn = matrix2.GetLength(1);

            if (sizeACollumn == sizeBRow)
            {
                double[,] MultiplyMatrix = new double[sizeARow, sizeBCollumn];
                for (int i = 0; i < sizeARow; i++)
                    for (int j = 0; j < sizeBCollumn; j++)
                        for (int k = 0; k < sizeACollumn; k++)
                            MultiplyMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                return MultiplyMatrix;
            }
            else
                throw new CommonAlgorithmException("Size does not match");
        }

        /// <summary>
        /// Get identity matrix
        /// </summary>
        /// <param name="size"></param>
        /// <returns>identity matrix</returns>
        public static double[,] GetIdentityMatrix(int size)
        {
            double[,] matrix=new double[size,size];
            for (int i = 0; i < size; i++)
                matrix[i, i] = 1;
            return matrix;
        }

        /// <summary>
        /// Multiply matrix on number
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="number"></param>
        /// <returns>Multiplied matrix</returns>
        public static double[,] MultiplyMatrixOnNumber(double[,] matrix, double number)
        {
            int sizeRow = matrix.GetLength(0);
            int sizeCollumn = matrix.GetLength(1);
            double[,] tempMatrix = new double[sizeRow,sizeCollumn];
            for (int i = 0; i < sizeRow; i++)
                for (int j = 0; j < sizeCollumn; j++)
                    tempMatrix[i, j] = matrix[i, j] * number;
            return tempMatrix;
        }

        /// <summary>
        /// Multiplication vector on matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vector"></param>
        /// <returns>Multiplied vector</returns>
        public static double[] MultiplyVectorOnMatrix(double[,] matrix, double[] vector)
        {
            int sizeVector = vector.Length;
            int sizeMatrixRow = matrix.GetLength(0);
            int sizeMatrixCollumn = matrix.GetLength(1);
            double[] tempVector = new double[sizeVector];

            if (sizeVector == sizeMatrixRow)
            {
                for (int i = 0; i < sizeVector; i++)
                    for (int j = 0; j < sizeMatrixRow; j++)
                        tempVector[i] += vector[j] * matrix[i, j];
                return tempVector;
            }
            else
                throw new CommonAlgorithmException("Sizes do not match");
        }

        /// <summary>
        /// Get discrepancy
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="vectorB"></param>
        /// <param name="vectorX"></param>
        /// <returns>discrepancy</returns>
        public static double GetDiscrepancy(double[,] matrixA, double[] vectorB, double[] vectorX)
        {
            int size = vectorB.Length;
            int sizeCollumnMatrixA = matrixA.GetLength(1);
            double discrepansy = 0;
            double maxDiscrepansy = 0;
            for (int i = 0; i < size; i++)
            {
                double sum = 0;
                for (int j = 0; j < sizeCollumnMatrixA; j++)
                {
                    sum += matrixA[i, j] * vectorX[j];
                }
                discrepansy = Math.Abs(sum - vectorB[i]);
                if (maxDiscrepansy < discrepansy)
                    maxDiscrepansy = discrepansy;
            }
            return maxDiscrepansy;
        }

        /// <summary>
        /// Get trace matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static double GetTraceMatrix(double[,] matrix)
        {
            double sum=0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                sum += matrix[i, i];
            return sum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static double[,] DifferenceMatrix(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.Length != matrix2.Length) throw new CommonAlgorithmException("Size do not match");
            int size = matrix1.GetLength(0);
            double[,] difMatrix = new double[size,size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    difMatrix[i, j] = matrix1[i, j] - matrix2[i, j];
            return difMatrix;
        }

        /// <summary>
        /// Get matrix with ones
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static double[,] MatrixWithOnes(int size)
        {
            double[,] matrix = new double[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matrix[i, j] = 1;
            return matrix;
        }

        /// <summary>
        /// Pow matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double[,] PowMatrix(double[,] matrix,int n)
        {
            double[,] tempMatrix;
            if (n == 0)
                tempMatrix = MatrixWithOnes(matrix.Length);
            else
                tempMatrix = (double[,])matrix.Clone();
            for (int i = 0; i < n-1; i++)
                tempMatrix = MatrixMultiplication(tempMatrix, matrix);
            return tempMatrix;
        }

        /// <summary>
        /// Get Norm vector
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static double GetNormVector(double[] vector)
        {
            double sum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i]*vector[i];
            }
            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Get projection vecE vecA
        /// </summary>
        /// <param name="vectorE">down vector</param>
        /// <param name="vectorA">up vector</param>
        /// <returns></returns>
        public static double[] GetProjVector(double[] vectorE,double[] vectorA)
        {
            if (vectorE.Length != vectorA.Length) throw new CommonAlgorithmException("Size does not match");

            int size = vectorE.Length;
            double[] vectorProj = new double[size];

            double sumEA = 0,sumEE=0;
            for (int i = 0; i < size; i++)
            {
                sumEA += vectorE[i] * vectorA[i];
                sumEE += vectorE[i] * vectorE[i];
            }
            double sum = sumEA / sumEE;
            for (int i = 0; i < size; i++)
            {
                vectorProj[i] = vectorE[i] * sum;
            }
            return vectorProj;
        }

    }
}
