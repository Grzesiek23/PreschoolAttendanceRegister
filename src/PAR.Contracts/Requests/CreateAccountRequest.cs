﻿namespace PAR.Contracts.Requests;

public class CreateAccountRequest
{
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string? ConfirmPassword { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}