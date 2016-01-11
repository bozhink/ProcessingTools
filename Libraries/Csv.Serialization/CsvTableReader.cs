namespace ProcessingTools.Csv.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CsvTableReader
    {
        private Queue<string[]> rows;

        public CsvTableReader()
            : this(new CsvObjectConfiguration())
        {
        }

        public CsvTableReader(CsvObjectConfiguration configuration)
        {
            this.Configuration = configuration;
            this.rows = new Queue<string[]>();
        }

        public CsvObjectConfiguration Configuration { get; private set; }

        public string[][] ReadToTable(string text)
        {
            char[] textChars = text.ToCharArray();

            Queue<string> fields = new Queue<string>();
            StringBuilder stringBuilder = new StringBuilder();

            bool escapeState = false;
            for (int i = 0, len = textChars.Length; i < len; ++i)
            {
                char ch = textChars[i];
                if (ch == this.Configuration.SingleCharEscapeSymbol)
                {
                    // Do not include the escape char in output
                    // stringBuilder.Append(ch);
                    if (!(i < len - 1))
                    {
                        throw new ApplicationException("Invalid escape of last character of the text.");
                    }

                    stringBuilder.Append(textChars[++i]);
                }
                else if (ch == this.Configuration.TerminatorEscapeLeftWrapSymbol && ch == this.Configuration.TerminatorEscapeRightWrapSymbol)
                {
                    // Do not include the escape char in output
                    // stringBuilder.Append(ch);
                    escapeState = !escapeState;
                }
                else if (ch == this.Configuration.TerminatorEscapeLeftWrapSymbol)
                {
                    // Do not include the escape char in output
                    // stringBuilder.Append(ch);
                    if (!escapeState)
                    {
                        escapeState = true;
                    }
                }
                else if (ch == this.Configuration.TerminatorEscapeRightWrapSymbol)
                {
                    // Do not include the escape char in output
                    // stringBuilder.Append(ch);
                    if (escapeState)
                    {
                        escapeState = false;
                    }
                }
                else if (ch == this.Configuration.FieldTerminator)
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
                else if (ch == this.Configuration.RowTerminator)
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