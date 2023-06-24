using System;
using CommunityToolkit.HighPerformance;

namespace Lab7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int[,] arr = new int[3,4];

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 3;
            dataGridView1.ColumnCount = 4;
        }

        private void LoadDataIntoView(DataGridView view, int[,] data)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    view.Rows[i].Cells[j].Value = data[i, j];
                }
            }
        }

        private int[] FindMinInEachRow(int[,] data)
        {
            Span2D<int> matrix = data;
            int[] result = new int[matrix.Height];
            for (int i = 0; i<matrix.Height; i++)
            {
                List<int> row = matrix.GetRow(i).ToArray().ToList();
                int minValue = row.Min();
                int minIndex = row.IndexOf(minValue);
                result[i] = minIndex;
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < this.arr.GetLength(0); i++)
            {
                for (int j = 0; j < this.arr.GetLength(1); j++)
                {
                    this.arr[i, j] = random.Next(-100, 100);
                }
            }
            this.LoadDataIntoView(dataGridView1, arr);
        }

        private void paintDataGrid(DataGridView view, int[] indices)
        {
            for (int i = 0; i<indices.Length; i++)
            {
                view.Rows[i].Cells[indices[i]].Style.BackColor = Color.Green;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int[] minIndices = this.FindMinInEachRow(this.arr);
            textBox1.Text = $"Минимальные значения каждой строки:{Environment.NewLine}";
            for (int i = 0; i < minIndices.Length; i++)
            {
                int val = this.arr[i, minIndices[i]];
                textBox1.AppendText($"{i+1}: {val}{Environment.NewLine}");
            }
            this.paintDataGrid(this.dataGridView1, minIndices);
        }
    }
}