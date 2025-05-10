
namespace Auth.Application.Common.Interface
{
    public interface IIdentityService
    {

        #region User Section
        ValueTask<(bool isSucceed, string userId)> CreateUserAsync(string username, string password, string email,
            string fullName, List<string> roles);
        ValueTask<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync(CancellationToken cancellationToken);
        ValueTask<bool> DeleteUserAsync(string userId);
        ValueTask<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId);
        ValueTask<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);
        ValueTask<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles);
        ValueTask<bool> SigninUserAsync(string userName, string password);
        ValueTask<string> GetUserIdAsync(string userName);


        #endregion


        #region User's Role Section
        Task<List<string>> GetUserRolesAsync(string userId);
        ValueTask<bool> AssignUserToRole(string userName, IList<string> roles);
        ValueTask<bool> UpdateUsersRole(string userName, IList<string> usersRole);

        #endregion

        #region Role Section
        ValueTask<bool> CreateRoleAsync(string roleName);
        ValueTask<List<(string id, string roleName)>> GetRolesAsync();
        Task<bool> DeleteRoleAsync(string roleId);
        ValueTask<(string id, string roleName)> GetRoleByIdAsync(string id);
        ValueTask<bool> UpdateRole(string id, string roleName);




        #endregion
    }
}
