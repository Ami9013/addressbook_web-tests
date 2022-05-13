using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace WebAddressbookTests
{
    public class FundistUserData : IEquatable<FundistUserData>
    {
        public string Id { get; set; }
        public string DateOfBet { get; set; }
        public int CountOfBet { get; set; } = 0;
        public double AmountOfBet { get; set; } = 0;
        public int CountOfWin { get; set; } = 0;
        public double AmountOfWin { get; set; } = 0;        
        public string GameOfUser { get; set; }


        public bool Equals(FundistUserData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            
            return AmountOfBet == other.AmountOfBet && AmountOfWin == other.AmountOfWin && CountOfBet == other.CountOfBet && CountOfWin == other.CountOfWin;
        }

    }
}
