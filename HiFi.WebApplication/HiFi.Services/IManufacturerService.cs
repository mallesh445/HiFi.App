using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services
{
    public interface IManufacturerService
    {
        IEnumerable<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturerById(int manufacturerId);
        Manufacturer InsertManufacturer(Manufacturer manufacturer);
        bool UpdateManufacturer(Manufacturer manufacturer);
        bool DeleteManufacturer(Manufacturer manufacturer);
        Task<Manufacturer> GetManufacturerByIdAsync(int manufacturerId);
    }
}
