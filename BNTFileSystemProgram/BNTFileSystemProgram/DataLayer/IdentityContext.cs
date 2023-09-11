﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;


namespace DataLayer
{
    public class IdentityContext
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public IdentityContext(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedDataAsync(string adminUsername, string adminPass)
        {
            var admins = _context.Users.Where(u => u.Role == Role.Admin).ToList();
            if (admins.Count == 0)
            {
                await CreateAdminAccountAsync(adminUsername, adminPass);
            }
        }
        private async Task CreateAdminAccountAsync(string username, string password)
        {
            await CreateUserAsync(username, password, "admin", "admin", Role.Admin);
        }

        public async Task CreateUserAsync(string username, string password, string firstname, string lastname, Role role)
        {
            try
            {
                var user = new User
                {
                    UserName = username,
                    FirstName = firstname,
                    LastName = lastname,
                    Role = role
                };

                IdentityResult result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    switch (role)
                    {
                        
                        case Role.User:
                            await _userManager.AddToRoleAsync(user, Role.User.ToString());
                            break;
                        case Role.Editor:
                            await _userManager.AddToRoleAsync(user, Role.Editor.ToString());
                            break;
                        case Role.Admin:
                            await _userManager.AddToRoleAsync(user, Role.Admin.ToString());
                            break;
                    }
                }
                else
                {
                    throw new ArgumentException("Error creating user!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> LogInUserAsync(string username, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);

                if (user == null)
                {
                    return null;
                }

                var result = await _userManager.PasswordValidators[0].ValidateAsync(_userManager, user, password);

                if (result.Succeeded)
                {
                    return await _context.Users.FindAsync(user.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> FindUserByNameAsync(string name)
        {
            try
            {
                return await _userManager.FindByNameAsync(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteUserByNameAsync(string name)
        {
            try
            {
                var user = await FindUserByNameAsync(name);

                if (user == null)
                {
                    throw new InvalidOperationException("User not found!");
                }

                await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //TODO: Add pdateUserAsync

        public async Task ChangeUserPasswordAsync(User user, string currentPassword, string newPassword)
        {
            await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }













    }
}
