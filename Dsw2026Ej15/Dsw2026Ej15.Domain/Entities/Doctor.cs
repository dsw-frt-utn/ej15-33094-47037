using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; private set; }
    public string LicenseNumber { get; init; }
    public bool IsActive { get; private set; }
    public Speciality? Speciality { get; private set; }
    public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) :base(id)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        IsActive = true;
        Speciality = speciality;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
    public void Activate()
    {
        IsActive = true;
    }
}
