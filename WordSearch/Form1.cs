using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

namespace WordSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Init random
            rand = new Random();
            //Default words
            words = new ArrayList()
            {
                "integer",
                "array",
                "byte",
                "boolean",
                "for",
                "string",
                "parameter",
                "while",
                "method",
                "loop"
            };
            //Display the default wors
            DisplayWords();
        }

        //Size of the wordsearch
        private int size;
        //The random var
        public static Random rand;
        //The words in the word search
        private ArrayList words;
        //Letters for filling cells
        private const string letters = "abcdefghijklmnopqrstuvwxyz";

        private void btnGo_Click(object sender, EventArgs e)
        {
            //Get size of Word Search
            if (!GetSize())
                return;

            //Check size of the grid
            if (!CheckSize())
                return;

            //Set size of Word Search
            dataGridView1.RowCount = dataGridView1.ColumnCount = size;
            //Ensure grid is refreshed every time
            EmptyGrid();
            //Fill the word search
            WriteWords();
        }

        private void btnAddWord_Click(object sender, EventArgs e)
        {
            //If it's not all letter show a message
            if (!txtAddWord.Text.All(char.IsLetter)
                || txtAddWord.Text == "")
            {
                MessageBox.Show("The value entered for size was not accepted." +
                                "\n Please only enter letters", "Invalid Input");
                return;
            }
            //Add the word
            words.Add(txtAddWord.Text);
            //Clear the text box
            txtAddWord.Text = "";
            //Refresh word display
            DisplayWords();
        }

        private void btnRemoveWord_Click(object sender, EventArgs e)
        {
            //Check if the word is in the list, if so remove it
            foreach (string word in words)
            {
                if (txtRemoveWord.Text != word) continue;
                words.Remove(word);
                break;
            }
            //Clear the text box
            txtRemoveWord.Text = "";
            //Refresh word display
            DisplayWords();
        }

        private void DisplayWords()
        {
            //Clear the box
            rtxtWords.Text = "";
            //Write out the words on seperate lines
            foreach (var word in words)
            {
                rtxtWords.Text += word + "\n";
            }
        }

        private void WriteWords()
        {
            //Pick a random direction and write the word
            foreach (string word in words)
            {
                int direction;
                int row;
                int column;
                int rowChange;
                int columnChange;
                do
                {
                    //direction = rand.Next(4);
                    direction = 0;
                    rowChange = getRowChange(direction);
                    columnChange = getColumnChange(direction);
                    column = rand.Next(size);
                    row = rand.Next(size);

                    switch (direction)
                    {
                        case 0:
                            row = checkOverflowUp(row, word.Length);
                            break;
                        case 1:
                            column = checkOverflowRight(column, word.Length);
                            break;
                        case 2:
                            row = checkOverflowDown(row, word.Length);
                            break;
                        case 3:
                            column = checkOverflowLeft(column, word.Length);
                            break;
                    }

                } while (!isValidStart(row, column, rowChange, columnChange, word));

                var step = 0;
                foreach (var c in word.ToCharArray())
                {
                    dataGridView1.Rows[row + (rowChange * step)].Cells[column + (columnChange * step)].Value = c;
                    step++;
                }

            }

            //Fill all the empty cells with random chars
            foreach (var cell in from DataGridViewRow gridRow in dataGridView1.Rows
                from DataGridViewCell cell in gridRow.Cells
                select cell)
            {
                if (cell.Value == "")
                    cell.Value = RandomChar();
            }
        }

        private bool isValidStart(int row, int column, int rowChange, int columnChange, string word)
        {
            var step = 0;
            foreach (var c in word.ToCharArray())
            {
                if (dataGridView1.Rows[row + (rowChange*step)].Cells[column + (columnChange*step)].Value == "")
                {
                    step++;
                    continue;
                }
                if (c.Equals(dataGridView1.Rows[row + (rowChange*step)].Cells[column + (columnChange*step)].Value))
                {
                    step++;
                    continue;
                }
                return false;
            }
            return true;
        }

        private int getColumnChange(int direction)
        {
            switch (direction)
            {
                case 0:
                    return -1;
                case 2:
                    return 1;
                default:
                    return 0;
            }  
        }

        private int getRowChange(int direction)
        {
            switch (direction)
            {
                case 1:
                    return 1;
                case 3:
                    return -1;
                default:
                    return 0;
            }
        }


        private char RandomChar()
        {
            //Getting a random char
            return letters[rand.Next(letters.Length)];
        }

        private bool GetSize()
        {
            //Show error if size isn't a number
            try
            {
                size = Convert.ToInt32(txtSize.Text);
            }
            catch (FormatException e)
            {
                MessageBox.Show("The value entered for size was not accepted." +
                                "\n Please try again", "Invalid Input");
                return false;
            }
            return true;
        }

        private bool CheckSize()
        {
            if (size > 40)
            {
                MessageBox.Show("The value entered for size is too large" +
                                "\nThe max size available is 40", "Invalid Size");
                return false;
            }

            foreach (string word in words)
            {
                if (word.Length*2 <= size) continue;
                MessageBox.Show("The value entered for size is too small" +
                                "\nIt needs to be double the length of the longest word" +
                                "\n\"" + word + "\" is too long for the size", "Invalid Size");
                return false;
            }

            return true;

            //Check whether size < longest word
        }

        private int checkOverflowUp(int row, int length)
        {
            if ((row - length) < 0)
                row -= row - length;
            return row;
        }

        private int checkOverflowDown(int row, int length)
        {
            if ((row + length) >= size)
                row -= ((row + length) - size);
            return row;
        }

        private int checkOverflowLeft(int column, int length)
        {
            if ((column - length) < 0)
                column -= column - length;
            return column;
        }

        private int checkOverflowRight(int column, int length)
        {
            if ((column + length) >= size)
                column -= ((column + length) - size);
            return column;
        }

        private void EmptyGrid()
        {
            //Set value in every cell to empty
            foreach (var cell in from DataGridViewRow row in dataGridView1.Rows
                from DataGridViewCell cell in row.Cells
                select cell)
            {
                cell.Value = string.Empty;
            }
        }

        private static void CheckWordKeypress(KeyPressEventArgs e)
        {
            //Ignore keypress if it isn't a letter
            e.Handled = !char.IsLetter(e.KeyChar);
        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Only allow numbers
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAddWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeypress(e);
        }

        private void txtRemoveWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckWordKeypress(e);
        }
    }
}
