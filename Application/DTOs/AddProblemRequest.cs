﻿namespace Application.DTOs;

public class AddProblemRequest
{
    public string ProblemName { get; set; }
    public string Location { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }

}