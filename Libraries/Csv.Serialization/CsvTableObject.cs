namespace ProcessingTools.Csv.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CsvTableObject
    {
        private Queue<string[]> rows;

        public CsvTableObject(
            char fieldTerminator = ',',
            char rowTerminator = '\n',
            int firstRow = 1,
            char singleCharEscapeSymbol = '\\',
            char terminatorEscapeLeftWrapSymbol = '"',
            char terminatorEscapeRightWrapSymbol = '"')
        {
            this.FieldTerminator = fieldTerminator;
            this.RowTerminator = rowTerminator;
            this.FirstRow = firstRow;
            this.SingleCharEscapeSymbol = singleCharEscapeSymbol;
            this.TerminatorEscapeLeftWrapSymbol = terminatorEscapeLeftWrapSymbol;
            this.TerminatorEscapeRightWrapSymbol = terminatorEscapeRightWrapSymbol;

            this.rows = new Queue<string[]>();
        }

        public char FieldTerminator { get; set; }

        public char RowTerminator { get; set; }

        public int FirstRow { get; set; }

        public char SingleCharEscapeSymbol { get; set; }

        public char TerminatorEscapeLeftWrapSymbol { get; set; }

        public char TerminatorEscapeRightWrapSymbol { get; set; }

        public string[][] ReadToTable(string text)
        {
            char[] textChars = text.ToCharArray();

            Queue<string> fields = new Queue<string>();
            StringBuilder stringBuilder = new StringBuilder();

            bool escapeState = false;
            for (int i = 0, len = textChars.Length; i < len; ++i)
            {
                char ch = textChars[i];
                if (ch == this.SingleCharEscapeSymbol)
                {
                    // Do not include the escape char in output
                    // stringBuilder.Append(ch);
                    if (!(i < len - 1))
                    {
                        throw new ApplicationException("Invalid escape of last character of the text.");
                    }

                    stringBuilder.Append(textChars[++i]);
                }
                else if (ch == this.TerminatorEscapeLeftWrapSymbol && ch == this.TerminatorEscapeRightWrapSymbol)
                {
                    // Do not include the escape char in output
                    // stringBuilder.Append(ch);
                    escapeState = !escapeState;
                }
                else if (ch == this.TerminatorEscapeLeftWrapSymbol)
                {
                    // Do not include the escape char in output
                    // stringBuilder.Append(ch);
                    if (!escapeState)
                    {
                        escapeState = true;
                    }
                }
                else if (ch == this.TerminatorEscapeRightWrapSymbol)
                {
                    // Do not include the escape char in output
                    // stringBuilder.Append(ch);
                    if (escapeState)
                    {
                        escapeState = false;
                    }
                }
                else if (ch == this.FieldTerminator)
                {
                    if (escapeState)
                    {
                        stringBuilder.Append(ch);
                    }
                    else
                    {
                        fields.Enqueue(stringBuilder.ToString());
                        stringBuilder.Clear();
                    }
                }
                else if (ch == this.RowTerminator)
                {
                    if (escapeState)
                    {
                        stringBuilder.Append(ch);
                    }
                    else
                    {
                        // The row terminator is actually a field terminator
                        fields.Enqueue(stringBuilder.ToString());
                        stringBuilder.Clear();

                        this.rows.Enqueue(fields.ToArray());
                        fields = new Queue<string>();
                    }
                }
                else
                {
                    stringBuilder.Append(ch);
                }
            }

            fields.Enqueue(stringBuilder.ToString());
            stringBuilder.Clear();

            this.rows.Enqueue(fields.ToArray());

            return this.rows.ToArray();
        }
    }
}