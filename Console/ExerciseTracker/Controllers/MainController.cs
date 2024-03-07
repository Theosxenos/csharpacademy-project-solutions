using ExerciseTracker.Data;
using ExerciseTracker.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace ExerciseTracker.Controllers;

public class MainController(ExerciseContext context)
{
    public void ShowMenu()
    {
    }
}