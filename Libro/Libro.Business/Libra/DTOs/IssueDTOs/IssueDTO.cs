namespace Libro.Business.Libra.DTOs.IssueDTOs
{
    public record IssueR_DTO(
        Guid Id,
        string POSName,
        string CreatedBy,
        DateTime CreationDate,
        string IssueType,
        string Status,
        string AssignedTo,
        string? Memo,
        string Priority);

    public class IssueDTO {
        public Guid Id;
        public string POSName;
        public string CreatedBy;
        public DateTime CreationDate;
        public string IssueType;
        public string Status;
        public string AssignedTo;
        public string? Memo;
        public string Priority;
    }
}
