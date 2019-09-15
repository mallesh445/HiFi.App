using HiFi.Data.Models;
using HiFi.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

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

        public bool InsertManufacturer(Manufacturer manufacturer)
        {
            _manufacturerRepository.Insert(manufacturer);
            return true;
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
