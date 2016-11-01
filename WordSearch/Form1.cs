using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace WordSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Init random
            rand = new Random();

            words = new List<GridWord>() {
                new GridWord("integer"),
                new GridWord("array"),
                new GridWord("byte"),
                new GridWord("boolean"),
                new GridWord("for"),
                new GridWord("string"),
                new GridWord("parameter"),
                new GridWord("while"),
                new GridWord("method"),
                new GridWord("loop")};

            //Display the default wors
            DisplayWordsInBox();

            if (Debugger.IsAttached)
                btnDebug.Visible = true;
        }

        //Size of the wordsearch
        private int size;
        //The random var
        private static Random rand;
        //Letters for filling cells
        private const string Letters = "abcdefghijklmnopqrstuvwxyz";

        private List<GridWord> words;

        private char[,] chars;

        private void btnGo_Click(object sender, EventArgs e)
        { 
            //Get size of GridWord Search
            if (!GetSize())
                return;

            //Check size of the grid
            if (!CheckSize())
                return;

            chars = new char[size,size];

            //Set size of GridWord Search
            dataGridView1.RowCount = dataGridView1.ColumnCount = size;

            //Ensure grid is refreshed every time
            //EmptyGrid();

            //Work out where words will go
            WriteWordsInArray();

            //Write the words in the grid
            WriteWordsInGrid();
        }

        private void btnAddWord_Click(object sender, EventArgs e)
        {
            //If it's not all letters show a message
            if (!txtAddWord.Text.All(char.IsLetter)
                || txtAddWord.Text == "")
            {
                MessageBox.Show("The value entered for size was not accepted." +
                                "\n Please only enter letters", "Invalid Input");
                return;
            }
            //Add the word
            words.Add(new GridWord(txtAddWord.Text));
            //Clear the text box
            txtAddWord.Text = "";
            //Refresh word display
            DisplayWordsInBox();
        }

        private void btnRemoveWord_Click(object sender, EventArgs e)
        {
            //Check if the word is in the list, if so remove it
            foreach (var word in words)
            {
                if (txtRemoveWord.Text != word.Word) continue;
                words.Remove(word);
                break;
            }
            //Clear the text box
            txtRemoveWord.Text = "";
            //Refresh word display
            DisplayWordsInBox();
        }

        private void btnShowAnswers_Click(object sender, EventArgs e)
        {
            //Avoid error if grid is not setup
            if (words == null)
                return;

            if (btnShowAnswers.Text == "Show Answers")
            {
                ShowAnswers();
                btnShowAnswers.Text = "Hide Answers";
            }
            else
            {
                HideAnswers();
                btnShowAnswers.Text = "Show Answers";
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
                return;

            CheckAnswer();

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                cell.Selected = false;
            }

            DisplayWordsInBox();
        }

        private void CheckAnswer()
        {
            foreach (GridWord word in words)
            {
                if (word.Length != dataGridView1.SelectedCells.Count) continue;

                if (!CheckHighlight(word)) continue;

                HighLightWord(word);
                word.Found = true;
            }
        }

        private bool CheckHighlight(GridWord word)
        {
            int rowChange = getRowChange(word.Direction);
            int columnChange = getColumnChange(word.Direction);

            var step = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (!dataGridView1.Rows[word.Row + (rowChange*i)].Cells[word.Column + (columnChange*i)].Selected)
                    return false;
            }
            return true;
        }

        private void ShowAnswers()
        {
            //Highlight words
            foreach (GridWord word in words)
            {
                HighLightWord(word);
            }
        }

        private void HighLightWord(GridWord word)
        {
            int rowChange = getRowChange(word.Direction);
            int columnChange = getColumnChange(word.Direction);

            var step = 0;
            for (int i = 0; i < word.Length; i++)
            {
                dataGridView1.Rows[word.Row + (rowChange * i)].Cells[word.Column + (columnChange * i)].Style.BackColor =
                    Color.LightGreen;
            }
        }

        private void HideAnswers()
        {
            //UnHighlight words (Instead of doing all)
            foreach (GridWord word in words)
            {
                int rowChange = getRowChange(word.Direction);
                int columnChange = getColumnChange(word.Direction);

                var step = 0;
                for (int i = 0; i < word.Length; i++)
                {
                    dataGridView1.Rows[word.Row + (rowChange*i)].Cells[word.Column + (columnChange*i)].Style.BackColor =
                        Color.White;
                }
            }
            //Chose to remove this
            foreach (var word in words)
            {
                word.Found = false;
            }
        }

        private void DisplayWordsInBox()
        {
            //Clear the box
            rtxtWords.Text = "";
            //Write out the words on seperate lines

            foreach (var word in words)
            {
                rtxtWords.Text += word.Word + "\n";
                if (word.Found)
                {
                    rtxtWords.SelectionFont = new Font(rtxtWords.SelectionFont, FontStyle.Strikeout);
                }
            }
            //TODO Strikethrough
        }

        private void WriteWordsInGrid()
        {
            for (var row = 0; row < size; row++)
            {
                for (var column = 0; column < size; column++)
                {
                    dataGridView1.Rows[row].Cells[column].Value = (chars[row, column] != 0
                        ? chars[row, column]
                        : RandomChar());
                }
            }
        }

        private void WriteWordsInArray()
        {
            //Pick random direction, and search for a suitable space for the word until found
            foreach (var word in words)
            {
                int direction;
                int row;
                int column;
                int rowChange;
                int columnChange;
                do
                {
                    direction = rand.Next(4);
                    //direction = 0;
                    rowChange = getRowChange(direction);
                    columnChange = getColumnChange(direction);
                    column = rand.Next(size);
                    row = rand.Next(size);

                    //Checking for overflow edge of grid
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

                } while (!isValidStart(row, column, rowChange, columnChange, word.Word));

                //Writing the actual word
                var step = 0;
                foreach (var c in word.Word.ToCharArray())
                {
                    chars[row + (rowChange*step), column + (columnChange*step)] = c;
                    //dataGridView1.Rows[row + (rowChange*step)].Cells[column + (columnChange*step)].Value = c;
                    step++;
                }

                word.Row = row;
                word.Column = column;
                word.Direction = direction;
            }

            //FillEmptyCells();
        }

        private void FillEmptyCells()
        {
            //Fill all the empty cells with random chars
            for (var row = 0; row < size; row++)
            {
                for (var column = 0; column < size; column++)
                {
                    if (chars[row, column] != 0) continue;
                    chars[row, column] = RandomChar();
                }
            }

            /*foreach (var cell in from DataGridViewRow gridRow in dataGridView1.Rows
                                 from DataGridViewCell cell in gridRow.Cells
                                 select cell)
            {
                if (cell.Value == "")
                    cell.Value = RandomChar();
            }*/
        }

        private bool isValidStart(int row, int column, int rowChange, int columnChange, string word)
        {
            //Check whether start is actually valid
            var step = 0;
            foreach (var c in word.ToCharArray())
            {
                if (chars[row + (rowChange * step), column + (columnChange * step)] == 0)
                {
                    step++;
                    continue;
                }
                if (c.Equals(chars[row + (rowChange * step), column + (columnChange * step)]))
                {
                    step++;
                    continue;
                }
                return false;
            }
            return true;
        }

        private int getRowChange(int direction)
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

        private int getColumnChange(int direction)
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
            return Letters[rand.Next(Letters.Length)];
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
            //Set a max size of 40 (still 1.6k boxes)
            if (size > 40)
            {
                MessageBox.Show("The value entered for size is too large" +
                                "\nThe max size available is 40", "Invalid Size");
                return false;
            }

            //Make sure size = longest word x 2
            foreach (var word in words)
            {
                if (word.Length*2 <= size) continue;
                MessageBox.Show("The value entered for size is too small" +
                                "\nIt needs to be double the length of the longest word" +
                                "\n\"" + word.Word + "\" is too long for the size", "Invalid Size");
                return false;
            }

            return true;
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
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
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

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnGo_Click(sender, new EventArgs());
        }

        private void txtAddWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddWord_Click(sender, new EventArgs());
        }

        private void txtRemoveWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnRemoveWord_Click(sender, new EventArgs());
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            Stopwatch timer = new Stopwatch();

            timer.Start();
            btnGo_Click(sender, e);
            timer.Stop();
            Debug.Print(timer.ElapsedMilliseconds.ToString());

            timer.Reset();
            long time = 0;
            for (int i = 0; i < 100; i++)
            {
                timer.Start();
                btnGo_Click(sender, e);
                timer.Stop();
                //Debug.Print(timer.ElapsedMilliseconds.ToString());
                time += timer.ElapsedMilliseconds;
                timer.Reset();
            }
            timer.Stop();
            time = time/100;
            Debug.Print(time.ToString());

            timer.Reset();
            timer.Start();
            btnGo_Click(sender, e);
            timer.Stop();
            Debug.Print(timer.ElapsedMilliseconds.ToString());
            /*foreach (var word in words)
            {
                Debug.Print(word.ToString());
            }*/
        }
    }

    internal class GridWord
    {
        public string Word { get; }
        public int Length { get; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Direction { get; set; }
        public bool Found { get; set; }

        public GridWord(string word)
        {
            this.Word = word;
            this.Length = word.Length;
        }

        public override string ToString()
        {
            return $"[{Word}]. Starting at {Column}, {Row}. Going in direction {Direction}. Found = {Found}";
        }
    }
}
