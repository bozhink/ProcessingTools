namespace ProcessingTools.Csv.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CsvObject
    {
        public CsvObject(
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
            this.TerminatorEscapeLeftWrapSymbol = terminatorEscapeLeftWrapSymbol;
            this.TerminatorEscapeRightWrapSymbol = terminatorEscapeRightWrapSymbol;
        }

        public char FieldTerminator { get; private set; }

        public char RowTerminator { get; private set; }

        public int FirstRow { get; private set; }

        public char SingleCharEscapeSymbol { get; private set; }

        public char TerminatorEscapeLeftWrapSymbol { get; private set; }

        public char TerminatorEscapeRightWrapSymbol { get; private set; }

        // TODO
        public Queue<Queue<string>> Deserialize(string text)
        {
            char[] textChars = text.ToCharArray();

            Queue<Queue<string>> rows = new Queue<Queue<string>>();

            Queue<string> fields = new Queue<string>();
            StringBuilder stringBuilder = new StringBuilder();

            bool escapeState = false;
            for (int i = 0, len = textChars.Length; i < len; ++i)
            {
                char ch = textChars[i];
                if (ch == this.SingleCharEscapeSymbol)
                {
                    stringBuilder.Append(ch);

                    if (!(i < len - 1))
                    {
                        throw new ApplicationException("Invalid escape of last character of the text.");
                    }

                    stringBuilder.Append(textChars[++i]);
                }
                else if (ch == this.TerminatorEscapeLeftWrapSymbol && ch == this.TerminatorEscapeRightWrapSymbol)
                {
                    // Equal TerminatorEscapeLeftWrapSymbol and TerminatorEscapeRightWrapSymbol
                    stringBuilder.Append(ch);
                    escapeState = !escapeState;
                }
                else if (ch == this.TerminatorEscapeLeftWrapSymbol)
                {
                    stringBuilder.Append(ch);
                    if (!escapeState)
                    {
                        escapeState = true;
                    }
                }
                else if (ch == this.TerminatorEscapeRightWrapSymbol)
                {
                    stringBuilder.Append(ch);
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

                        rows.Enqueue(fields);
                        fields.Clear();
                    }
                }
                else
                {
                    stringBuilder.Append(ch);
                }
            }

            fields.Enqueue(stringBuilder.ToString());
            stringBuilder.Clear();

            rows.Enqueue(fields);
            fields.Clear();

            return rows;
        }
    }
}