using PAR.Contracts.Common;

namespace PAR.Contracts.Dtos;

public class GroupDetailDto : BaseDto
{
    public string Name { get; set; }
    public int TeacherId { get; set; }
    public int SchoolYearId { get; set; }
    public string SchoolYearName { get; set; }
    public string TeacherName { get; set; }
}