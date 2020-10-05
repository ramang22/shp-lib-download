using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_sharepoint_download_samuel.Visual
{
    class ProgressBar
    {
        public int progress { get; set; }
        public float total { get; set; }
        public float onechunk { get; set; }

        public bool bar { get; set; }
        public ProgressBar(float total)
        {
            progress = 0;
            this.total = total;
            onechunk = 31.0f / total;
            bar = true;
        }

        public ProgressBar()
        {
            progress = 0;
            this.total = 0;
            onechunk = 0;
            bar = false;
        }


        private void cleanProgressBar()
        {
            Console.CursorVisible = true;
        }

        public void drawProgress(string fileName)
        {
            if (this.bar)
            {
                drawTextProgressBar(fileName);
            }
            else
            {
                drawProgressText(fileName);
            }
        }

        private void drawTextProgressBar(string fileName)
        {
            Console.CursorVisible = false;
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start

            Console.CursorLeft = 1;

            Console.CursorLeft = 32;
            Console.Write("]"); //end
            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(String.Format("{0} / {1}      ", progress.ToString(), total.ToString()));
        }


        private void drawProgressText(string fileName)
        {
            Console.CursorVisible = false;
            Console.CursorLeft = 0;
            Console.Write(string.Format("DOWNLOADED : {0} files", progress));
        }

        internal void printEndMessage(int notDownloaded)
        {
            cleanProgressBar();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            if (this.bar)
            {
                Console.WriteLine("Succesesful downloads: " + (this.total - notDownloaded));
            }
            else
            {
                Console.WriteLine("Succesesful downloads: " + (this.progress - notDownloaded));
            }
        }
    }
}
