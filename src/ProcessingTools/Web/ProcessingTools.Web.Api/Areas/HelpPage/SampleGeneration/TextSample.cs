namespace ProcessingTools.Web.Api.Areas.HelpPage
{
    using System;

    /// <summary>
    /// This represents a pre-formatted text sample on the help page. There's a display template named TextSample associated with this class.
    /// </summary>
    public class TextSample
    {
        public TextSample(string text)
        {
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Text { get; private set; }

        public override bool Equals(object obj)
        {
            TextSample other = obj as TextSample;
            return other != null && this.Text == other.Text;
        }

        public override int GetHashCode()
        {
            return this.Text.GetHashCode();
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
