using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Diagnostics;
using System.Numerics;

namespace TypeTesting
{
    public partial class Form2 : Form
    {
        public readonly int minutes = 1;
        private int _ticks;
        private int total_time;

        private string test_string = @"Editing is a growing field of work in the service industry.
Paid editing services may be provided by specialized editing firms or
by self-employed (freelance) editors.";

        public Form2()
        {
            InitializeComponent();
            Debug.WriteLine("Debug Information-Product Starting ");
            Debug.Indent();
            _ticks = minutes * 60;
            label1.Text = _ticks.ToString();
            label2.Text = time_format(_ticks);
            // label2.Text = string str = time.ToString(@"hh\:mm\:ss\:fff");
            label3.Text = test_string.ToString();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label3.Hide();
            textBox1.Hide();
            button2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Hide();
            button2.Show();
            timer1.Start();
            label3.Show();
            textBox1.Show();
            
        }

    private static string time_format(int secs)
    {
        int hours = secs / 3600;
        int mins = (secs % 3600) / 60;
        secs = secs % 60;
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, mins, secs);
    }

    private static int GetWordCount(string text)
    {
        int wordCount = 0, index = 0;

        // skip whitespace until first word
        while (index < text.Length && char.IsWhiteSpace(text[index]))
            index++;

        while (index < text.Length)
        {
            // check if current char is part of a word
            while (index < text.Length && !char.IsWhiteSpace(text[index]))
                index++;

            wordCount++;

            // skip whitespace until next word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;
        }

        return wordCount;
    }

    private string evaluate_result(int time_remaining, string typed_text)
    {
        total_time = minutes * 60;
        int test_time_seconds = total_time - time_remaining;
        decimal test_time_minutes = (Convert.ToDecimal(total_time) - Convert.ToDecimal(time_remaining)) / Convert.ToDecimal(60);
        Debug.WriteLine("TotalTime is =" + total_time + " seconds");
        Debug.WriteLine("TimeRemaining is =" + time_remaining + " seconds");
        Debug.WriteLine("Time Utilised is =" + test_time_seconds.ToString() + " seconds");
        Debug.WriteLine("TestTime is =" + test_time_minutes + " minutes");

        MessageBox.Show(typed_text.ToString());
        Debug.WriteLine("Typed Text is == " + typed_text);
        Debug.WriteLine("No. of words typed is = " + GetWordCount(typed_text));
        int countSpaces = typed_text.Count(Char.IsWhiteSpace);
        Debug.WriteLine("countSpaces is = " + countSpaces);
        int countWords = typed_text.Split().Length;
        Debug.WriteLine("countWords is = " + countWords);
        int typed_length = typed_text.Length;
        Debug.WriteLine("TypedLength is = " + typed_length);

        //int gross_words = GetWordCount(typed_text);
        decimal gross_words = typed_length / 5;
        Debug.WriteLine("gross_words is = " + gross_words);
        decimal GWPM = gross_words / test_time_minutes;
        Debug.WriteLine("GWPM is = " + GWPM);

        //
        // get the diff between 2 strings
        //
        List<string> diff;
        IEnumerable<string> set1 = test_string.Split(' ').Distinct();
        IEnumerable<string> set2 = typed_text.Split(' ').Distinct();

        if (set2.Count() > set1.Count())
        {
            diff = set2.Except(set1).ToList();
        }
        else
        {
            diff = set1.Except(set2).ToList();
        }

        //Debug.WriteLine(diff);
        Debug.WriteLine(String.Join("\n", diff));
        Debug.WriteLine("diff in strings is = " + diff.Count);
        //Debug.WriteLine(String.Compare(test_string, typed_text));
        //Debug.WriteLine(EqualsExcludingWhitespace(test_string, typed_text));

        int net_words = GetWordCount(test_string) - diff.Count;
        Debug.WriteLine("net_words is = " + net_words);
        decimal NWPM = net_words / test_time_minutes;
        Debug.WriteLine("NWPM is =" + NWPM);

        int typing_accuracy = Convert.ToInt32(NWPM) * 100 / Convert.ToInt32(GWPM);
        Debug.WriteLine("Typing Accuracy is =" + typing_accuracy + "%");

        return time_remaining.ToString();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        _ticks--;
        label1.Text = _ticks.ToString();
        label2.Text = time_format(_ticks);

        if (_ticks == 0)
        {
            //this.Text = "Done";
            timer1.Stop();
            //MessageBox.Show(textBox1.Text);
            MessageBox.Show("Time Out. The Test is Closed now");
            evaluate_result(_ticks, textBox1.Text);
            this.Close();
        }
    }

    private void label3_Click(object sender, EventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
        timer1.Stop();
        var confirmResult = MessageBox.Show("Are you sure you want to finish this test ??", "Please Confirm", MessageBoxButtons.YesNo);
        //MessageBox.Show("Total Characters are:" + textBox1.Text.Length);

        if (confirmResult == DialogResult.Yes)
        {
            // If 'Yes', do something here.
            timer1.Stop();
            
            //MessageBox.Show(textBox1.Text);
            evaluate_result(_ticks, textBox1.Text);
            this.Close();
        }
        else
        {
            // If 'No', do something here.
            timer1.Start();

        }
    }
}
}
