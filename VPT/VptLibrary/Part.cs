﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VptLibrary
{
    // min 1 max 3 rows, every row is same length
    public class Part
    {
        public char Letter { get; set; }
        private bool IsPartFull { get; set; }
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
            Random randChairs = new Random();
            int amountOfChairsThisRow = randChairs.Next(3, 11);

            for (int i = 0; i < randRows.Next(1, 4); i++)
            {
                Row row = new Row(Letter, rowNr++, amountOfChairsThisRow);
                Rows.Add(row);
            }
        }

        public void SetupRows(List<Visitor> grouplessVisitors, List<Group> groups, List<Visitor> allVisitors)
        {          
            // TODO: Sometimes stays stuck in loop because of certain conditions, fix this
            if (allVisitors.Count(v => v.SignedOnTime) > 0)
            {
                foreach (var item in Rows)
                {
                    bool readyForNextRow = false;

                    while (!RowIsReady(item, allVisitors))
                    {
                        item.PlaceGroups(groups, ref readyForNextRow);
                        item.PlaceGrouplessVisitors(grouplessVisitors, ref readyForNextRow);
                        var t = item.Chairs.Count(v => v.IsTaken == true);
                        var f = allVisitors.Count(v => v.SignedOnTime);
                    }

                    if (item.Chairs.Count(v => v.IsTaken == true) == item.Chairs.Count())
                    {
                        item.IsRowFull = true;
                    }
                }
            }

        }
        private bool RowIsReady(Row item, List<Visitor> allVisitors)
        {
            if (allVisitors.Count(v => v.SignedOnTime && v.IsSeated) == allVisitors.Count(v => v.SignedOnTime))
            {
                return true;
            }
            else if (item.Chairs.Count(v => v.IsTaken == true) < item.Chairs.Count())
            {
                return false;
            }       
            return true;
        }        
    }
}
