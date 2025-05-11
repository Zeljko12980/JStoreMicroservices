
using Auth.Application.Common.Interface;

namespace Auth.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly ITokenGenerator _tokenGenerator;

        public IdentityService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager,IEmailService emailService,ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _tokenGenerator = tokenGenerator;
        }
        #region UserSection
        public async ValueTask<(bool isSucceed, string userId)> CreateUserAsync(string username, string password, string email, string fullName, List<string> roles)
        {
            var user = new ApplicationUser()
            {
                FullName = fullName,
                UserName = username,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }

            var addUserRole = await _userManager.AddToRolesAsync(user, roles);
            if (!addUserRole.Succeeded)
            {
                throw new ValidationException(addUserRole.Errors);
            }
            return (result.Succeeded, user.Id);


        }



        public async ValueTask<List<(string id, string fullName, string userName, string email)>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Select(x => new
            {
                x.Id,
                x.UserName,
                x.Email,
                x.FullName,
            }).ToListAsync(cancellationToken);
            return users.Select(user => (user.Id, user.FullName, user.UserName, user.Email)).ToList();
        }

        public async ValueTask<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (user.UserName == "system" || user.UserName == "admin")
            {
                throw new Exception("You can not delete system or admin user");

            }
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        public async ValueTask<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }
        public async ValueTask<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }
        public async ValueTask<bool> UpdateUserProfile(string id, string fullName, string email, IList<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.FullName = fullName;
            user.Email = email;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async ValueTask<bool> SigninUserAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, true, false);
            return result.Succeeded;
        }
        public async ValueTask<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
                //throw new Exception("User not found");
            }
            return await _userManager.GetUserIdAsync(user);
        }

        #endregion

        #region User's Role Section
        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user is null)
            {
                throw new NotFoundException("User not found");

            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async ValueTask<bool> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user is null)
            {
                throw new NotFoundException("User not found");

            }
            var result = await _userManager.AddToRolesAsync(user, roles);
            return result.Succeeded;
        }

        public async ValueTask<bool> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            result = await _userManager.AddToRolesAsync(user, usersRole);
            return result.Succeeded;
        }



        #endregion

        #region Role Section
        public async ValueTask<bool> CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;
        }

        public async ValueTask<List<(string id, string roleName)>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToListAsync();
            return roles.Select(role => (role.Id, role.Name)).ToList();
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var roleDetails = await _roleManager.FindByIdAsync(roleId);
            if (roleDetails is null)
            {
                throw new NotFoundException("Role not found");
            }
            if (roleDetails.Name == "Administrator")
            {
                throw new BadRequestException("You can not delete Administrator Role");

            }
            var result = await _roleManager.DeleteAsync(roleDetails);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }
            return result.Succeeded;


        }

        public async ValueTask<(string id, string roleName)> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return (role.Id, role.Name);
        }

        public async ValueTask<bool> UpdateRole(string id, string roleName)
        {
            if (roleName != null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                role.Name = roleName;
                var result = await _roleManager.UpdateAsync(role);
                return result.Succeeded;
            }
            return false;
        }

        public async ValueTask<(bool isSucceed, string userId)> RegisterUserAsync(string username, string password, string email, string fullName)
        {
            // Kreiraj novog korisnika
            var user = new ApplicationUser()
            {
                FullName = fullName,
                UserName = username,
                Email = email,
                TwoFactorEnabled = false, // 2FA nije omogućeno dok korisnik ne potvrdi email
            };

            // Registracija korisnika
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new ValidationException(result.Errors);
            }

            // Dodeljivanje rola korisniku
            var addUserRole = await _userManager.AddToRoleAsync(user, "User");
            if (!addUserRole.Succeeded)
            {
                throw new ValidationException(addUserRole.Errors);
            }

            // Generisanje tokena za email potvrdu
            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Kreiranje URL-a za potvrdu
            var confirmationUrl = $"https://localhost:6064/api/auth/confirmemail?token={confirmationToken}&userId={user.Id}";

            // Slanje emaila sa linkom za potvrdu
            var emailSubject = "Confirm your email address";
            var emailBody = $"Please confirm your email address by clicking the following link: {confirmationUrl}";

            await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);

            return (true, user.Id);
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return false; // Invalid input
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false; // User not found
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
           /* if (!result.Succeeded)
            {
                return false; // Email confirmation failed
            }
           */
            // After email confirmation, enable two-factor authentication
            // Example: enable 2FA for the user
            await _userManager.SetTwoFactorEnabledAsync(user, true);

            // Now you can update the user in case you want to store other changes
            await _userManager.UpdateAsync(user);

            return true;


       
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email)
                   ?? throw new Exception("User not found.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid credentials.");

            var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
            await _emailService.SendEmailAsync(email, "2FA Code", $"Your 2FA code is: {code}");

            return true;
        }

        public async Task<(string id, string userName, string token)> VerifyTwoFactorCodeAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email)
                    ?? throw new Exception("User not found.");

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider, code);
            if (!isValid)
                throw new Exception("Invalid verification code.");

            var roles= await _userManager.GetRolesAsync(user);

            var token= _tokenGenerator.GenerateJWTToken((user.Id,user.UserName,roles));

            return (user.Id, user.UserName, token);
        }


        #endregion

    }
}
