using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Appl.Models.BusinessLayer.Account
{
    public static class InputEncoding
    {
        public static string DecodePassword(string password)
        {
            string[] hash = new string[] {"aGs", "asS", "1as", "asd", "12s", "_as", "_as", "ouk", "por", "pek",
                "12d", "vbf", "mat", "qpg", "3yh", "asr", "098", "xcl", "laa", "ASe",
                "pka", "1rs", "9ps", "4p7", "993", "128",
                "ASW", "QWE", "TTR", "EWE", "AAA", "PRT", "Y6T", "LKL", "MNB", "OIP",
                "QTY", "VVB", "MNM", "ZZX", "XCX", "CVC", "VBV", "MLP", "NJI", "VGY",
                "ZSE", "XDR", "CFT", "BHU", "UHB", "TFC",
                "__1", "154", "165", "098", "1pl", "wer", "100", "119", "mbo", "lLl",
                "776", "112", "345", "111", "jkl", "hhh", "GGG" };
            string[] chars = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                ".", "/", "_", "-", ":", "?", "=" };

            double parts = 3;
            int k = 0;
            string[] separated = password
                .ToLookup(c => Math.Floor(k++ / parts))
                .Select(e => new String(e.ToArray()))
                .ToArray();

            StringBuilder sb = new StringBuilder();
            int index;

            for (int i = 0; i < separated.Length; i++)
            {
                index = Array.IndexOf(hash, separated[i]);

                if (index == -1)
                {
                    continue;
                }

                sb.Append(chars[index]);
            }

            return sb.ToString();
        }
    }
}