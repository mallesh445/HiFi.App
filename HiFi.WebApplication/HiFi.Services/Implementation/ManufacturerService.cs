using HiFi.Data.Models;
using HiFi.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services.Implementation
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IMemoryCache _memoryCache;

        public ManufacturerService(IMemoryCache memoryCache,IRepository<Manufacturer> manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
            _memoryCache = memoryCache;
        }

        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var data = _manufacturerRepository.GetAll();
            return data;
        }

        public Manufacturer GetManufacturerById(int manufacturerId)
        {
            return _manufacturerRepository.GetById(manufacturerId);
        }
        
        public async Task<Manufacturer> GetManufacturerByIdAsync(int manufacturerId)
        {
            var result = _manufacturerRepository.GetById(manufacturerId);
            return result;
        }

        public Manufacturer InsertManufacturer(Manufacturer manufacturer)
        {
            //_manufacturerRepository.Insert(manufacturer);
            //return true;
            return _manufacturerRepository.InsertData(manufacturer); ;
        }

        public bool UpdateManufacturer(Manufacturer manufacturer)
        {
            _manufacturerRepository.Update(manufacturer);
            return true;
        }
        public bool DeleteManufacturer(Manufacturer manufacturer)
        {
            _manufacturerRepository.Delete(manufacturer);
            return true;
        }
    }
}
