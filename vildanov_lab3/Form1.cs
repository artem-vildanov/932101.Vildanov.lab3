using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vildanov_lab3
{
    public partial class Form1 : Form
    {
        private int RowCount = 0;
        private int CellsInRowCount = 20;
        private bool CellChangeColor = true;
        private char[] Rule = new char[8];
        private String[] Patterns = new string[] { "111", "110", "101", "100", "011", "010", "001", "000" };

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < CellsInRowCount; i++)
            {
                CellsFieldInForm.Columns.Add("", "");
            }

            RowCount = 1;
            CellsFieldInForm.Rows.Add();
            CellsFieldInForm.Rows[0].HeaderCell.Value = RowCount.ToString();

            SetRule(Convert.ToString((int)Ed1.Value, 2));

        }

        private void SetRule(string input)
        {
            int len = input.Length;
            while (len < 8)
            {
                input = "0" + input;
                len = input.Length;
            }
            for (int i = 0; i < input.Length; i++)
            {
                Rule[i] = input[i];
            }
        }
        private void edRule_ValueChanged(object sender, EventArgs e)
        {
            SetRule(Convert.ToString((int)Ed1.Value, 2));
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            CellChangeColor = false;
            timer1.Start();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            char[] PreviousRow = new char[CellsInRowCount];
            char[] NewRow = new char[CellsInRowCount];

            for (int i = 0; i < CellsInRowCount; i++)
            {
                if (CellsFieldInForm[i, RowCount - 1].Style.BackColor == Color.Green) PreviousRow[i] = '1';
                else PreviousRow[i] = '0';
            }

            for (int i = 0; i < CellsInRowCount; i++)
            {
                string Pattern = "";
                int LeftCell = i - 1;
                int RightCell = i + 1;
                var Builder = new StringBuilder();

                if (i == 0) LeftCell = CellsInRowCount - 1;
                if (i == CellsInRowCount - 1) RightCell = 0;

                Builder.Append(PreviousRow[LeftCell]);
                Builder.Append(PreviousRow[i]);
                Builder.Append(PreviousRow[RightCell]);

                Pattern = Builder.ToString();

                int index = Array.IndexOf(Patterns, Pattern);
                NewRow[i] = Rule[index];

            }

            RowCount = RowCount + 1;
            CellsFieldInForm.Rows.Add();
            CellsFieldInForm.Rows[RowCount - 1].HeaderCell.Value = RowCount.ToString();

            for (int i = 0; i < CellsInRowCount; i++)
            {
                if (NewRow[i] == '1') CellsFieldInForm[i, RowCount - 1].Style.BackColor = Color.Green;
                else CellsFieldInForm[i, RowCount - 1].Style.BackColor = Color.White;
            }

        }

        private void CellsFieldInForm_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CellChangeColor)
            {
                if (CellsFieldInForm[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.White)
                {
                    CellsFieldInForm[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Green;
                    CellsFieldInForm.ClearSelection();
                }
                else
                {
                    CellsFieldInForm[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.White;
                    CellsFieldInForm.ClearSelection();
                }
            }
        }
    }
}
