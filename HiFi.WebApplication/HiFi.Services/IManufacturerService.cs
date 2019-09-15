using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Services
{
    public interface IManufacturerService
    {
        IEnumerable<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturerById(int manufacturerId);
        bool InsertManufacturer(Manufacturer manufacturer);
        bool UpdateManufacturer(Manufacturer manufacturer);
        bool DeleteManufacturer(Manufacturer manufacturer);
    }
}
