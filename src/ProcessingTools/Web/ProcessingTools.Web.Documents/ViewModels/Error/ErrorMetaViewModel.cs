namespace ProcessingTools.Web.Documents.ViewModels.Error
{
    using System.Collections.Generic;
    using System.Text;

    public class ErrorMetaViewModel
    {
        public ErrorMetaViewModel()
        {
            this.DestinationActions = new HashSet<ActionMetaViewModel>();
        }

        public object ErrorCode { get; set; }

        public string InstanceName { get; set; }

        public string Message { get; set; }

        public ActionMetaViewModel SourceAction { get; set; }

        public ICollection<ActionMetaViewModel> DestinationActions { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.InstanceName))
            {
                stringBuilder.Append(this.InstanceName);
                stringBuilder.Append(": ");
            }

            if (!string.IsNullOrWhiteSpace(this.Message))
            {
                stringBuilder.Append(this.Message);
            }
            else
            {
                stringBuilder.Append("Error");
            }

            if (this.ErrorCode != null)
            {
                stringBuilder.AppendFormat(" ({0})", this.ErrorCode);
            }

            return stringBuilder.ToString();
        }
    }
}