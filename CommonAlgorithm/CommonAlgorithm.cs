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

    public class CommonAlgorithm
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
    }
}
