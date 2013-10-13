using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {

        double[,] A = new double[,]{
                {1.54, 1.7 , 1.62},
                {3.69, 3.73, 3.59},
                {2.45, 2.43, 2.25}};
        double[] B = new double[] { -1.97, -3.74, -2.26 };

        public Form1()
        {
            InitializeComponent();

            

             
            dataGridView1.RowCount = 3;
            dataGridView1.ColumnCount = 7;
            for (int i = 0; i < B.Length; i++)
            {
                for (int j = 0; j < B.Length; j++)
                {
                    dataGridView1[i, j].Value = A[j, i];
                }
                dataGridView1[B.Length,i].Value = B[i];
            }


            
            
            
            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.RowCount = (int)numericUpDown1.Value;
            dataGridView1.ColumnCount = (int)numericUpDown1.Value + 4;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int size = dataGridView1.RowCount;
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    A[i, j] = Convert.ToDouble(dataGridView1[j, i].Value);
                }
                B[i] = Convert.ToDouble(dataGridView1[size, i].Value);
            }

            Holec hol = new Holec(A, B);
            Gaus.Gaus LS = new Gaus.Gaus(A, B);
           

            for (int i = 0; i < size; i++)
            {
                dataGridView1[dataGridView1.ColumnCount - 2, i].Value = LS.XVector[i];
                dataGridView1[dataGridView1.ColumnCount - 1, i].Value = hol.VectorX[i];
            }

            

            

            
        }
    }
}
