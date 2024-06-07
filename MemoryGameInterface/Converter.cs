using System;

namespace MemoryGameInterface
{
    internal class Converter
    {
        public static int ConvertLetterIndexToNumericIndex(char i_LetterIndex)
        {
            int resultOfConverting;

            if (('A' < i_LetterIndex && i_LetterIndex < 'Z' || 'a' < i_LetterIndex && i_LetterIndex < 'z'))
            {
                if (('A' < i_LetterIndex && i_LetterIndex < 'Z'))
                {
                    resultOfConverting = i_LetterIndex - 'A';
                }
                else
                {
                    resultOfConverting = i_LetterIndex - 'a';
                }

                return resultOfConverting;
            }
            else
            {
                throw new ArgumentException("Letter Index accept values [a-z, A-Z].");
            }
        }

        public static char ConvertNumericIndexToLetterIndex(int i_NumericIndex)
        {
            const int k_NumberOfLetters = 26;
            if (0 <= i_NumericIndex && i_NumericIndex < k_NumberOfLetters)
            {
                char resultOfConverting = (char)('A' + i_NumericIndex);
                return resultOfConverting;
            }
            else
            {
                throw new ArgumentException("LetterIndex as a numeric value accept values [1-26].");
            }
        }

        public static bool ConvertYesOrNoToBool(string i_UserInput)
        {
            bool userInputIsYes;
            string userInputUpperCase = i_UserInput.ToUpper();

            if (userInputUpperCase == "Y")
            {
                userInputIsYes = true;
            }
            else
            {
                userInputIsYes = false;
            }

            return userInputIsYes;
        }

        public class CellReferenceConverter
        {
            public static (int, int) ConvertCellIndex(string i_CellReference)
            {
                if (string.IsNullOrEmpty(i_CellReference))
                {
                    throw new ArgumentException("Cell reference cannot be null or empty.");
                }

                if (i_CellReference.Length < 2)
                {
                    throw new ArgumentException("Cell reference cannot be shorter than two characters.");
                }

                int column = 0;
                int row = 0;

                if (char.IsLetter(i_CellReference[0]))
                {
                    char chosenLetterToUpper = char.ToUpper(i_CellReference[0]);
                    column = chosenLetterToUpper - 'A';
                }
                else
                {
                    throw new ArgumentException("Cell reference have to start with letter.");
                }

                if (int.TryParse(i_CellReference.Substring(1), out row) && row >= 1)
                {
                    return (column, row - 1);
                }
                else
                {
                    throw new ArgumentException("Invalid cell reference format, The cell reference should include number (>=1) after the letter.");
                }
            }
        }
    }
}
