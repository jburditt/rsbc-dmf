using System;

namespace RSBC.DMF.MedicalPortal.API.ViewModels
{
    public class CaseDocument
    {
        public int DmerType { get; set; }
        public int DmerStatus { get; set; }
        public string IdCode { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public string Description { get; set; }
        public string DocumentUrl { get; set; }
        public string SubmittalStatus { get; set; }
        public string DocumentType { get; set; }
        public string DocumentSubType { get; set; }
        public string DocumentId { get; set; }
    }
}
