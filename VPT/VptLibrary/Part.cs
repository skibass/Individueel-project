﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VptLibrary
{
    // min 1 max 3 rows, every row is same length
    public class Part
    {
        public char Letter { get; set; }
        public List<Row> Rows { get; set; }       

        public Part(char letter) 
        {           
            this.Letter = letter;
            Rows = new List<Row>();
            GetRows();
        }

        private void GetRows()
        {
            int rowNr = 1;
            Random randRows = new Random();
            for (int i = 0; i < randRows.Next(1, 4); i++)
            {
                Row row = new Row(Letter, rowNr++);
                Rows.Add(row);
            }
        }
    }
}