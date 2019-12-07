using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetAllManufacturers();
        Task<Manufacturer> GetManufacturerById(int manufacturerId);
        Task<Manufacturer> InsertManufacturer(Manufacturer manufacturer);
        Task<bool> UpdateManufacturer(Manufacturer manufacturer);
        Task<bool> DeleteManufacturer(Manufacturer manufacturer);
        Task<Manufacturer> GetManufacturerByIdAsync(int manufacturerId);
        Task<Manufacturer> GetManufacturerByProductId(int productId);
    }
}
