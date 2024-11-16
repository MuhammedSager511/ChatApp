namespace Application.Features.Admin.Queries.GetUsersWithRole
{
    public class UsersWithRoleDto
    {
        public string? Id { get; set; } 
        public string? UserName { get; set; }
        public IList<string>? Roles { get; set; }
    }
}