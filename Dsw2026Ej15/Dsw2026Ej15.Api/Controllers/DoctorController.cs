using System.ComponentModel.DataAnnotations;
using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("doctors")]
public class DoctorController : AppController
{
    private readonly IPersistence _persistence;
    public DoctorController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {


        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Doctor name is required.");
        if (string.IsNullOrWhiteSpace(request.LicenseNumber))
            throw new ValidationException("Doctor license number is required.");

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality == null)
            throw new ValidationException("Speciality not found.");

       
        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created();
    }

    [HttpGet]
    
    public async Task<IActionResult> GetDoctor()
    {
        var doctors = _persistence.GetActiveDoctors();
        var doctorResponse = new List<DoctorModel.Response>();

        doctorResponse = [.. doctors.Where(d => d.IsActive)
            .Select(d => new DoctorModel.Response(d.Id,d.Name, d.LicenseNumber ,d.Speciality.Name))];

        return Ok(doctorResponse);

    }


    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDoctorById([FromRoute] Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);
        if (doctor is not null)
        {
            var doctorResponse = new DoctorModel.Response(doctor.Id,doctor.Name, doctor.LicenseNumber, doctor?.Speciality.Name);
            return Ok(doctorResponse);
        }

        return NotFound("No se encontró un doctor con el ID ingresado o no esta activo");

    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteDoctorById([FromRoute] Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);
        if (doctor is not null)
        {
            doctor.Deactivate();
            return NoContent();
        }
        return NotFound("No se encontró un doctor con el ID ingresado o no esta activo");


    }
}
