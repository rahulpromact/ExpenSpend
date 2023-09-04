﻿using AutoMapper;
using ExpenSpend.Core.User;
using ExpenSpend.Domain.Models;
using ExpenSpend.Repository.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenSpend.Web.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLoggedInUser()
    {
        var user =  await _userRepository.GetLoggedInUser(User);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(_mapper.Map<GetUserDto>(user));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(_mapper.Map<GetUserDto>(user));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser(string id, UpdateUserDto input)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        
        var result = await _userRepository.UpdateUserAsync(_mapper.Map(input, user));
        
        if (result.Succeeded)
        {
            return Ok("User updated successfully");
        }
        return BadRequest(result.Errors);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        var result = await _userRepository.DeleteUserAsync(user);
        if (result.Succeeded)
        {
            return Ok("User deleted successfully");
        }
        return BadRequest(result.Errors);
    }
}